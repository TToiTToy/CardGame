using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class StageSettingManager : MonoBehaviour
{
    public StageSettingSO stages;
    public StageSettingSO stageBackgrounds;
    public StageSettingSO stageBGMs;
    public StageSettingSO cardBacks;

    public CardSOList CardSOList;

    //오브젝트 목록에서 인덱스에 맞는 StageSettingItem 출력 함수
    public StageSettingItem LoadStageSettingItem(StageObject stageObject, int index) 
    {
        if (stageObject == StageObject.Stage)
        {
            return stages.items[index];
        }
        else if (stageObject == StageObject.Background)
        {
            return stageBackgrounds.items[index];
        }
        else if (stageObject == StageObject.BGM)
        {
            return stageBGMs.items[index];
        }
        else if (stageObject == StageObject.CardBack)
        {
            return cardBacks.items[index];
        }
        return null;
    }


    public CardSO LoadCardSO(int index) // 인덱스에 맞는 CardSO 출력 함수
    {
        return CardSOList.list[index];
    }
}

