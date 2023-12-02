using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController
{
    private ChestView chestView;
    private ChestData chestData;
    private float totalTimeInSeconds;
    private float remainingTimeInSeconds;
    private WaitForSeconds waitSeconds = new WaitForSeconds(1f);

    public ChestData Data => chestData;
    public ChestController(ChestView chestViewPrefab,ChestDataScriptableObject chestDataScriptableObject)
    {
        this.chestView = UnityEngine.Object.Instantiate(chestViewPrefab);
        this.chestView.SetController(this);
        this.chestData = new ChestData {
            chestDataSO = chestDataScriptableObject
        };
        this.totalTimeInSeconds = chestData.chestDataSO.unlockTime * 60;
        SetChestStatus(ChestStatus.LOCKED);
    }

    public IEnumerator StartTimer()
    {
        remainingTimeInSeconds = totalTimeInSeconds;
        while (remainingTimeInSeconds >= 0)
        {
            yield return waitSeconds;
            UpdateTimer();
        }
    }

    public void Unlock()
    {
        if (this.chestData.chestStatus == ChestStatus.UNLOCKING ||
            this.chestData.chestStatus == ChestStatus.UNLOCKED)
            return;

        this.chestData.chestStatus = ChestStatus.UNLOCKING;
        this.chestView.UpdateChestStatusText("Unlocking...");
        this.chestView.StartTimerCouroutine();
    }

    public void UpdateTimer() 
    {
        remainingTimeInSeconds--;

        if (remainingTimeInSeconds >= 0)
        {
            FormatTime(remainingTimeInSeconds);
        }
        else
        {
            chestView.StopCoroutine(StartTimer());
        }
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

    public void FormatTime(float seconds)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);
        string timerFormat;
        if (timeSpan.Hours > 0)
        {
            timerFormat = $"{timeSpan.Hours}h:{timeSpan.Minutes}m";
        }
        else if (timeSpan.Minutes > 0)
        {
            timerFormat =  $"{timeSpan.Minutes}m:{timeSpan.Seconds}s";
        }
        else
        {
            timerFormat =  $"{timeSpan.Seconds}s";
        }
        chestView.UpdateChestTimerText(timerFormat);
    }

    public void Destroy() => UnityEngine.Object.Destroy(this.chestView.gameObject);

}
