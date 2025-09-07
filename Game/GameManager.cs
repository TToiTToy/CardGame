using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Phase
{
    None,
    RoundStart,
    CardSelect,
    Battle,


}

public enum Result
{
    WIn,
    Draw,
    Lose
}

public enum playerType
{
    Blue,
    Red
}
public class GameManager : MonoBehaviour
{
    public Camera mainCamera;
    public Camera actionCamera;

    public AttackType playerAttackType;
    public AttackType enemyAttackType;

    public BackgroundManager backgroundManager;
    public UIManager uiManager;
    public CardManager cardManager;
    public EnemyCardManager enemyCardManager;

    public int playerScore;
    public int enemyScore;
    public float cardSelectTime;

    public Phase phase;
    int round = 1;
    int goalScore = 5;
    int highScore = 1;
    playerType leadPlayerType;
    Result roundResult;



    void Update()
    {
        if(phase == Phase.RoundStart)
        {
            RoundStart();
        }
        else if(phase == Phase.CardSelect)
        {
            cardSelectTime = cardSelectTime - Time.deltaTime;
            if(cardSelectTime > 0 )
            {
                if (playerAttackType != AttackType.None && enemyAttackType != AttackType.None)
                {
                    phase = Phase.None;
                    CheckWin();

                }
            }
            else
            {
                phase = Phase.None;
                CardRandomSelect();

            }
            
        }
        else if(phase == Phase.Battle)
        {
            Battle(roundResult);
        }

    }

    void RoundStart() //���� ���۽�
    {
        CheckHighScore(); // ������ Ȯ��
        DisappearObject(); //�ʿ���� ������Ʈ ��Ȱ��ȭ
        phase = Phase.None;
        uiManager.RoundUIStart(round); //���� ����UIȰ��ȭ
        StartCoroutine(RoundStartEnd()); 
    }

    IEnumerator RoundStartEnd()
    {
        cardSelectTime = 15f; //ī�� ���ýð� ����
        yield return new WaitForSecondsRealtime(1.5f);
        uiManager.RoundUIEnd(); //���� ����UI ��Ȱ��ȭ
        round++;
        Appearobject(); //��Ȱ��ȭ�� ������Ʈ Ȱ��ȭ
        phase = Phase.CardSelect;
    }

    void CheckHighScore() // ������ Ȯ���ؼ�, ���� ������ �Ǿ����� ���� ȯ�� ����
    {
        int score;
        if (playerScore >= enemyScore)
        {
            score = playerScore;
            leadPlayerType = playerType.Red;
        }
        else
        {
            score = enemyScore;
            leadPlayerType = playerType.Blue;
        }
        if(score > highScore)
        {
            highScore = score;
            if (highScore == 2)
            {
                backgroundManager.ChageStage(leadPlayerType);
            }
            else if (highScore == 3)
            {
                backgroundManager.AddBackground(leadPlayerType);
            }
            else if (highScore == 4)
            {
                backgroundManager.MusicOn(leadPlayerType);
            }
        }
        
    }

    void CardRandomSelect() //���ѽð� ���� ī�带 �������� �ʾ����� ī�带 ���� ����
    {
        phase = Phase.None;
        if(playerAttackType == AttackType.None)
        {
            cardManager.RandomSelectCard();
            PlayerRoundLose();
        }
        else if(enemyAttackType == AttackType.None)
        {
            enemyCardManager.RandomSelectCard();
            PlayerRoundWin();
        }
        else if(playerAttackType == AttackType.None && enemyAttackType == AttackType.None)
        {
            cardManager.RandomSelectCard();
            enemyCardManager.RandomSelectCard();
            goalScore--;
            RandomDraw();
        }
    }


    void CheckWin() //�¸� �Ǵ�
    {
        if (playerAttackType == enemyAttackType)
        {
            RoundDraw();

        }
        else if(playerAttackType == AttackType.Special)
        {
            PlayerGameWin();
        }
        else if(enemyAttackType == AttackType.Special)
        {
            PlayerGameLose();
        }
        else if ((playerAttackType == AttackType.normal && enemyAttackType == AttackType.charge) || (playerAttackType == AttackType.charge && enemyAttackType == AttackType.counter) || (playerAttackType == AttackType.counter && enemyAttackType == AttackType.normal))
        {
            PlayerRoundWin();

        }
        else
        {
            PlayerRoundLose();

        }
    }

