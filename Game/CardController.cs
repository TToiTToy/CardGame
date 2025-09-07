using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public int CardPosition;


    public GameObject normalAttackButton;
    public GameObject ChargeAttackButton;
    public GameObject CounterAttackButton;

    public TextMeshPro CardNameText;
    public GameObject characterImage;

    public Card card;
    private Animator anim;

    private GameObject character;
    private void Awake()
    {
        card = GetComponent<Card>();
      

    }

    private void Start()
    {
        character = Instantiate(card.cardSO.cards.Character);
        anim = character.GetComponent<Animator>();
        character.SetActive(false);
    }
    public void InDicateCardName()//ī�� �̸�, �̹��� ���� �Լ�
    {
        CardNameText.SetText(card.cardSO.cards.Cardname);
        Renderer renderer = characterImage.GetComponent<Renderer>();
        renderer.material = card.cardSO.cards.inGameCardImage;
    }





    public void RemoveAttackButton(AttackType attackType) //����� ����Ÿ�� ��ư ���� �Լ�
    {
        if (attackType == AttackType.normal)
        {
            normalAttackButton.SetActive(false);
        }
        else if (attackType == AttackType.charge)
        {
            ChargeAttackButton.SetActive(false);
        }
        else if (attackType == AttackType.counter)
        {
            CounterAttackButton.SetActive(false);
        }
        card.RemoveAttackType(attackType);
    }

    public void CharacterOn(Transform characterPosition) //��Ʋ�� ĳ���� Ȱ��ȭ �Լ�
    {
        character.SetActive(true);
        character.transform.position = characterPosition.position;
        character.transform.rotation = characterPosition.rotation;
    }

    public void CharacterOff() //ĳ���� ��Ȱ��ȭ �Լ�
    {
        character.SetActive(false);
    }

    public void CharaterAction(AttackType attackType) //���� Ÿ�Կ� ���� ĳ���� �ִϸ��̼� ���� �Լ�
    {
        character.SetActive(true);
        if(attackType == AttackType.None)
        {
            anim.SetTrigger("isNone");
        }
        else if(attackType == AttackType.normal)
        {
            anim.SetTrigger("isNormalAttack");

        }
        else if(attackType == AttackType.charge)
        {
            anim.SetTrigger("isChargeAttack");
        }
        else if(attackType == AttackType.counter)
        {
            anim.SetTrigger("isCounterAttack");
        }
        else
        {
            anim.SetTrigger("isSpecialAttack");
        }
    }


}
