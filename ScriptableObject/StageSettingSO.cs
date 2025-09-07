using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class StageSettingItem
{
    public string itemName;
    public Image itemImage;
    public GameObject itemObject;
}


[CreateAssetMenu(fileName = "StageSettingItemSO", menuName = "Scriptable Object/StageSettingItemSO")]
public class StageSettingSO : ScriptableObject
{
    public List<StageSettingItem> items;
}
