using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardSlot : MonoBehaviour, IDropHandler
{
    public int slotIndex;
    public SelectItemManager selectItemManager;

    public void OnDrop(PointerEventData eventData) //카드가 카드슬롯 위에 놓아졌을 때
    {
        if(RectTransformUtility.RectangleContainsScreenPoint(GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera) == true)
        {
            SelectableCard selectableCard = eventData.pointerDrag.GetComponent<SelectableCard>(); //선택안되어 있는 카드가 카드 슬롯에 놓아졌을 때
            if (selectableCard != null) 
            {
                int cardIndex = selectableCard.index;
                selectItemManager.DropNonSelectCard(slotIndex, cardIndex); //해당 카드를 카드 슬롯에 등록
            }
            SelectedCard selectedCard = eventData.pointerDrag.GetComponent<SelectedCard>(); //선택되어 있는 카드가 카드 슬롯에 놓아졌을 때
            if (selectedCard != null)
            {
                int nowIndex = selectedCard.index;
                selectItemManager.DropSelectedCardToSlot(nowIndex, slotIndex); // 카드 슬롯 위치로 선택되있는 카드 위치 변경
            }
        }

    }
}
