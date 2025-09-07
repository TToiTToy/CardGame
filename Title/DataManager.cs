using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public PlayerData playerData;
    public EnemyData enemyData;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if(instance != this)
            {
                Destroy(gameObject);
            }
        }
    }


    public void ChangePlayerData(StageObject stageObject, int ItemIndex) //플레이어 데이터 변경 함수
    {

        if (stageObject == StageObject.Stage)
        {
            playerData.StageIndex = ItemIndex;
        }
        else if(stageObject == StageObject.Background)
        {
            playerData.backgroundIndex = ItemIndex;
        }
        else if(stageObject == StageObject.BGM)
        {
            playerData.BGMIndex = ItemIndex;
        }
        else if(stageObject == StageObject.CardBack)
        {
            playerData.cardBackIndex = ItemIndex;
        }
    }
}
