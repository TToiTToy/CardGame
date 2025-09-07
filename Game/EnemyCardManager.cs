using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCardManager : MonoBehaviour
{
    public List<CardSO> enemyCardSOList;
    public GameManager GameManager;
    public Enemy enemy;
    public GameObject EnemyCardObject;
    public List<Transform> EnemyCardPositions;

    public List<Card> EnemyCardList;
    public Transform EnemyCharacterPosition;
    private Card selectCard;
    private AttackType selectAttackType;

    private void Start()
    {
        EnemyCardList = new List<Card>();

        for(int i = 0; i < 5; i++)
        {
            GameObject newEnemyCard = Instantiate(EnemyCardObject);
            newEnemyCard.transform.position = EnemyCardPositions[i].position;
            Card newCard = newEnemyCard.GetComponent<Card>();
            newCard.cardSO = enemyCardSOList[i];
            EnemyCardList.Add(newCard);

        }
    }

    private void Update()
    {
        if(selectCard == null && EnemyCardList.Count != 0)
        {
            enemy.ChooseCard();
        }
    }

    public void ChangeCardBack(StageSettingItem enemyCardBack) //상대가 선택한 카드 뒷면으로 변경
    {
        EnemyCardObject = enemyCardBack.itemObject;
    }

    public void EnemyWinorDraw() //적이 라운드 승리 또는 비겼을 때
    {
        selectCard.RemoveAttackType(selectAttackType);


    }



    public void EnemyLose() // 적이 졌을 때
    {
        RemoveCard(selectCard);
        ReplaceCard();

    }

    public void ActiveFalseCard() //카드 오브젝트 비활성화
    {
        for (int i = 0; i < EnemyCardList.Count; i++)
        {
            EnemyCardList[i].gameObject.SetActive(false);
        }
    }

    public void ActiveTrueCard() //카드 오브젝트 활성화
    {
        for (int i = 0; i < EnemyCardList.Count; i++)
        {
            EnemyCardList[i].gameObject.SetActive(true);
        }
    }

    void ReplaceCard()
    {
        for (int i = 0; i < EnemyCardList.Count; i++)
        {
            EnemyCardList[i].transform.position = EnemyCardPositions[i].position;
            EnemyCardList[i].gameObject.SetActive(true);
        }
    }
    void RemoveCard(Card card) //카드목록에서 카드 제거
    {
        card.gameObject.SetActive(false);
        EnemyCardList.Remove(card);
    }

    void EnemyCard()
    {
        GameManager.GetEnemyAttackType(selectAttackType);
    }

    public void GetSelectCardAndAttackType(Card card, AttackType attackType)
    {
        selectCard = card;
        selectAttackType = attackType;
        EnemyCard();
    }

    public void RandomSelectCard()
    {
        int randomIndex = Random.Range(0, EnemyCardList.Count);
        selectCard = EnemyCardList[randomIndex];
    }

    public void BattleAction(AttackType attackType)
    {
        CardController cardController = selectCard.GetComponent<CardController>();
        cardController.CharaterAction(selectAttackType);

    }

    public void CharacterOn()
    {
        CardController cardController = selectCard.GetComponent<CardController>();
        cardController.CharacterOn(EnemyCharacterPosition);
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

    public void SetEnemy(Enemy selectenemy)
    {
        enemy = selectenemy;
        enemy.SetEnemyManager(this);
    }
}
