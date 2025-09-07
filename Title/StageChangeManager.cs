using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum StageObject
{
    Stage,
    Background,
    BGM,
    CardBack

}

public class StageChangeManager : MonoBehaviour
{
    public StageSettingManager stageSettingManager;

    public List<Button> objectSelectBtns;
    public List<GameObject> objectUIGroup;

    public Image playerStageImage;
    public TextMeshProUGUI playerStageName;
    public Image playerStageBackgroundImage;
    public TextMeshProUGUI playerStageBackgroundName;
    public Image playerStageBGMImage;
    public TextMeshProUGUI playerStageBGMName;
    public Image playerCardBackImage;
    public TextMeshProUGUI playerCardBackName;

    public SelectItemScrollManager stageControllViewManager;
    public SelectItemScrollManager stageBackgroundControllViewManager;
    public SelectItemScrollManager stageBGMControllViewManager;
    public SelectItemScrollManager cardBackControllViewManager;
    public TextMeshProUGUI ararmText;
    StageObject currentStageObject;
    Title title;
    int currentIndex;
    int currentItemIndex;

    PlayerData playerData;


    private void Awake()
    {
        title = GetComponent<Title>();
        playerData = title.dataManager.playerData;
        LoadSetting(StageObject.Stage);
        MakeScrollView();
    }

    public void ClickStageChangeBtn()
    {
        ChangeObejctGroup(0);
        title.StageSettingBtnClick();
    }


    public void ChangeObejctGroup(int index) //ȭ�鿡 ǥ�õǴ� ���������׷� ����
    {
        objectSelectBtns[currentIndex].image.color = new Color32(182, 255, 231, 255); //���� ��ư ������
        objectSelectBtns[index].image.color = new Color32(0, 188, 195, 255); //������ ��ư ������
        objectUIGroup[currentIndex].SetActive(false); //���� �׷� ��Ȱ��ȭ
        objectUIGroup[index].SetActive(true); //���� �׷� Ȱ��ȭ
        currentIndex = index;
        ChangeStageObject(index);
        LoadSetting(currentStageObject); //���� �÷��̾� ���� �ҷ���

    }

    void ChangeStageObject(int index)
    {
        if (index == 0)
        {
            currentStageObject = StageObject.Stage;
        }
        else if (index == 1)
        {
            currentStageObject = StageObject.Background;
        }
        else if (index == 2)
        {
            currentStageObject = StageObject.BGM;
        }
        else
        {
            currentStageObject = StageObject.CardBack;
        }
    }

    public void ChangePlayerdata() //�÷��̾� ������ ���� �Լ�
    {
        title.ChangePlayerData(currentStageObject, currentItemIndex);
        StartCoroutine(SetArarmText(0));
    }

    void LoadSetting(StageObject stageObject) //�� �������� �׷� ���ο� �÷��̾ ������ ������Ʈ�� ǥ�õǰ� �ϴ� �Լ�
    {
        int playerItemIndex = LoadPlayerData(stageObject);
        if (playerItemIndex != -1)
        {
            StageSettingItem playerItem = stageSettingManager.LoadStageSettingItem(stageObject, playerItemIndex);
            if (playerItem != null)
            {
                if (stageObject == StageObject.Stage)
                {
                    playerStageImage.color = playerItem.itemImage.color;
                    playerStageImage.sprite = playerItem.itemImage.sprite;
                    playerStageName.SetText(playerItem.itemName);
                }
                else if (stageObject == StageObject.Background)
                {
                    playerStageBackgroundImage.color = playerItem.itemImage.color;
                    playerStageBackgroundImage.sprite = playerItem.itemImage.sprite;
                    playerStageBackgroundName.SetText(playerItem.itemName);
                }
                else if (stageObject == StageObject.BGM)
                {
                    playerStageBGMImage.color = playerItem.itemImage.color;
                    playerStageBGMImage.sprite = playerItem.itemImage.sprite;
                    playerStageBGMName.SetText(playerItem.itemName);
                }
                else if (stageObject == StageObject.CardBack)
                {
                    playerCardBackImage.color = playerItem.itemImage.color;
                    playerCardBackImage.sprite = playerItem.itemImage.sprite;
                    playerCardBackName.SetText(playerItem.itemName);
                }
            }
            currentItemIndex = playerItemIndex;
        }

    }

    void MakeScrollView()
    {
        stageControllViewManager.MakeView(stageSettingManager.stages);
        stageBackgroundControllViewManager.MakeView(stageSettingManager.stageBackgrounds);
        stageBGMControllViewManager.MakeView(stageSettingManager.stageBGMs);
        cardBackControllViewManager.MakeView(stageSettingManager.cardBacks);

    }


    int LoadPlayerData(StageObject stageObject)
    {
        if (stageObject == StageObject.Stage)
        {
            return playerData.StageIndex;
        }
        else if (stageObject == StageObject.Background)
        {
            return playerData.backgroundIndex;
        }
        else if (stageObject == StageObject.BGM)
        {
            return playerData.BGMIndex;
        }
        else if (stageObject == StageObject.CardBack)
        {
            return playerData.cardBackIndex;
        }
        return -1;
    }

    public void changePlayerCurrentItem(int index)
    {
        if (index != currentItemIndex)
        {
            ChangeItem(index);

        }

    }

    void ChangeItem(int index) //���ο� ������ �������� ǥ���ϴ� �Լ�
    {
        StageSettingItem playerItem = stageSettingManager.LoadStageSettingItem(currentStageObject, index);
        if (playerItem != null)
        {
            if (currentStageObject == StageObject.Stage)
            {
                playerStageImage.color = playerItem.itemImage.color;
                playerStageImage.sprite = playerItem.itemImage.sprite;
                playerStageName.SetText(playerItem.itemName);
            }
            else if (currentStageObject == StageObject.Background)
            {
                playerStageBackgroundImage.color = playerItem.itemImage.color;
                playerStageBackgroundImage.sprite = playerItem.itemImage.sprite;
                playerStageBackgroundName.SetText(playerItem.itemName);
            }
            else if (currentStageObject == StageObject.BGM)
            {
                playerStageBGMImage.color = playerItem.itemImage.color;
                playerStageBGMImage.sprite = playerItem.itemImage.sprite;
                playerStageBGMName.SetText(playerItem.itemName);
            }
            else if (currentStageObject == StageObject.CardBack)
            {
                playerCardBackImage.color = playerItem.itemImage.color;
                playerCardBackImage.sprite = playerItem.itemImage.sprite;
                playerCardBackName.SetText(playerItem.itemName);
            }
        }
        currentItemIndex = index;
        
    }
    IEnumerator SetArarmText(int ArarmCase)
    {
        ararmText.gameObject.SetActive(true);
        if (ArarmCase == 0)
        {
            ararmText.SetText("����Ǿ����ϴ�");
        }


        yield return new WaitForSeconds(1);
        ararmText.gameObject.SetActive(false);
    }
}
