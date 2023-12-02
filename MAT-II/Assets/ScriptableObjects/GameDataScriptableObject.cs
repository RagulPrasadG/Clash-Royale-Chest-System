using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameData",menuName = "Data/NewGameData")]
public class GameDataScriptableObject : ScriptableObject
{

    [Header("UI")]
    public List<ChestDataScriptableObject> chestDataList;
    public ChestDataScriptableObject GetRandomChestData()
    {
        int randomIndex = Random.Range(0, chestDataList.Count);
        return chestDataList[randomIndex];
    }
}
