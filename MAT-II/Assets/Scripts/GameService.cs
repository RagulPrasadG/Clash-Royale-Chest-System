using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameService : MonoBehaviour
{
    [SerializeField] UIService uIService;
    [SerializeField] ChestService chestService;

    public EventService eventService;
    public void Awake()
    {
        eventService = new EventService();
        uIService.Init(eventService);
        chestService.Init(eventService,uIService);
      
        
    }

}
