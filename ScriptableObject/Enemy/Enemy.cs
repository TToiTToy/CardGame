using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private EnemyCardManager enemyCardManager;

    private Card selectCard;
    private AttackType selectAttackType;

    public void ChooseCard() //적 카드 선택 함수
    {
        int i = Random.Range(0, enemyCardManager.EnemyCardList.Count);
        selectCard = enemyCardManager.EnemyCardList[i];
        if (selectCard.canSpecialAttack)
        {
            selectAttackType = AttackType.Special;
        }
        else
        {
            if(selectCard.normalAttackOn)
            {
                selectAttackType = AttackType.normal;
            }
            else if (selectCard.ChargeAttackOn)
            {
                selectAttackType = AttackType.charge;
            }
            else if (selectCard.CounterAttackOn)
            {
                selectAttackType = AttackType.counter;
            }

        }
        enemyCardManager.GetSelectCardAndAttackType(selectCard, selectAttackType);
    }

    public void SetEnemyManager(EnemyCardManager enemycardmanager)
    {
        enemyCardManager = enemycardmanager;
    }




}
