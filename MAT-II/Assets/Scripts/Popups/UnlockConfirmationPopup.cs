using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnlockConfirmationPopup : PopUp
{
    [SerializeField] Button closeButton;
    [SerializeField] Button unlockWithTimerButton;
    [SerializeField] Button unlockWithGem;

    [SerializeField] Image chestImage;
    [SerializeField] TMP_Text gemText;

    private void Awake()
    {
        closeButton.onClick.AddListener(Hide);
    }

    
    public void Init(ChestController chestController)
    {
        this.chestImage.sprite = chestController.Data.chestDataSO.chestSprite;
        this.gemText.text = chestController.GetOpenNowCost().ToString();
        
    }
}
