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
            if (currentHoverObject != null && Input.GetMouseButtonDown(0) && !isCardSelect) //마우스에 위치한 카드를 클릭했을 때 카드가 선택되게 함
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
            else if (Input.GetMouseButtonDown(0) && isCardSelect && !mouseHoverSelectCard) //카드가 하나 선택됐을때, 선택된 카드 외의 장소를 클릭했을 때 선택 취소
            {
                isCardSelect = false;
                determineButton.SetActive(false);
                cardManager.ResetCardPosition();
            }
        }
        
    }

    void CardSelect() //아무 카드도 선택되지 않았을 때
    {
        Ray ray = maincamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Transform hitTransform = hit.transform;
            GameObject hitObject = hit.collider.gameObject;
            if (hitTransform != null && hitTransform != currentHoveredObjectTrnasform && hitObject.tag == "Card") // 마우스가 위치한 카드 등록
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


    void CardSelected() //카드가 하나 선택되어 있을 때
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
            else if (Input.GetMouseButtonDown(0) && hitTransform != null && hitObject.tag == "NormalAttack")//노말공격 버튼을 클릭했을때
            {
                mouseHoverSelectCard = true;
                determineButton.SetActive(true);// 결정버튼 활성화
                selectAttackType = AttackType.normal;
            }
            else if (Input.GetMouseButtonDown(0) && hitTransform != null && hitObject.tag == "ChargeAttack") //차지공격 버튼을 클릭했을때
            {
                mouseHoverSelectCard = true;
                determineButton.SetActive(true);
                selectAttackType = AttackType.charge;
            }
            else if (Input.GetMouseButtonDown(0) && hitTransform != null && hitObject.tag == "CounterAttack")  //카운터공격 버튼을 클릭했을때
            {
                mouseHoverSelectCard = true;
                determineButton.SetActive(true);
                selectAttackType = AttackType.counter;
            }
            else if (Input.GetMouseButtonDown(0) && hitTransform != null && hitObject.tag == "DetermineButton") //결정버튼 클릭
            {
                mouseHoverSelectCard = true;
                SelectAttack();
            }
            else if (Input.GetMouseButtonDown(0) && hitTransform != null && hitObject.tag == "Card") //선택되지 않은 카드 클릭
            {
                mouseHoverSelectCard = true;
                determineButton.SetActive(false); //결정버튼 비활성화
                selectCard.gameObject.tag = "Card"; //선택한 카드 취소
                CardController card = hitObject.GetComponent<CardController>();
                selectCard = card.card;
                card.gameObject.tag = "SelectCard"; //클릭한 카드를 선택한 카드로 만듬
                card.gameObject.transform.localScale = originalScale;
                int cardPosition = card.CardPosition;
                isCardSelect = true;
                cardManager.SelectCard(cardPosition); //카드 위치 변경
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

    void SelectAttack() //결정버튼 클릭시 공격타입 전달 함수
    {
        GameManager.GetPlayerAttackType(selectAttackType);
        cardManager.GetSelectCard(selectCard, selectAttackType);
        selectAttackType = AttackType.None;
        selectCard = null;
        determineButton.SetActive(false);
        isCardSelect = false;
    }

    
}
