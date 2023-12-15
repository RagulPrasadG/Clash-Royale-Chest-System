using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventService
{
    public EventController onAddChestButtonClicked;
    public EventController<ChestController> onChestButtonClicked;
    public EventController onClickUnlockWithTimer;
    public EventController onClickUnlockWithGems;

    public EventService()
    {
        onAddChestButtonClicked = new EventController();
        onChestButtonClicked = new EventController<ChestController>();
        onClickUnlockWithTimer = new EventController();
        onClickUnlockWithGems = new EventController();
    }
}

public class EventController
{
    public Action baseEvent;

    public void AddListener(Action listener)
    {
        baseEvent += listener;
    }
    public void RaiseEvent()
    {
        baseEvent?.Invoke();
    }

    public void RemoveListener(Action listener)
    {
        baseEvent -= listener;
    }

}

public class EventController<T>
{
    public Action<T> baseEvent;

    public void AddListener(Action<T> listener)
    {
        baseEvent += listener;
    }
    public void RaiseEvent(T param)
    {
        baseEvent?.Invoke(param);
    }

    public void RemoveListener(Action<T> listener)
    {
        baseEvent -= listener;
    }

}