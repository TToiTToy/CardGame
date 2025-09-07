using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectableCard : SelectableItem, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public override void SelectItem()
    {
        selectItemManager.ChangeCard(index);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            startPosition = rectTransform.position;
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
            rectTransform.position = startPosition;
            canvas.sortingOrder = 0;
            canvas.overrideSorting = false;
            canvasGroup.blocksRaycasts = true;
        }



    }
}
