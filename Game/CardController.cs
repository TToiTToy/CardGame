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
    public void InDicateCardName()//카드 이름, 이미지 설정 함수
    {
        CardNameText.SetText(card.cardSO.cards.Cardname);
        Renderer renderer = characterImage.GetComponent<Renderer>();
        renderer.material = card.cardSO.cards.inGameCardImage;
    }





    public void RemoveAttackButton(AttackType attackType) //사용한 공격타입 버튼 제거 함수
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

    public void CharacterOn(Transform characterPosition) //배틀시 캐릭터 활성화 함수
    {
        character.SetActive(true);
        character.transform.position = characterPosition.position;
        character.transform.rotation = characterPosition.rotation;
    }

    public void CharacterOff() //캐릭터 비활성화 함수
    {
        character.SetActive(false);
    }

    public void CharaterAction(AttackType attackType) //공격 타입에 따른 캐릭터 애니메이션 실행 함수
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
