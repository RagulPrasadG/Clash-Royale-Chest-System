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
    private ChestController selectedChest;
    private Queue<ChestController> chestsQueue = new Queue<ChestController>();

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
        this.eventService.onChestButtonClicked.AddListener(OnChestClicked);
        this.eventService.onClickUnlockWithGems.AddListener(OnUnlockWithGems);
        this.eventService.onClickUnlockWithTimer.AddListener(OnUnlockWithTimer);
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

    public void OnUnlockWithTimer()
    {
        if (chestsQueue.Count > maxQueue)
            return;
            
        if(chestsQueue.Count > 0)
        {
            foreach (var chest in chestsQueue)
            {
                if (chest.Data.chestStatus == ChestStatus.UNLOCKING)
                {
                    selectedChest.SetChestStatus(ChestStatus.QUEUED);
                    selectedChest.RemoveListeners();
                    chestsQueue.Enqueue(selectedChest);
                    selectedChest = null;
                    return;
                }

            }
        }

        selectedChest.StartUnlockTimer();
        selectedChest.RemoveListeners();
        chestsQueue.Enqueue(selectedChest);
        selectedChest = null;


    }

    public void OnUnlockWithGems()
    {

    }

    public void TryAddChest()
    {
        ChestSlotController freeSlot = GetFreeSlot();
        if(freeSlot != null)
        {
            ChestController chestController = new ChestController(this.chestView, gameDataScriptableObject.GetRandomChestData(),eventService);
            chestController.SetSlot(freeSlot);
            freeSlot.isSlotEmpty = false;
        }

    }

    public void OnChestClicked(ChestController chestController)
    {
        selectedChest = chestController;
        uIService.ShowUnlockConfirmationPopup(chestController);
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
