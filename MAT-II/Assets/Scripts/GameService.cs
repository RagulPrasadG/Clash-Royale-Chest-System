using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameService : MonoBehaviour
{
    [SerializeField] UIService uIService;
    [SerializeField] ChestService chestService;
    public EventService eventService;
    public CommandInvoker commandInvoker;

    [SerializeField] private int coins = 100;
    [SerializeField] private int gems = 100;

    public int Coins { 
        get
        {
            return coins;
        }
        private set 
        {
            coins = value;
            uIService.UpdateCurrencyUI();
        }
    }
    public int Gems
    {
        get
        {
            return gems;
        }
        private set
        {
            gems = value;
            uIService.UpdateCurrencyUI();
        }
    }

    public void Awake()
    {
        eventService = new EventService();
        uIService.Init(eventService,this);
        chestService.Init(eventService,uIService,this);
        commandInvoker = new CommandInvoker();
        
    }

    private void Start()
    {
        uIService.UpdateCurrencyUI();    
    }

    public void AddCoinAmount(int amount)
    {
        Coins += amount;
    }

    public void ReduceCoinAmount(int amount)
    {
        Coins -= amount;
    }

    public void AddGemAmount(int amount)
    {
        Gems += amount;
    }

    public void ReduceGemAmount(int amount)
    {
        Gems -= amount;
    }

    public void Undo() => commandInvoker.Undo();
}
