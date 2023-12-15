using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class RewardsPanel : PopUp
{
    [SerializeField] TMP_Text coinText;
    [SerializeField] TMP_Text gemText;
    [SerializeField] Button closeButton;

    private void Awake()
    {
        closeButton.onClick.AddListener(Hide);
    }

    public void Init(List<RewardData> rewards)
    {
        foreach(var reward in rewards)
        {
            switch (reward.rewardType)
            {
                case RewardType.COINS:
                    int coinReward = Random.Range(reward.minimumAmount, reward.maximumAmount);
                    coinText.text = coinReward.ToString();
                    break;
                case RewardType.GEMS:
                    int gemReward = Random.Range(reward.minimumAmount, reward.maximumAmount);
                    gemText.text = gemReward.ToString();
                    break;
            }     
        }
    }
}
