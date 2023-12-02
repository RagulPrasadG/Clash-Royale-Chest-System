using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PopUp : MonoBehaviour
{
    public PopupType popupType { get; set; }
    public abstract void Show();
    public abstract void  Hide();
}
