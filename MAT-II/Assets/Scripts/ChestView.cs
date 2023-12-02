using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ChestView : MonoBehaviour
{
    [SerializeField] Button chestButton;
    [SerializeField] TMP_Text chestStatusText;
    [SerializeField] TMP_Text chestTimerText;

    private ChestController chestController;

    private void Awake()
    {
        chestButton.onClick.AddListener(OnClickChestButton);  
        
    }

    public void SetController(ChestController chestController) => this.chestController = chestController;

    public void OnClickChestButton()
    {
        Debug.Log("Chest Clicked!");
        chestController.Unlock();
    }

    public void StartTimerCouroutine() => StartCoroutine(chestController.StartTimer());
    public void StopTimerCouroutine() => StopCoroutine(chestController.StartTimer());


    public void UpdateChestStatusText(string status) => chestStatusText.text = status;
    public void UpdateChestTimerText(string timer) => chestTimerText.text = timer;


}
