using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController
{
    private ChestView chestView;
    private ChestData chestData;
    private float totalTimeInSeconds;
    private WaitForSeconds waitSeconds = new WaitForSeconds(1f);
    private EventService eventService;


    public ChestData Data => chestData;
   

    public ChestController(ChestView chestViewPrefab,ChestDataScriptableObject chestDataScriptableObject, EventService eventService)
    {
        this.chestView = UnityEngine.Object.Instantiate(chestViewPrefab);
        this.chestView.SetController(this);
        this.chestData = new ChestData
        {
            chestDataSO = chestDataScriptableObject
        };
        this.chestView.SetChestSprite(chestData.chestDataSO.chestSprite);
        this.totalTimeInSeconds = chestData.chestDataSO.unlockTime * 60;
        this.chestData.currentTime = totalTimeInSeconds;
        this.eventService = eventService;
        SetChestStatus(ChestStatus.LOCKED);
        FormatChestTimer(totalTimeInSeconds);
    }

    public IEnumerator StartTimer()
    {
        while (chestData.currentTime >= 0)
        {
            yield return waitSeconds;
            UpdateTimer();
        }
    }

    public void StartUnlockTimer()
    {
        if (this.chestData.chestStatus == ChestStatus.UNLOCKING ||
            this.chestData.chestStatus == ChestStatus.UNLOCKED)
            return;

        this.chestData.chestStatus = ChestStatus.UNLOCKING;
        this.chestView.chestTimerText.gameObject.SetActive(true);
        this.chestView.openNowButton.gameObject.SetActive(true);
        this.chestView.StartTimerCouroutine();

    }

    public void OnClickChest() => eventService.onChestButtonClicked.RaiseEvent(this);
 

    public void UpdateTimer() 
    {
        chestData.currentTime--;

        if (chestData.currentTime >= 0)
        {
            FormatChestTimer(chestData.currentTime);
        }
        else
        {
            StopTimer();
            Unlock();
        }
    }

    public void SetChestStatus(ChestStatus chestStatus)
    {
        this.chestData.chestStatus = chestStatus;
        switch (chestStatus)
        {
            case ChestStatus.LOCKED:
                chestView.UpdateChestStatusText("LOCKED");
                break;
            case ChestStatus.QUEUED:
                chestView.UpdateChestStatusText("QUEUED");
                break;
            case ChestStatus.UNLOCKED:
                chestView.UpdateChestStatusText("UNLOCKED");
                break;
        }

      
            
    }

    public void ResumeTimer()
    {
        this.chestView.chestTimerText.gameObject.SetActive(true);
        SetChestStatus(ChestStatus.LOCKED);
        StartUnlockTimer();
    }

    public void StopTimer()
    {
        this.chestView.chestTimerText.gameObject.SetActive(false);
        chestView.StopAllCoroutines();
    }

    public void OnUnlockWithGems() => this.eventService.onClickUnlockWithGems.RaiseEvent(this);

    public void Unlock()
    {
        SetChestStatus(ChestStatus.UNLOCKED);
        this.chestView.openButton.gameObject.SetActive(true);
        this.chestView.openNowButton.gameObject.SetActive(false);
        this.eventService.onChestTimerComplete.RaiseEvent(this);
    }

    public void Lock()
    {
        SetChestStatus(ChestStatus.LOCKED);
        this.chestView.openButton.gameObject.SetActive(false);
        this.chestView.openNowButton.gameObject.SetActive(false);
        this.chestView.chestTimerText.gameObject.SetActive(true);

        if(chestData.currentTime != totalTimeInSeconds)
           this.chestData.currentTime = totalTimeInSeconds;
    }

    public void SetQueued()
    {
        SetChestStatus(ChestStatus.QUEUED);
        this.chestView.openButton.gameObject.SetActive(false);
        this.chestView.openNowButton.gameObject.SetActive(false);
        
    }

    public void Open() => this.eventService.onChestUnlocked.RaiseEvent(this);


    public void SetSlot(ChestSlotController chestSlotController)
    {
        this.chestView.transform.SetParent((RectTransform)chestSlotController.View.transform);
        this.chestView.transform.position = chestSlotController.View.transform.position;
    }

    public void SetParent(RectTransform parent) => chestView.transform.SetParent(parent);

    public void FormatChestTimer(float seconds)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);
        string timerFormat;
        if (timeSpan.Hours > 0)
        {
            if(timeSpan.Minutes > 0)
              timerFormat = $"{timeSpan.Hours}h:{timeSpan.Minutes}m";
            else
              timerFormat = $"{timeSpan.Hours}h";
        }
        else if (timeSpan.Minutes > 0)
        {
            if(timeSpan.Seconds > 0)
               timerFormat =  $"{timeSpan.Minutes}m:{timeSpan.Seconds}s";
            else
               timerFormat = $"{timeSpan.Minutes}m";
        }
        else
        {
            timerFormat =  $"{timeSpan.Seconds}s";
        }
        chestView.UpdateChestTimerText(timerFormat);
    }

    public int GetOpenNowCost()
    {
        float unlockCost = Mathf.Ceil((this.chestData.currentTime / 60) / 10);
        return (int)unlockCost;
    }

    public void Destroy() => UnityEngine.Object.Destroy(this.chestView.gameObject);

    public void RemoveListeners() => this.chestView.RemoveListeners();
}
