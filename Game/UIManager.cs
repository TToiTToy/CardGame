using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;

    public CanvasGroup baseUIGroup;
    public CanvasGroup RoundGroup;
    public CanvasGroup battleUIGroup;
    public CanvasGroup newGameGroup;

    public TextMeshProUGUI playerScore;
    public TextMeshProUGUI Enemyscore;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI ResultText;
    public TextMeshProUGUI roundText;
    public TextMeshProUGUI battleText;
    public TextMeshProUGUI roundResultText;
    private int remainTime;
    private void Update()
    {
        playerScore.SetText(gameManager.playerScore.ToString());
        Enemyscore.SetText(gameManager.enemyScore.ToString());
        int remianTime = (int)gameManager.cardSelectTime;
        timerText.SetText(remianTime.ToString());
    }

    public void RoundUIStart(int round) //라운드 시작시 라운드 UI 활성화
    {
        Time.timeScale = 0;
        UIGroupOff(baseUIGroup);
        UIGroupOn(RoundGroup);
        roundText.SetText("Round " +round.ToString());
    }

    public void RoundUIEnd() // 라운드 UI 비활성화
    {
        Time.timeScale = 1;
        UIGroupOff(RoundGroup);
        UIGroupOn(baseUIGroup);

    }

    public void RoundUIOff()
    {
        UIGroupOff(baseUIGroup);
    }

    public void ResultOn(Result result) //게임 결과 UI 활성화
    {
        Time.timeScale = 0;
        ResultText.gameObject.SetActive(true);
        if(result == Result.WIn)
        {
            ResultText.SetText("You Win");
        }
        else if(result == Result.Lose)
        {
            ResultText.SetText("You Lose");
        }
        else if(result == Result.Draw)
        {
            ResultText.SetText("Draw");
        }
        UIGroupOn(newGameGroup); //새게임 UI활성화
    }


    public void BattleUIGoupOn(playerType playerType, AttackType attackType) // 배틀UI활성화
    {
        UIGroupOn(battleUIGroup); //배틀UI 활성화
        roundResultText.SetText("");
        if (playerType == playerType.Blue) //적의 공격시
        {
            battleText.color = new Color32(0, 0, 255, 255); 
            string playerName = "상대플레이어는 ";
            string action = SetBattleText(attackType); //공격타입 텍스트 활성화
            battleText.SetText(playerName + action);
        }
        else //플레이어의 공격시
        {
            battleText.color = new Color32(255, 0, 0, 255);
            string playerName = "플레이어는 ";
            string action = SetBattleText(attackType);
            battleText.SetText(playerName + action);
        }

    }

    public void BattleUIOff() // 배틀UI 비활성화
    {
        UIGroupOff(battleUIGroup);
    }

    public void SetRoundResultText(Result result)
    {
        if(result == Result.WIn)
        {
            roundResultText.SetText("승리");
        }
        else if(result == Result.Lose)
        {
            roundResultText.SetText("패배");
        }
        else
        {
            roundResultText.SetText("무승부");
        }

    }

    public void NewGameYesBtnClick() //새게임 버튼
    {
        SceneManager.LoadScene("VsCPUScene");
    }

    public void NewGameNoBtnClick() //메인화면으로 버튼
    {
        SceneManager.LoadScene("StartScene");
    }

    string SetBattleText(AttackType attackType) //공격 타입 텍스트 변경
    {
        if(attackType == AttackType.None)
        {
            return "아무것도 하지 않았다";
        }
        else if(attackType == AttackType.normal)
        {
            return "빠르게 적을 공격했다";
        }
        else if(attackType == AttackType.charge)
        {
            return "강력한 공격을 준비하고 있다";
        }
        else if(attackType == AttackType.counter)
        {
            return "카운터를 준비하고 있다";
        }
        else if(attackType == AttackType.Special)
        {
            return "필살기를 사용했다";
        }
        return null;
    }

    void UIGroupOn(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    void UIGroupOff(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
