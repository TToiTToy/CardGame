using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUBattleManager : MonoBehaviour
{
    public DataManager DataManager;
    public StageSettingManager StageSettingManager;
    public CardManager CardManager;
    public EnemyCardManager EnemyCardManager;
    public BackgroundManager BackgroundManager;

    void Awake()
    {
        GameObject datamanager = GameObject.Find("DataManager");
        DataManager = datamanager.GetComponent<DataManager>();
        SetPlayerCard();
        SetEnemy();
    }

    void SetPlayerCard()
    {
        PlayerData playerData = DataManager.playerData;
        List<CardSO> playerCardSO = new List<CardSO>();
        for(int i = 0;i <5 ;i++)
        {
            CardSO newCardSO = StageSettingManager.LoadCardSO(playerData.playerCardSOIndex[i]);
            playerCardSO.Add(newCardSO);
        }
        CardManager.myCardSOList = playerCardSO;
        StageSettingItem playerstage = StageSettingManager.LoadStageSettingItem(StageObject.Stage, playerData.StageIndex);
        GameObject playerStageobject = playerstage.itemObject;
        StageSettingItem playerBackground = StageSettingManager.LoadStageSettingItem(StageObject.Background, playerData.backgroundIndex);
        GameObject playerBackgroundObject = playerBackground.itemObject;
        StageSettingItem playerBGM = StageSettingManager.LoadStageSettingItem(StageObject.BGM, playerData.BGMIndex);
        GameObject playerBGMObject = playerBGM.itemObject;
        BackgroundManager.GetPlayerItem(playerStageobject, playerBackgroundObject, playerBGMObject);
    }

    void SetEnemy()
    {
        EnemyData enemyData = DataManager.enemyData;
        List<CardSO> enemyCardSO = new List<CardSO>();
        for (int i = 0; i < 5; i++)
        {
            CardSO newCardSO = StageSettingManager.LoadCardSO(enemyData.enemyCardSOIndex[i]);
            enemyCardSO.Add(newCardSO);
        }
        EnemyCardManager.enemyCardSOList = enemyCardSO;
        EnemyCardManager.SetEnemy(enemyData.enemy);
        StageSettingItem enemyStage = StageSettingManager.LoadStageSettingItem(StageObject.Stage, enemyData.StageIndex);
        GameObject enemyStageobject = enemyStage.itemObject;
        StageSettingItem enemyBackground = StageSettingManager.LoadStageSettingItem(StageObject.Background, enemyData.backgroundIndex);
        GameObject enemyBackgroundObject = enemyBackground.itemObject;
        StageSettingItem enemyBGM = StageSettingManager.LoadStageSettingItem(StageObject.BGM, enemyData.BGMIndex);
        GameObject enemyBGMObject = enemyBGM.itemObject;
        StageSettingItem enemyCardBack = StageSettingManager.LoadStageSettingItem(StageObject.CardBack, enemyData.cardBackIndex);
        EnemyCardManager.ChangeCardBack(enemyCardBack);
        BackgroundManager.GetEnemyItem(enemyStageobject, enemyBackgroundObject, enemyBGMObject);
    }

}
