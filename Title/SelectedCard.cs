using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectedCard : SelectableItem, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public RectTransform startPosition;
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        //startPosition = rectTransform.position;
        canvas = GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        rectTransform.position = startPosition.position;
        canvasGroup.blocksRaycasts = true;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
           // startPosition = rectTransform.position;
            canvas.overrideSorting = true;
            canvas.sortingOrder = 1;
            canvasGroup.blocksRaycasts = false;
        }


    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            rectTransform.position = Input.mousePosition;
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            rectTransform.position = startPosition.position;
            canvas.sortingOrder = 0;
            canvas.overrideSorting = false;
            canvasGroup.blocksRaycasts = true;
        }



    }

    public void OnDrop(PointerEventData eventData)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera) == true)
        {
            SelectableCard selectableCard = eventData.pointerDrag.GetComponent<SelectableCard>();
            if (selectableCard != null)
            {
                int cardIndex = selectableCard.index;
                selectItemManager.DropNonSelectCard(index, cardIndex);
            }
            SelectedCard selectedCard = eventData.pointerDrag.GetComponent<SelectedCard>();
            if (selectedCard != null)
            {
                int nowIndex = selectedCard.index;
                selectItemManager.DropSelectedCard(nowIndex, index);
            }
        }

    }

    public override void SelectItem()
    {
        selectItemManager.SelectedCardClick(index);
    }
}
