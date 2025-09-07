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

    void CreatCard() //ī�� ������Ʈ ���� �Լ�
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
    
    public void ActiveFalseCard() //ī�� ������Ʈ ��Ȱ��ȭ �Լ�
    {
        for(int i = 0;i < myCard.Count;i++)
        {
            myCard[i].gameObject.SetActive(false);
        }
    }

    public void ActiveTrueCard() //ī�� ������Ʈ Ȱ��ȭ �Լ�
    {
        for (int i = 0; i < myCard.Count; i++)
        {
            myCard[i].gameObject.SetActive(true);
        }
    }

    public void SelectCard(int cardPosition) //ī�� ���ý� ī�� ��ġ ���� �Լ�
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

    public void RandomSelectCard() //�ð��ʰ��� ī�� ���� ���� �Լ�
    {
        int randomIndex = Random.Range(0, myCard.Count);
        Debug.Log(randomIndex);
        selectCard = myCard[randomIndex].card;
    }

    public void ResetCardPosition() //ī�� ����ġ �Լ�
    {
        for (int i = 0;i < myCard.Count;i++)
        {

            myCard[i].gameObject.transform.position = OriginalCardPosition[i].position;
            myCard[i].gameObject.tag = "Card";
            
        }
        
    }

    

    public void GetSelectCard(Card card, AttackType attackType) //������ ī��� ����Ÿ�� �޴� �Լ�
    {
        selectCard = card;
        selectAttackType = attackType;
    }

    public void PlayerWinorDraw() //�÷��̾ ���忡�� �̱�ų� ������� ����� ����Ÿ�� ���� �Լ�
    {
        CardController cardController = selectCard.GetComponent<CardController>();
        cardController.RemoveAttackButton(selectAttackType);
        ResetCardPosition();
    }

    public void PlayerLose() //�÷��̾ ���忡�� ���� �� ī�� ���� �Լ�
    {
        RemoveCard();
        ResetCardPosition();
    }
    void RemoveCard() //�й��� ī�� ����
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
