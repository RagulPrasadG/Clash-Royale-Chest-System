using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestService : MonoBehaviour
{
    
    [SerializeField] RectTransform slotContainer;
    [SerializeField] ChestSlotView chestSlotView;
    [SerializeField] ChestView chestView;
    [SerializeField] GameDataScriptableObject gameDataScriptableObject;

    private EventService eventService;
    private UIService uIService;
    private List<ChestSlotController> slots = new List<ChestSlotController>();

    [Space(10)]
    [Header("CONFIG")]
    public int maxSlot;
    public int maxQueue;

    private void Start()
    {
        CreateSlots();  
    }

    public void Init(EventService eventService,UIService uIService)
    {
        this.eventService = eventService;
        this.uIService = uIService;
        SetEvents();
    }

    public void SetEvents()
    {
        this.eventService.onAddChestButtonClicked.AddListener(TryAddChest);
    }

    public void CreateSlots()
    {
        for(int i = 0;i<maxSlot;i++)
        {
            ChestSlotController chestSlotController = new ChestSlotController(chestSlotView);
            chestSlotController.SetParent(slotContainer);
            slots.Add(chestSlotController);
        }

    }

    public void TryAddChest()
    {
        ChestSlotController freeSlot = GetFreeSlot();
        if(freeSlot != null)
        {
            ChestController chestController = new ChestController(this.chestView, gameDataScriptableObject.GetRandomChestData());
            chestController.SetSlot(freeSlot);
            freeSlot.isSlotEmpty = false;
        }

    }

    public ChestSlotController GetFreeSlot()
    {
        foreach(var slot in slots)
        {
            if (slot.isSlotEmpty)
                return slot;
        }
        uIService.ShowInfoPopup("No Free Slots Available!!");
        return null;
       
    }
}
