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

    private List<ChestSlotController> slots = new List<ChestSlotController>();
    private Queue<ChestController> chestsQueue = new Queue<ChestController>();
    private ChestController selectedChest;


    private GameService gameService;
    private EventService eventService;
    private UIService uIService;

    [Space(10)]
    [Header("CONFIG")]
    public int maxSlot;
    public int maxQueue;

    private void Start()
    {
        CreateSlots();  
    }

    public void Init(EventService eventService,UIService uIService,GameService gameService)
    {
        this.eventService = eventService;
        this.uIService = uIService;
        this.gameService = gameService;
        SetEvents();
    }

    public void SetEvents()
    {
        this.eventService.onAddChestButtonClicked.AddListener(TryAddChest);
        this.eventService.onChestButtonClicked.AddListener(OnChestClicked);
        this.eventService.onClickUnlockWithGems.AddListener(OnUnlockWithGems);
        this.eventService.onClickUnlockWithTimer.AddListener(OnUnlockWithTimer);
        this.eventService.onChestTimerComplete.AddListener(OnChestTimerComplete);
        this.eventService.onChestUnlocked.AddListener(OnChestUnlocked);
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

    public void OnChestUnlocked(ChestController chestController)
    {
        uIService.ShowRewardsPanel(chestController.Data.chestDataSO.rewardData);
        ChestSlotController chestSlotController = slots.Find(chestSlot => chestSlot.chestController == chestController);
        chestSlotController.RemoveChest();
    }


    public void OnUnlockWithTimer()
    {
        if (chestsQueue.Count > maxQueue)
            return;

        if (chestsQueue.Count > 0)
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

    public void OnUnlockWithGems(ChestController chestController)
    {
        selectedChest = chestController;
        if (selectedChest.GetOpenNowCost() > gameService.Gems)
        {
            uIService.ShowNotEnoughGemsPanel();
            return;
        }

        UnlockWithGemsCommand unlockWithGemsCommand = new UnlockWithGemsCommand(chestController,this);
        gameService.commandInvoker.ProcessCommand(unlockWithGemsCommand);
        
    }

    public void OpenWithGems()
    {
        chestsQueue.Enqueue(selectedChest);
        selectedChest.Unlock();
        selectedChest.StopTimer();
        this.gameService.ReduceGemAmount(selectedChest.GetOpenNowCost());
    }

    public void UndoOpenWithGems(ChestController chestController)
    {
        //if the chest is unlocking while instantopen then resume the timer or keep the chest locked
        if (chestController.Data.currentTime < chestController.Data.chestDataSO.unlockTime * 60)
        {
            if (chestsQueue.Count > 0)
            {
                foreach (var chest in chestsQueue)
                {
                    if (chest.Data.chestStatus == ChestStatus.UNLOCKING ||
                        chest.Data.chestStatus == ChestStatus.QUEUED)

                    {
                        chestController.SetQueued();
                        chestsQueue.Enqueue(chestController);
                        return;
                    }

                }
            }

            chestsQueue.Enqueue(chestController); 
            chestController.ResumeTimer();
        }
            
        else
            chestController.Lock();
            
        gameService.AddGemAmount(chestController.GetOpenNowCost());
    }

    public void TryAddChest()
    {
        ChestSlotController freeSlot = GetFreeSlot();
        if(freeSlot != null)
        {
            ChestController chestController = new ChestController(this.chestView, gameDataScriptableObject.GetRandomChestData(),eventService);
            chestController.SetSlot(freeSlot);
            freeSlot.AddChest(chestController);
            freeSlot.isSlotEmpty = false;
        }

    }

    public void OnChestClicked(ChestController chestController)
    {
        selectedChest = chestController;
        uIService.ShowUnlockConfirmationPopup(chestController);
    }

    public void OnChestTimerComplete(ChestController chestController)
    {
        chestsQueue.Dequeue();
        ChestController nextchestController;
        chestsQueue.TryPeek(out nextchestController);

        if (nextchestController != null)
            nextchestController.StartUnlockTimer();

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
