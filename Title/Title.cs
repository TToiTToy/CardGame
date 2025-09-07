using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public DataManager dataManager;
    public CanvasGroup mainGroup;
    public CanvasGroup vsCPUGroup;
    public CanvasGroup cardAndStageSettingGroup;
    public CanvasGroup cardSettingGroup;
    public CanvasGroup stageSettingGroup;

    CanvasGroup previousGroup;
    CanvasGroup nowGroup;

    private void Awake()
    {
        GameObject datamanager = GameObject.Find("DataManager");
        dataManager = datamanager.GetComponent<DataManager>();
        nowGroup = mainGroup;

    }

    public void VsCPUBtnClick() //VsCPU버튼 클릭시
    {
        canvasOff(nowGroup);
        canvasOn(vsCPUGroup);
        previousGroup = nowGroup;
        nowGroup = vsCPUGroup;

    }

    public void CardAndStageSettingBtnClick() //카드&프로필버튼 클릭시
    {
        canvasOff(nowGroup);
        canvasOn(cardAndStageSettingGroup);
        previousGroup = nowGroup;
        nowGroup = cardAndStageSettingGroup;

    }
    public void CardSettingBtnClick()//카드세팅 버튼 클릭시
    {
        canvasOff(nowGroup);
        canvasOn(cardSettingGroup);
        previousGroup = nowGroup;
        nowGroup = cardSettingGroup;

    }

    public void StageSettingBtnClick() //스테이지 세팅 버튼 클릭시
    {
        canvasOff(nowGroup);
        canvasOn(stageSettingGroup);
        previousGroup = nowGroup;
        nowGroup = stageSettingGroup;

    }
    public void ExitBtnClick() //나가기 버튼 클릭 시 
    {
        Application.Quit();
    }

    public void backToPreviousCanvasBtnClick() //이전화면으로 돌아가는 함수
    {
        if(previousGroup == mainGroup)
        {
            canvasOff(nowGroup);
            canvasOn(previousGroup);
            nowGroup = previousGroup;
            previousGroup = null;
        }
        else
        {
            canvasOff(nowGroup);
            canvasOn(previousGroup);
            nowGroup = previousGroup;
            previousGroup = mainGroup;
        }

    }

    void canvasOn(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;  
    }

    void canvasOff(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void ChangePlayerData(StageObject stageObject, int ItemIndex) //플레이어 데이터 변경 함수
    {
        dataManager.ChangePlayerData(stageObject, ItemIndex);

    }

    public void VsCPUStart() //CPU대전 시작 함수
    {
        SceneManager.LoadScene("VsCPUScene");
    }
}
