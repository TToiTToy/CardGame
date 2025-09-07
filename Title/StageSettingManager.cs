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

    //������Ʈ ��Ͽ��� �ε����� �´� StageSettingItem ��� �Լ�
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


    public CardSO LoadCardSO(int index) // �ε����� �´� CardSO ��� �Լ�
    {
        return CardSOList.list[index];
    }
}