    void PlayerGameWin() // �÷��̾ ������ �̰��� ��
    {
        DisappearObject();
        uiManager.RoundUIOff();
        StartCoroutine(SpecialWin());
    }

    IEnumerator SpecialWin()
    {
        mainCamera.gameObject.SetActive(false);
        actionCamera.gameObject.SetActive(true);
        actionCamera.gameObject.transform.position = new Vector3(0, 2, 0);
        actionCamera.gameObject.transform.rotation = Quaternion.Euler(-3, 180, 0);
        cardManager.CharacterOn();
        cardManager.BattleAction(playerAttackType);
        uiManager.BattleUIGoupOn(playerType.Red, playerAttackType);
        yield return new WaitForSeconds(1f);
        uiManager.BattleUIOff();
        yield return new WaitForSeconds(3f);
        uiManager.ResultOn(Result.WIn);
    }

    void PlayerGameLose() // �÷��̾ ���ӿ��� ���� ��
    {
        DisappearObject();
        uiManager.RoundUIOff();
        StartCoroutine(SpecialLose());
    }

    IEnumerator SpecialLose()
    {
        mainCamera.gameObject.SetActive(false);
        actionCamera.gameObject.SetActive(true);
        actionCamera.gameObject.transform.position = new Vector3(0, 2, 0);
        actionCamera.gameObject.transform.rotation = Quaternion.Euler(-3, 0, 0);
        enemyCardManager.CharacterOn();
        enemyCardManager.BattleAction(playerAttackType);
        uiManager.BattleUIGoupOn(playerType.Blue, enemyAttackType);
        yield return new WaitForSeconds(1f);
        uiManager.BattleUIOff();
        yield return new WaitForSeconds(3f);
        uiManager.ResultOn(Result.WIn);
    }

    void RoundDraw() // ���忡�� ����� ��
    {
        if(playerAttackType == AttackType.Special)
        {
            playerScore++;
            enemyScore++;
            cardManager.PlayerLose();
            enemyCardManager.EnemyLose();
            roundResult = Result.Draw;
            phase = Phase.Battle;
        }
        else
        {
            Debug.Log("Draw");
            cardManager.PlayerWinorDraw();
            enemyCardManager.EnemyWinorDraw();
            roundResult = Result.Draw;
            phase = Phase.Battle;

        }

        
    }

    void PlayerRoundWin() // ���忡�� �÷��̾ �̰��� ��
    {
        playerScore++; //�÷��̾� ���� ���
        cardManager.PlayerWinorDraw(); //�̱� ī���� ����Ÿ�� ����
        enemyCardManager.EnemyLose(); // ��ī�� ����
        roundResult = Result.WIn;
        phase = Phase.Battle; //��Ʋ���� ������� �ѱ�
    }

    void PlayerRoundLose() // ���忡�� �÷��̾ ���� ��
    {
        enemyScore++;
        cardManager.PlayerLose();
        enemyCardManager.EnemyWinorDraw();
        roundResult = Result.Lose;
        phase = Phase.Battle;
        Debug.Log("Lose");
    }

    void RandomDraw() //���� �÷��̾ ��� ���ѽð� ���� ī�带 �������� �ʾ� ����� �� ����
    {
        cardManager.PlayerLose();
        enemyCardManager.EnemyLose();
        roundResult = Result.Lose;
        phase = Phase.Battle;
    }

    void CheckScore() //���ӿ��� �̱�ų� ������ �Ǵ�
    {
        if(playerScore == enemyScore && playerScore == goalScore)
        {
            uiManager.ResultOn(Result.Draw);
        }
        else if(playerScore == goalScore)
        {
            uiManager.ResultOn(Result.WIn);
        }
        else if(enemyScore == goalScore)
        {
            uiManager.ResultOn(Result.Lose);
        }
        else
        {
            phase = Phase.RoundStart;
            NextRound(); 
        }
    }

