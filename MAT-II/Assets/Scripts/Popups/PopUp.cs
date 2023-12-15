using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PopUp : MonoBehaviour
{
    public PopupType popupType;
    public virtual void Show() => this.gameObject.SetActive(true);
    public virtual void  Hide() => this.gameObject.SetActive(false);


}
