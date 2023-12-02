using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController
{
    private ChestView chestView;
    private ChestData chestData;

    public ChestData Data => chestData;

    public ChestController(ChestView chestViewPrefab,ChestDataScriptableObject chestDataScriptableObject)
    {
        this.chestView = Object.Instantiate(chestViewPrefab);
        this.chestView.SetController(this);
        this.chestData = new ChestData {
            chestDataSO = chestDataScriptableObject
        };
        SetChestStatus(ChestStatus.LOCKED);
    }

    public void UpdateTimer() 
    {
        this.chestData.currentTimer += Time.deltaTime;
        chestView.UpdateChestTimerText(chestData.currentTimer);
    }

    public void SetChestStatus(ChestStatus chestStatus)
    {
        this.chestData.chestStatus = chestStatus;
        if (chestStatus == ChestStatus.LOCKED)
            chestView.UpdateChestStatusText("LOCKED");
    }

    public void SetSlot(ChestSlotController chestSlotController)
    {
        this.chestView.transform.SetParent((RectTransform)chestSlotController.View.transform);
        this.chestView.transform.position = chestSlotController.View.transform.position;
    }

    public void SetParent(RectTransform parent)
    {
        chestView.transform.SetParent(parent);

    }

    public void Destroy() => Object.Destroy(this.chestView.gameObject);

}
