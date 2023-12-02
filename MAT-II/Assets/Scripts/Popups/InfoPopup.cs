using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
public class InfoPopup : PopUp
{
    [SerializeField] Button okButton;
    [SerializeField] TMP_Text messageText;

    private string message { get; set; }

    private void Awake()
    {
        okButton.onClick.AddListener(Hide);
    }

    public void SetMessageText(string message)
    {
        this.message = message;
        messageText.text = message;
    }
}
