using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardSlot : MonoBehaviour, IDropHandler
{
    public int slotIndex;
    public SelectItemManager selectItemManager;

    public void OnDrop(PointerEventData eventData) //ī�尡 ī�彽�� ���� �������� ��
    {
        if(RectTransformUtility.RectangleContainsScreenPoint(GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera) == true)
        {
            SelectableCard selectableCard = eventData.pointerDrag.GetComponent<SelectableCard>(); //���þȵǾ� �ִ� ī�尡 ī�� ���Կ� �������� ��
            if (selectableCard != null) 
            {
                int cardIndex = selectableCard.index;
                selectItemManager.DropNonSelectCard(slotIndex, cardIndex); //�ش� ī�带 ī�� ���Կ� ���
            }
            SelectedCard selectedCard = eventData.pointerDrag.GetComponent<SelectedCard>(); //���õǾ� �ִ� ī�尡 ī�� ���Կ� �������� ��
            if (selectedCard != null)
            {
                int nowIndex = selectedCard.index;
                selectItemManager.DropSelectedCardToSlot(nowIndex, slotIndex); // ī�� ���� ��ġ�� ���õ��ִ� ī�� ��ġ ����
            }
        }

    }
}
