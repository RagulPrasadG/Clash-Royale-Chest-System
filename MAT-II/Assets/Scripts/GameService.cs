using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameService : MonoBehaviour
{
    [SerializeField] UIService uIService;
    [SerializeField] ChestService chestService;
    public EventService eventService;


    public int coinsAmount = 100;
    public int gemsAmount = 100;

    public void Awake()
    {
        eventService = new EventService();
        uIService.Init(eventService);
        chestService.Init(eventService,uIService,this);
        
        
    }

}
