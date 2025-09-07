using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectableItem : MonoBehaviour, IPointerClickHandler
{

    public int index;
    public SelectItemManager selectItemManager;



    public void OnPointerClick(PointerEventData eventData)
    {

        if (eventData.button == PointerEventData.InputButton.Right) //오브젝트 우클릭시 SelectItem호출
        {
            SelectItem();
        }
    }


    public virtual void SelectItem()
    {
        selectItemManager.ChangeItem(index);
    }

    public void SetIndex(int i)
    {
        index = i;
    }


}
