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

    public void VsCPUBtnClick() //VsCPU��ư Ŭ����
    {
        canvasOff(nowGroup);
        canvasOn(vsCPUGroup);
        previousGroup = nowGroup;
        nowGroup = vsCPUGroup;

    }

    public void CardAndStageSettingBtnClick() //ī��&�����ʹ�ư Ŭ����
    {
        canvasOff(nowGroup);
        canvasOn(cardAndStageSettingGroup);
        previousGroup = nowGroup;
        nowGroup = cardAndStageSettingGroup;

    }
    public void CardSettingBtnClick()//ī�弼�� ��ư Ŭ����
    {
        canvasOff(nowGroup);
        canvasOn(cardSettingGroup);
        previousGroup = nowGroup;
        nowGroup = cardSettingGroup;

    }

    public void StageSettingBtnClick() //�������� ���� ��ư Ŭ����
    {
        canvasOff(nowGroup);
        canvasOn(stageSettingGroup);
        previousGroup = nowGroup;
        nowGroup = stageSettingGroup;

    }
    public void ExitBtnClick() //������ ��ư Ŭ�� �� 
    {
        Application.Quit();
    }

    public void backToPreviousCanvasBtnClick() //����ȭ������ ���ư��� �Լ�
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

    public void ChangePlayerData(StageObject stageObject, int ItemIndex) //�÷��̾� ������ ���� �Լ�
    {
        dataManager.ChangePlayerData(stageObject, ItemIndex);

    }

    public void VsCPUStart() //CPU���� ���� �Լ�
    {
        SceneManager.LoadScene("VsCPUScene");
    }
}
