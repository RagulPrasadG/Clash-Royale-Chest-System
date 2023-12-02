using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIService : MonoBehaviour
{
    [SerializeField] RectTransform canvas;
    [SerializeField] Button addChestButton;
    [SerializeField] ChestService chestService;
    [SerializeField] PopUpServiceScriptableObject popUpService;

    private EventService eventService;

    public void Init(EventService eventService)
    {
        this.eventService = eventService;
        SetEvents();
    }

    public void SetEvents()
    {
        addChestButton?.onClick.AddListener(OnAddChestButtonClicked);
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

}
