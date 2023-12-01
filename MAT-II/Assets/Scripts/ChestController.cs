using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController
{
    private ChestView chestView;
    private ChestData chestData;

    public ChestData Data => chestData;

    public ChestController(ChestView chestViewPrefab,ChestData chestData)
    {
        this.chestView = Object.Instantiate(chestViewPrefab);
        this.chestView.SetController(this);
        this.chestData = chestData;
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
   
}
