using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPopUpData",menuName = "Data/NewPopUpData")]
public class PopUpServiceScriptableObject : ScriptableObject
{
    public List<PopUp> popUpPrefabs;

    private List<PopUp> popUpInstances = new List<PopUp>();

    private void Awake()
    {
        popUpInstances.Clear();
    }

    public PopUp CreatePopup(PopupType popupType,RectTransform canvasTransform)
    {
        var popUpInstance = popUpInstances.Find(popUp => popUp.popupType == popupType);
        if(popUpInstance == null)
        {
            PopUp popUp = popUpPrefabs.Find(popup => popup.popupType == popupType);
            var Popup = Object.Instantiate(popUp, canvasTransform);
            popUpInstances.Add(Popup);
            return Popup;
        }
        return popUpInstance;
    }

}
