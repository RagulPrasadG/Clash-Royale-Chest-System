using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnlockConfirmationPopup : PopUp
{
    [SerializeField] Button closeButton;
    [SerializeField] Button unlockWithTimerButton;
    [SerializeField] Button unlockWithGemButton;

    [SerializeField] Image chestImage;
    [SerializeField] TMP_Text gemText;

    private EventService eventService;
    private ChestController selectedChestController;

    private void Awake()
    {
        closeButton.onClick.AddListener(Hide);
        unlockWithTimerButton.onClick.AddListener(OnClickUnlockWithTimer);
        unlockWithGemButton.onClick.AddListener(OnClickUnlockWithGems);
    }

    public void OnClickUnlockWithTimer()
    {
        this.eventService.onClickUnlockWithTimer.RaiseEvent();
        Hide();
    }

    public void OnClickUnlockWithGems()
    {
        this.eventService.onClickUnlockWithGems.RaiseEvent(selectedChestController);
        Hide();
    }

    public void Init(ChestController chestController,EventService eventService)
    {
        this.selectedChestController = chestController;
        this.chestImage.sprite = chestController.Data.chestDataSO.chestSprite;
        this.gemText.text = chestController.GetOpenNowCost().ToString();
        this.eventService = eventService;
    }

  

}
