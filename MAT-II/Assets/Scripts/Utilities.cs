
public enum ChestStatus
{
    LOCKED,
    UNLOCKING,
    UNLOCKED,
    COLLECTED
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


public struct ChestData
{
    public ChestDataScriptableObject chestDataSO;
    public ChestStatus chestStatus;
    public float currentTimer;
}
public enum PopupType
{
    INFO,
    CHESTOPENCONFIRMATION
}
