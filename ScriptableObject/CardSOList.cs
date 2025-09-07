using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CardSOList", menuName = "Scriptable Object/CardSOList")]
public class CardSOList : ScriptableObject
{
    public List<CardSO> list;
}
