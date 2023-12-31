using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIService : MonoBehaviour
{
    [SerializeField] RectTransform canvas;
    [SerializeField] TMP_Text coinText;
    [SerializeField] TMP_Text gemText;
    [SerializeField] Button undoButton;
    [SerializeField] Button addChestButton;
    [SerializeField] ChestService chestService;
    [SerializeField] PopUpServiceScriptableObject popUpService;

    private EventService eventService;
    private GameService gameService;

    public void Init(EventService eventService,GameService gameService)
    {
        this.eventService = eventService;
        this.gameService = gameService;
        SetEvents();
    }

    public void SetEvents()
    {
        addChestButton?.onClick.AddListener(OnAddChestButtonClicked);
        undoButton?.onClick.AddListener(OnUndoButtonClicked);
    }

    public void OnAddChestButtonClicked()
    {
        eventService.onAddChestButtonClicked.RaiseEvent();
        Debug.Log("Click");
    }

    public void ShowInfoPopup(string message)
    {
        InfoPopup infoPopUp = popUpService.CreatePopup(PopupType.INFO,canvas) as InfoPopup;
        infoPopUp.SetMessageText(message);
        infoPopUp.Show();
    }

    public void ShowUnlockConfirmationPopup(ChestController chestController)
    {
        UnlockConfirmationPopup unlockConfirmPopup = popUpService.CreatePopup(PopupType.CHESTOPENCONFIRMATION, canvas) as UnlockConfirmationPopup;
        unlockConfirmPopup.Init(chestController,eventService);
        unlockConfirmPopup.Show();
    }

    public void ShowRewardsPanel(List<RewardData> rewardData)
    {
        RewardsPanel rewardsPanel = popUpService.CreatePopup(PopupType.REWARDSPANEL, canvas) as RewardsPanel;
        rewardsPanel.Init(rewardData,gameService);
        rewardsPanel.Show();
    }


    public void OnUndoButtonClicked() => gameService.Undo();

    public void UpdateCurrencyUI()
    {
        coinText.text = gameService.Coins.ToString();
        gemText.text = gameService.Gems.ToString();
    }

}
