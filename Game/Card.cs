using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum AttackType
{
    None,
    normal,
    charge,
    counter,
    Special
}

public class Card : MonoBehaviour
{
    public CardSO cardSO;

    public bool normalAttackOn;
    public bool ChargeAttackOn;
    public bool CounterAttackOn;
    public bool canSpecialAttack;

    public void RemoveAttackType(AttackType attackType) //사용한 공격 타입을 제거
    {
        if (attackType == AttackType.normal)
        {
            normalAttackOn = false;
        }
        else if (attackType == AttackType.charge)
        {
            ChargeAttackOn = false;
        }
        else if (attackType == AttackType.counter)
        {
            CounterAttackOn = false;
        }
        CheckCanSpecialAttack();
    }

    void CheckCanSpecialAttack() //모든 공격타입을 사용했을 때 스페셜어택을 사용가능하게 함
    {
        if (!normalAttackOn && !ChargeAttackOn && !CounterAttackOn)
        {
            canSpecialAttack = true;
        }
    }
}
