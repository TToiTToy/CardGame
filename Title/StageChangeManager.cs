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


    public void ChangeObejctGroup(int index) //화면에 표시되는 스테이지그룹 변경
    {
        objectSelectBtns[currentIndex].image.color = new Color32(182, 255, 231, 255); //이전 버튼 색변경
        objectSelectBtns[index].image.color = new Color32(0, 188, 195, 255); //선택한 버튼 색변경
        objectUIGroup[currentIndex].SetActive(false); //이전 그룹 비활성화
        objectUIGroup[index].SetActive(true); //선택 그룹 활성화
        currentIndex = index;
        ChangeStageObject(index);
        LoadSetting(currentStageObject); //현재 플레이어 설정 불러옴

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

    public void ChangePlayerdata() //플레이어 데이터 변경 함수
    {
        title.ChangePlayerData(currentStageObject, currentItemIndex);
        StartCoroutine(SetArarmText(0));
    }

    void LoadSetting(StageObject stageObject) //각 스테이지 그룹 메인에 플레이어가 선택한 오브젝트가 표시되게 하는 함수
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

    void ChangeItem(int index) //메인에 선택한 아이템을 표시하는 함수
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
            ararmText.SetText("저장되었습니다");
        }


        yield return new WaitForSeconds(1);
        ararmText.gameObject.SetActive(false);
    }
}
