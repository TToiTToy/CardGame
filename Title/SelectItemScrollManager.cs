using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectItemScrollManager : MonoBehaviour
{
    public GameObject ItemUIObject;
    public GameObject scrollViewContent;
    public SelectItemManager SelectItemManager;
    

    void Start()
    {
        
    }

    // ∏  º±≈√ √≥∏Æ
   

    public void MakeView(StageSettingSO stageSettingSO)
    {
        for(int i = 0; i < stageSettingSO.items.Count; i++)
        {
            StageSettingItem item = stageSettingSO.items[i];
            GameObject ItemUI = Instantiate(ItemUIObject);
            ItemUI.transform.SetParent(scrollViewContent.transform);
            SelectableItem selectableItem = ItemUI.GetComponent<SelectableItem>();
            selectableItem.SetIndex(i);
            selectableItem.selectItemManager = SelectItemManager;
            Image[] UIimages = ItemUI.GetComponentsInChildren<Image>();
            UIimages[UIimages.Length - 1].color = item.itemImage.color;
            UIimages[UIimages.Length-1].sprite = item.itemImage.sprite;
            TextMeshProUGUI itemText = ItemUI.GetComponentInChildren<TextMeshProUGUI>();
            itemText.SetText(item.itemName);

        }
    }


}



