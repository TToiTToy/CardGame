using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using UnityEngineInternal;

public class CardOnMouse : MonoBehaviour
{
    public GameManager GameManager;
    public Camera maincamera;
    public CardManager cardManager;
    public GameObject determineButton;

    Vector3 originalScale;
    float scaleFactor = 1.3f;
    Transform currentHoveredObjectTrnasform;
    GameObject currentHoverObject;
    bool isCardSelect;
    bool mouseHoverSelectCard;
    Card selectCard;
    AttackType selectAttackType;

    private void Awake()
    {
        cardManager = GetComponent<CardManager>();
    }

    private void Update()
    {
        if(Time.timeScale != 0)
        {
            if (!isCardSelect)
            {
                CardSelect();
            }
            else
            {
                // SelectAttackType();
                CardSelected();
            }
            if (currentHoverObject != null && Input.GetMouseButtonDown(0) && !isCardSelect) //���콺�� ��ġ�� ī�带 Ŭ������ �� ī�尡 ���õǰ� ��
            {
                CardController card = currentHoverObject.GetComponent<CardController>();
                selectCard = card.card;
                card.gameObject.tag = "SelectCard";
                card.gameObject.transform.localScale = originalScale;
                int cardPosition = card.CardPosition;
                isCardSelect = true;
                cardManager.SelectCard(cardPosition);
                if (selectCard.canSpecialAttack)
                {
                    determineButton.SetActive(true);
                    selectAttackType = AttackType.Special;
                }
            }
            else if (Input.GetMouseButtonDown(0) && isCardSelect && mouseHoverSelectCard)
            {

            }
            else if (Input.GetMouseButtonDown(0) && isCardSelect && !mouseHoverSelectCard) //ī�尡 �ϳ� ���õ�����, ���õ� ī�� ���� ��Ҹ� Ŭ������ �� ���� ���
            {
                isCardSelect = false;
                determineButton.SetActive(false);
                cardManager.ResetCardPosition();
            }
        }
        
    }

    void CardSelect() //�ƹ� ī�嵵 ���õ��� �ʾ��� ��
    {
        Ray ray = maincamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Transform hitTransform = hit.transform;
            GameObject hitObject = hit.collider.gameObject;
            if (hitTransform != null && hitTransform != currentHoveredObjectTrnasform && hitObject.tag == "Card") // ���콺�� ��ġ�� ī�� ���
            {
                if (currentHoveredObjectTrnasform != null)
                {
                    currentHoveredObjectTrnasform.localScale = originalScale;
                    originalScale = Vector3.zero;
                }
                currentHoveredObjectTrnasform = hitTransform;
                currentHoverObject = hitObject;
                if (originalScale == Vector3.zero)
                {
                    originalScale = currentHoveredObjectTrnasform.localScale;
                }

                currentHoveredObjectTrnasform.localScale = originalScale * scaleFactor;

            }
        }
        else
        {
            if (currentHoveredObjectTrnasform != null)
            {
                currentHoveredObjectTrnasform.localScale = originalScale;
                originalScale = Vector3.zero;
                currentHoveredObjectTrnasform = null;
                currentHoverObject = null;
            }
        }
    }


    void CardSelected() //ī�尡 �ϳ� ���õǾ� ���� ��
    {
        Ray ray = maincamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Transform hitTransform = hit.transform;
            GameObject hitObject = hit.collider.gameObject;
            if (hitTransform != null && hitObject.tag == "SelectCard")
            {
                mouseHoverSelectCard = true;
            }
            else if (Input.GetMouseButtonDown(0) && hitTransform != null && hitObject.tag == "NormalAttack")//�븻���� ��ư�� Ŭ��������
            {
                mouseHoverSelectCard = true;
                determineButton.SetActive(true);// ������ư Ȱ��ȭ
                selectAttackType = AttackType.normal;
            }
            else if (Input.GetMouseButtonDown(0) && hitTransform != null && hitObject.tag == "ChargeAttack") //�������� ��ư�� Ŭ��������
            {
                mouseHoverSelectCard = true;
                determineButton.SetActive(true);
                selectAttackType = AttackType.charge;
            }
            else if (Input.GetMouseButtonDown(0) && hitTransform != null && hitObject.tag == "CounterAttack")  //ī���Ͱ��� ��ư�� Ŭ��������
            {
                mouseHoverSelectCard = true;
                determineButton.SetActive(true);
                selectAttackType = AttackType.counter;
            }
            else if (Input.GetMouseButtonDown(0) && hitTransform != null && hitObject.tag == "DetermineButton") //������ư Ŭ��
            {
                mouseHoverSelectCard = true;
                SelectAttack();
            }
            else if (Input.GetMouseButtonDown(0) && hitTransform != null && hitObject.tag == "Card") //���õ��� ���� ī�� Ŭ��
            {
                mouseHoverSelectCard = true;
                determineButton.SetActive(false); //������ư ��Ȱ��ȭ
                selectCard.gameObject.tag = "Card"; //������ ī�� ���
                CardController card = hitObject.GetComponent<CardController>();
                selectCard = card.card;
                card.gameObject.tag = "SelectCard"; //Ŭ���� ī�带 ������ ī��� ����
                card.gameObject.transform.localScale = originalScale;
                int cardPosition = card.CardPosition;
                isCardSelect = true;
                cardManager.SelectCard(cardPosition); //ī�� ��ġ ����
                if (selectCard.canSpecialAttack)
                {
                    determineButton.SetActive(true);
                    selectAttackType = AttackType.Special;
                }
            }
            else
            {
                mouseHoverSelectCard = false;
            }
        }
        else
        {
            mouseHoverSelectCard = false;
        }
        
    }

    void SelectAttack() //������ư Ŭ���� ����Ÿ�� ���� �Լ�
    {
        GameManager.GetPlayerAttackType(selectAttackType);
        cardManager.GetSelectCard(selectCard, selectAttackType);
        selectAttackType = AttackType.None;
        selectCard = null;
        determineButton.SetActive(false);
        isCardSelect = false;
    }

    
}
