using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPopUpData",menuName = "Data/NewPopUpData")]
public class PopUpServiceScriptableObject : ScriptableObject
{
    public List<PopUp> popUpPrefabs;

    private List<PopUp> popUpInstances = new List<PopUp>();
    public void ShowPopUp(PopupType popupType)
    {
        var popUpInstance = popUpInstances.Find(popUp => popUp.popupType == popupType);
        if(popUpInstance == null)
        {
            PopUp popUp = popUpPrefabs.Find(popup => popup.popupType == popupType);
            var Popup = Object.Instantiate(popUp);
            Popup.Show();
        }
        popUpInstance.Show();
    }

}
