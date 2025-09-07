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

    public void RemoveAttackType(AttackType attackType) //����� ���� Ÿ���� ����
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

    void CheckCanSpecialAttack() //��� ����Ÿ���� ������� �� ����Ⱦ����� ��밡���ϰ� ��
    {
        if (!normalAttackOn && !ChargeAttackOn && !CounterAttackOn)
        {
            canSpecialAttack = true;
        }
    }
}
