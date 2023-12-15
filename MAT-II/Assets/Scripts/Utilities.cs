
using System.Collections.Generic;

public enum ChestStatus
{
    LOCKED,
    UNLOCKING,
    UNLOCKED,
    COLLECTED,
    QUEUED
}
public enum ChestType
{
    COMMON,
    RARE,
    EPIC,
    LEGENDARY
}
public enum RewardType
{
    COINS,
    GEMS
}

[System.Serializable]
public struct RewardData
{
    public UnityEngine.Sprite rewardSprite;
    public RewardType rewardType;
    public int minimumAmount;
    public int maximumAmount;
}

[System.Serializable]
public struct Rewards
{
    public List<RewardData> rewards;
}


public struct ChestData
{
    public ChestDataScriptableObject chestDataSO;
    public ChestStatus chestStatus;
    public float currentTime;
}

public enum PopupType
{
    INFO,
    CHESTOPENCONFIRMATION,
    REWARDSPANEL
}
