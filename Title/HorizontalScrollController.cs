using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HorizontalScrollController : MonoBehaviour
{
    public ScrollRect scrollRect;
    public float scrollSpeed = 0.1f; 

    void Update()
    {
        // ���콺 �巡�׿� ���� ��ũ�� �̵�
        if (Input.GetMouseButton(0))
        {
            float scrollInput = Input.GetAxis("Mouse X");
            scrollRect.horizontalNormalizedPosition -= scrollInput * scrollSpeed * Time.deltaTime;
        }
    }


}
