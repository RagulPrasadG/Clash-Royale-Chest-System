using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSlotController
{
    private ChestSlotView chestSlotView;
    private ChestController chest;
    public bool isSlotEmpty;

    public ChestSlotView View => chestSlotView;

    public ChestSlotController(ChestSlotView chestViewPrefab)
    {
        this.chestSlotView = Object.Instantiate(chestViewPrefab);
        this.chestSlotView.SetController(this);
        this.isSlotEmpty = true;
    }

    public void AddChest(ChestController chestController)
    {
        if (!isSlotEmpty)
            return;

        this.chest = chestController;
        isSlotEmpty = false;
    }

    public void SetParent(RectTransform parent)
    {
        chestSlotView.transform.SetParent(parent);
          
    }

    public void RemoveChest()
    {
        this.chest.Destroy();
        chest = null;
    }
}
