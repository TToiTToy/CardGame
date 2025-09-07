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

    void RoundStart() //라운드 시작시
    {
        CheckHighScore(); // 점수를 확인
        DisappearObject(); //필요없는 오브젝트 비활성화
        phase = Phase.None;
        uiManager.RoundUIStart(round); //라운드 시작UI활성화
        StartCoroutine(RoundStartEnd()); 
    }

    IEnumerator RoundStartEnd()
    {
        cardSelectTime = 15f; //카드 선택시간 세팅
        yield return new WaitForSecondsRealtime(1.5f);
        uiManager.RoundUIEnd(); //라운드 시작UI 비활성화
        round++;
        Appearobject(); //비활성화한 오브젝트 활성화
        phase = Phase.CardSelect;
    }

    void CheckHighScore() // 점수를 확인해서, 일정 점수가 되었을때 게임 환경 변경
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

    void CardRandomSelect() //제한시간 내에 카드를 선택하지 않았을때 카드를 랜덤 선택
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


    void CheckWin() //승리 판단
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

    void PlayerGameWin() // 플레이어가 게임을 이겼을 때
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

    void PlayerGameLose() // 플레이어가 게임에서 졌을 때
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

    void RoundDraw() // 라운드에서 비겼을 때
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

    void PlayerRoundWin() // 라운드에서 플레이어가 이겼을 때
    {
        playerScore++; //플레이어 점수 상승
        cardManager.PlayerWinorDraw(); //이긴 카드의 공격타입 제거
        enemyCardManager.EnemyLose(); // 진카드 제거
        roundResult = Result.WIn;
        phase = Phase.Battle; //배틀연출 페이즈로 넘김
    }

    void PlayerRoundLose() // 라운드에서 플레이어가 졌을 때
    {
        enemyScore++;
        cardManager.PlayerLose();
        enemyCardManager.EnemyWinorDraw();
        roundResult = Result.Lose;
        phase = Phase.Battle;
        Debug.Log("Lose");
    }

    void RandomDraw() //양쪽 플레이어가 모두 제한시간 내에 카드를 선택하지 않아 비겼을 때 실행
    {
        cardManager.PlayerLose();
        enemyCardManager.EnemyLose();
        roundResult = Result.Lose;
        phase = Phase.Battle;
    }

    void CheckScore() //게임에서 이기거나 졌는지 판단
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

    public void GetPlayerAttackType(AttackType attackType) // 플레이어의 어택타입을 받아옴
    {
        playerAttackType = attackType;
    }

    public void GetEnemyAttackType(AttackType attackType) // 적의 어택타입을 받아옴
    {
        enemyAttackType = attackType;
    }

    void DisappearObject() //전투, 라운드 시작 시 오브젝트 비활성화
    {
        cardManager.ActiveFalseCard();
        enemyCardManager.ActiveFalseCard();
    }

    void Appearobject() //전투, 라운드 시작 후 오브젝트 활성화
    {
        cardManager.ActiveTrueCard();
        enemyCardManager.ActiveTrueCard();
    }

    void Battle(Result result) //라운드 결과를 바탕으로 전투 연출
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
