using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewChestData",menuName = "Data/NewChestData")]
public class ChestDataScriptableObject : ScriptableObject
{
    public ChestType chestType;
    public Sprite chestSprite;
    public float unlockTime;
    public List<RewardData> rewardData;
    
}
