using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<CardSO> myCardSOList;
    public GameObject cardObject;
    public List<CardController> myCard;
    public Transform SelectCardTransform;
    public List<Transform> nonSelectCardTransform;

    public List<Transform> OriginalCardPosition;
    public Transform playerCharacterPosition;

    private Card selectCard;
    private AttackType selectAttackType;
    private void Awake()
    {
        myCard = new List<CardController>();

        
    }

    private void Start()
    {
        CreatCard();
    }

    void CreatCard() //카드 오브젝트 생성 함수
    {
        for (int i = 0; i < myCardSOList.Count; i++)
        {
            GameObject newCard = Instantiate(cardObject);
            newCard.transform.position = OriginalCardPosition[i].position;
            CardController newCardController = newCard.GetComponent<CardController>();
            newCardController.CardPosition = i;
            newCardController.card.cardSO = myCardSOList[i];
            newCardController.InDicateCardName();
            myCard.Add(newCardController);
        }
    }
    
    public void ActiveFalseCard() //카드 오브젝트 비활성화 함수
    {
        for(int i = 0;i < myCard.Count;i++)
        {
            myCard[i].gameObject.SetActive(false);
        }
    }

    public void ActiveTrueCard() //카드 오브젝트 활성화 함수
    {
        for (int i = 0; i < myCard.Count; i++)
        {
            myCard[i].gameObject.SetActive(true);
        }
    }

    public void SelectCard(int cardPosition) //카드 선택시 카드 위치 변경 함수
    {
        
        int nonSelectCard = 0;
        for (int i = 0; i < myCard.Count; i++)
        {
            if (myCard[i].CardPosition == cardPosition)
            {
                myCard[i].gameObject.transform.position = SelectCardTransform.position;
            }
            else
            {
                myCard[i].gameObject.transform.position = nonSelectCardTransform[nonSelectCard].position;
                nonSelectCard++;
            }
        }
    }

    public void RandomSelectCard() //시간초과시 카드 랜덤 선택 함수
    {
        int randomIndex = Random.Range(0, myCard.Count);
        Debug.Log(randomIndex);
        selectCard = myCard[randomIndex].card;
    }

    public void ResetCardPosition() //카드 원위치 함수
    {
        for (int i = 0;i < myCard.Count;i++)
        {

            myCard[i].gameObject.transform.position = OriginalCardPosition[i].position;
            myCard[i].gameObject.tag = "Card";
            
        }
        
    }

    

    public void GetSelectCard(Card card, AttackType attackType) //선택한 카드와 공격타입 받는 함수
    {
        selectCard = card;
        selectAttackType = attackType;
    }

    public void PlayerWinorDraw() //플레이어가 라운드에서 이기거나 비겼을때 사용한 공격타입 제거 함수
    {
        CardController cardController = selectCard.GetComponent<CardController>();
        cardController.RemoveAttackButton(selectAttackType);
        ResetCardPosition();
    }

    public void PlayerLose() //플레이어가 라운드에서 졌을 때 카드 제거 함수
    {
        RemoveCard();
        ResetCardPosition();
    }
    void RemoveCard() //패배한 카드 제거
    {
        selectCard.gameObject.SetActive(false);
        CardController cardController = selectCard.GetComponent<CardController>();
        myCard.Remove(cardController);

    }

    public void BattleAction(AttackType attackType)
    {
        CardController cardController = selectCard.GetComponent<CardController>();
        cardController.CharaterAction(selectAttackType);

    }

    public void CharacterOn()
    {
        CardController cardController = selectCard.GetComponent<CardController>();
        cardController.CharacterOn(playerCharacterPosition);
    }

    public void CharacterOff() 
    {
        CardController cardController = selectCard.GetComponent<CardController>();
        cardController.CharacterOff();
    }

    public void ResetSelectCard()
    {
        selectAttackType = AttackType.None;
        selectCard = null;
    }
}
