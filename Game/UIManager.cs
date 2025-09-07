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

    public void RoundUIStart(int round) //���� ���۽� ���� UI Ȱ��ȭ
    {
        Time.timeScale = 0;
        UIGroupOff(baseUIGroup);
        UIGroupOn(RoundGroup);
        roundText.SetText("Round " +round.ToString());
    }

    public void RoundUIEnd() // ���� UI ��Ȱ��ȭ
    {
        Time.timeScale = 1;
        UIGroupOff(RoundGroup);
        UIGroupOn(baseUIGroup);

    }

    public void RoundUIOff()
    {
        UIGroupOff(baseUIGroup);
    }

    public void ResultOn(Result result) //���� ��� UI Ȱ��ȭ
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
        UIGroupOn(newGameGroup); //������ UIȰ��ȭ
    }


    public void BattleUIGoupOn(playerType playerType, AttackType attackType) // ��ƲUIȰ��ȭ
    {
        UIGroupOn(battleUIGroup); //��ƲUI Ȱ��ȭ
        roundResultText.SetText("");
        if (playerType == playerType.Blue) //���� ���ݽ�
        {
            battleText.color = new Color32(0, 0, 255, 255); 
            string playerName = "����÷��̾�� ";
            string action = SetBattleText(attackType); //����Ÿ�� �ؽ�Ʈ Ȱ��ȭ
            battleText.SetText(playerName + action);
        }
        else //�÷��̾��� ���ݽ�
        {
            battleText.color = new Color32(255, 0, 0, 255);
            string playerName = "�÷��̾�� ";
            string action = SetBattleText(attackType);
            battleText.SetText(playerName + action);
        }

    }

    public void BattleUIOff() // ��ƲUI ��Ȱ��ȭ
    {
        UIGroupOff(battleUIGroup);
    }

    public void SetRoundResultText(Result result)
    {
        if(result == Result.WIn)
        {
            roundResultText.SetText("�¸�");
        }
        else if(result == Result.Lose)
        {
            roundResultText.SetText("�й�");
        }
        else
        {
            roundResultText.SetText("���º�");
        }

    }

    public void NewGameYesBtnClick() //������ ��ư
    {
        SceneManager.LoadScene("VsCPUScene");
    }

    public void NewGameNoBtnClick() //����ȭ������ ��ư
    {
        SceneManager.LoadScene("StartScene");
    }

    string SetBattleText(AttackType attackType) //���� Ÿ�� �ؽ�Ʈ ����
    {
        if(attackType == AttackType.None)
        {
            return "�ƹ��͵� ���� �ʾҴ�";
        }
        else if(attackType == AttackType.normal)
        {
            return "������ ���� �����ߴ�";
        }
        else if(attackType == AttackType.charge)
        {
            return "������ ������ �غ��ϰ� �ִ�";
        }
        else if(attackType == AttackType.counter)
        {
            return "ī���͸� �غ��ϰ� �ִ�";
        }
        else if(attackType == AttackType.Special)
        {
            return "�ʻ�⸦ ����ߴ�";
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
