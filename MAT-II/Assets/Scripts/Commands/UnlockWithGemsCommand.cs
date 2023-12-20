using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockWithGemsCommand : ICommand
{
    private ChestController chestController;
    private ChestService chestService;

    public UnlockWithGemsCommand(ChestController chestController, ChestService chestService)
    {
        this.chestController = chestController;
        this.chestService = chestService;   
    }

    public void Execute() => chestService.OpenWithGems();
    public void Undo()
    {
        chestService.UndoOpenWithGems(chestController);
    }

}
