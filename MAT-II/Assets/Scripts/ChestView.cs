using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ChestView : MonoBehaviour
{
    public Button openButton;
    public Button openNowButton;
    public TMP_Text chestTimerText;
    [SerializeField] Button chestButton;
    [SerializeField] TMP_Text chestStatusText;
    [SerializeField] TMP_Text openNowCostText;
    [SerializeField] Image chestImage;


    private ChestController chestController;

    private void Awake()
    {
        chestButton.onClick.AddListener(OnClickChest);
        openButton.onClick.AddListener(OnOpenChest);
        openNowButton.onClick.AddListener(OnOpenWithGems);
    }

    public void Update()
    {
        this.openNowCostText.text = chestController.GetOpenNowCost().ToString();
    }

    public void OnOpenWithGems() => chestController.OnUnlockWithGems();

    public void OnClickChest() => chestController.OnClickChest();

    public void OnOpenChest() => chestController.Open();

    public void SetController(ChestController chestController) => this.chestController = chestController;

    public void StartTimerCouroutine() => StartCoroutine(chestController.StartTimer());
    public void StopTimerCouroutine() => StopCoroutine(chestController.StartTimer());

    public void SetChestSprite(Sprite sprite) => chestImage.sprite = sprite;
    public void UpdateChestStatusText(string status) => chestStatusText.text = status;
    public void UpdateChestTimerText(string timer) => chestTimerText.text = timer;

    public void RemoveListeners()
    {
        chestButton.onClick.RemoveAllListeners();
    }
}
