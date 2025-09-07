using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CardProperty
{
    public string Cardname;
    public Image CharacterImage;
    public Material inGameCardImage;
    public GameObject Character;

}



[CreateAssetMenu(fileName = "CardSO", menuName = "Scriptable Object/CardSO")]
public class CardSO : ScriptableObject
{
    public CardProperty cards;
}