    void NextRound() 
    {
        cardManager.ResetSelectCard();
        enemyCardManager.ResetSelectCard();
    }

    public void GetPlayerAttackType(AttackType attackType) // �÷��̾��� ����Ÿ���� �޾ƿ�
    {
        playerAttackType = attackType;
    }

    public void GetEnemyAttackType(AttackType attackType) // ���� ����Ÿ���� �޾ƿ�
    {
        enemyAttackType = attackType;
    }

    void DisappearObject() //����, ���� ���� �� ������Ʈ ��Ȱ��ȭ
    {
        cardManager.ActiveFalseCard();
        enemyCardManager.ActiveFalseCard();
    }

    void Appearobject() //����, ���� ���� �� ������Ʈ Ȱ��ȭ
    {
        cardManager.ActiveTrueCard();
        enemyCardManager.ActiveTrueCard();
    }

    void Battle(Result result) //���� ����� �������� ���� ����
    {
        phase = Phase.None;
        DisappearObject();
        uiManager.RoundUIOff();
        cardManager.CharacterOn();
        enemyCardManager.CharacterOn();
        StartCoroutine(BattleAction(result));
       
        
    }

    IEnumerator BattleAction(Result result)
    {
        mainCamera.gameObject.SetActive(false);
        actionCamera.gameObject.SetActive(true);
        if (result == Result.WIn)
        {
            actionCamera.gameObject.transform.rotation = Quaternion.Euler(-3, 0, 0);
            yield return new WaitForSeconds(0.2f);
            enemyCardManager.BattleAction(enemyAttackType);
            uiManager.BattleUIGoupOn(playerType.Blue, enemyAttackType);
            yield return new WaitForSeconds(1.5f);
            actionCamera.gameObject.transform.rotation = Quaternion.Euler(-3, 180, 0);
            cardManager.BattleAction(playerAttackType);
            uiManager.BattleUIGoupOn(playerType.Red, playerAttackType);
            yield return new WaitForSeconds(1.5f);
            uiManager.SetRoundResultText(result);
        }
        else if (result == Result.Lose)
        {
            actionCamera.gameObject.transform.rotation = Quaternion.Euler(-3, -180, 0);
            yield return new WaitForSeconds(0.2f);
            cardManager.BattleAction(playerAttackType);
            uiManager.BattleUIGoupOn(playerType.Red, playerAttackType);
            yield return new WaitForSeconds(1.5f);
            actionCamera.gameObject.transform.rotation = Quaternion.Euler(-3, 0, 0);
            enemyCardManager.BattleAction(enemyAttackType);
            uiManager.BattleUIGoupOn(playerType.Blue, enemyAttackType);
            yield return new WaitForSeconds(1.5f);
            uiManager.SetRoundResultText(result);
        }
        else if (result == Result.Draw)
        {
            actionCamera.gameObject.transform.rotation = Quaternion.Euler(-3, -180, 0);
            yield return new WaitForSeconds(0.2f);
            cardManager.BattleAction(playerAttackType);
            uiManager.BattleUIGoupOn(playerType.Red, playerAttackType);
            yield return new WaitForSeconds(1.5f);
            actionCamera.gameObject.transform.rotation = Quaternion.Euler(-3, 0, 0);
            enemyCardManager.BattleAction(enemyAttackType);
            uiManager.BattleUIGoupOn(playerType.Blue, enemyAttackType);
            yield return new WaitForSeconds(1.5f);
            uiManager.SetRoundResultText(result);
        }
        yield return new WaitForSeconds(1f);
        playerAttackType = AttackType.None;
        enemyAttackType = AttackType.None;
        cardManager.CharacterOff();
        enemyCardManager.CharacterOff();
        uiManager.BattleUIOff();
        CheckScore();
        mainCamera.gameObject.SetActive(true);
        actionCamera.gameObject.SetActive(false);
    }
}
