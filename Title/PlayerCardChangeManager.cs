using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCardChangeManager : MonoBehaviour
{
    public List<GameObject> playerCards;
    public StageSettingManager stageSettingManager;
    public GameObject cardObject;
    public GameObject scrollViewContent;
    public SelectItemManager selectItemManager;
    public TextMeshProUGUI ArarmText;
    Title title;
    PlayerData playerData;
    int[] currentCardIndexList;
    private void Awake()
    {
        currentCardIndexList = new int[5];
        title = GetComponent<Title>();
        playerData = title.dataManager.playerData;
        LoadPlayerCard();
        MakeCardScrollView(stageSettingManager.CardSOList);
    }

    void LoadPlayerCard() // 플레이어의 카드 정보를 등록
    {
        for (int i = 0; i < playerCards.Count; i++)
        {
            int playerCardIndex = playerData.playerCardSOIndex[i];
            CardSO newCardSO = stageSettingManager.LoadCardSO(playerCardIndex);
            TextMeshProUGUI cardNameText = playerCards[i].GetComponentInChildren<TextMeshProUGUI>();
            cardNameText.SetText(newCardSO.cards.Cardname);
            Image[] images = playerCards[i].GetComponentsInChildren<Image>();
            images[1].sprite = newCardSO.cards.CharacterImage.sprite;
            currentCardIndexList[i] = playerCardIndex;
            playerCards[i].SetActive(true);
        }
    }

    public void CardChangeBtnClick() 
    {
        LoadPlayerCard();
        title.CardSettingBtnClick();
    }

    public void MakeCardScrollView(CardSOList cardSOList) //모든 카드를 지닌 스크롤뷰 생성 함수 
    {
        for (int i = 0; i < cardSOList.list.Count; i++)
        {
            CardSO cardSO = cardSOList.list[i];
            GameObject newcard = Instantiate(cardObject);
            newcard.transform.SetParent(scrollViewContent.transform);
            SelectableCard selectableCard = newcard.GetComponent<SelectableCard>();
            selectableCard.SetIndex(i);
            selectableCard.selectItemManager = selectItemManager;
            Image[] UIimages = newcard.GetComponentsInChildren<Image>();
            UIimages[1].sprite = cardSO.cards.CharacterImage.sprite;
            TextMeshProUGUI itemText = newcard.GetComponentInChildren<TextMeshProUGUI>();
            itemText.SetText(cardSO.cards.Cardname);

        }
    }


    public void ChangeCard(int index) //카드 목록에서 카드를 선택했을 때
    {
        for(int i = 0; i < 5; i++)
        {
            if (currentCardIndexList[i] == index) //카드가 이미 선택되 있을 경우 선택된 카드 목록에서 카드 제거
            {
                currentCardIndexList[i] = -1;
                playerCards[i].SetActive(false);
                return;
            }
        }
        for(int i = 0;i < 5; i++)
        {
            if (currentCardIndexList[i] == -1) // 카드가 선택되어 있지 않을 경우, 첫 빈칸에 카드 추가
            {
                currentCardIndexList[i] = index;
                playerCards[i].SetActive(true);
                CardSO newCardSO = stageSettingManager.LoadCardSO(index);
                TextMeshProUGUI cardNameText = playerCards[i].GetComponentInChildren<TextMeshProUGUI>();
                cardNameText.SetText(newCardSO.cards.Cardname);
                Image[] images = playerCards[i].GetComponentsInChildren<Image>();
                images[1].sprite = newCardSO.cards.CharacterImage.sprite;
                currentCardIndexList[i] = index;
                return;
            }
        }
        StartCoroutine(SetArarmText(2)); //빈칸이 없어 카드 등록불가 안내 표시
    }

    public void SelectPlayerCard() //플레이어 카드 저장 함수 
    {
        for (int i = 0; i < 5; i++)
        {
            if(currentCardIndexList[i] == -1)
            {
                StartCoroutine(SetArarmText(0)); //플레이어의 카드가 5장이 되지 않을 때 오류 표시
                return;
            }
           
        }
        for (int i = 0; i < 5; i++)
        {
            playerData.playerCardSOIndex[i] = currentCardIndexList[i];
        }
        StartCoroutine(SetArarmText(3)); //저장완료 표시
    }

    public void SelectedCardClick(int index) //선택되있는 카드를 선택시 리스트에서 카드 제거 함수
    {
        currentCardIndexList[index] = -1;
        playerCards[index].SetActive(false);
    }


    public void DropNonSelectCard(int slotindex, int cardIndex) //선택되있지 않은 카드를 카드 슬롯에 놓았을 때 함수
    {
        for (int i = 0; i < 5; i++)
        {
            if (currentCardIndexList[i] == cardIndex)
            {
                StartCoroutine(SetArarmText(1)); //같은 카드가 등록되 있을 때 안내 표시
                return;
            }
        }
        //카드슬롯에 있던 카드와 새로 선택한 카드 교환
        currentCardIndexList[slotindex] = cardIndex;
        playerCards[slotindex].SetActive(true);
        CardSO newCardSO = stageSettingManager.LoadCardSO(cardIndex);
        TextMeshProUGUI cardNameText = playerCards[slotindex].GetComponentInChildren<TextMeshProUGUI>();
        cardNameText.SetText(newCardSO.cards.Cardname);
        Image[] images = playerCards[slotindex].GetComponentsInChildren<Image>();
        images[1].sprite = newCardSO.cards.CharacterImage.sprite;
        currentCardIndexList[slotindex] = cardIndex;
    }

    public void DropSelectedCard(int nowIndex, int changeIndex) //선택되있는 카드를 다른 선택되있는 카드에 놓았을 때 두 카드의 위치를 스왑하는 함수
    {

            int nowCardIndex = currentCardIndexList[nowIndex];
            CardSO nowCardSO = stageSettingManager.LoadCardSO(nowCardIndex);
            int changeCardIndex = currentCardIndexList[changeIndex];
            CardSO changeCardSO = stageSettingManager.LoadCardSO(changeCardIndex);
            TextMeshProUGUI changecardNameText = playerCards[changeIndex].GetComponentInChildren<TextMeshProUGUI>();
            changecardNameText.SetText(nowCardSO.cards.Cardname);
            Image[] changecardimages = playerCards[changeIndex].GetComponentsInChildren<Image>();
            changecardimages[1].sprite = nowCardSO.cards.CharacterImage.sprite;
            currentCardIndexList[changeIndex] = nowCardIndex;
            TextMeshProUGUI cardNameText = playerCards[nowIndex].GetComponentInChildren<TextMeshProUGUI>();
            cardNameText.SetText(changeCardSO.cards.Cardname);
            Image[] images = playerCards[nowIndex].GetComponentsInChildren<Image>();
            images[1].sprite = changeCardSO.cards.CharacterImage.sprite;
            currentCardIndexList[nowIndex] = changeCardIndex;
    }

    public void DropSelectedCardToSlot(int nowIndex,  int changeIndex) //선택되있는 카드가 카드슬롯에 놓여졌을 때 해당 카드 슬롯으로 선택된 카드의 위치를 변경하는 함수
    {

        int nowCardIndex = currentCardIndexList[nowIndex];
        CardSO nowCardSO = stageSettingManager.LoadCardSO(nowCardIndex);
        playerCards[nowIndex].SetActive(false);
        currentCardIndexList[nowIndex] = -1;
        TextMeshProUGUI cardNameText = playerCards[changeIndex].GetComponentInChildren<TextMeshProUGUI>();
        cardNameText.SetText(nowCardSO.cards.Cardname);
        Image[] images = playerCards[changeIndex].GetComponentsInChildren<Image>();
        images[1].sprite = nowCardSO.cards.CharacterImage.sprite;
        currentCardIndexList[changeIndex] = nowCardIndex;
        playerCards[changeIndex].SetActive(true);
    }

    IEnumerator SetArarmText(int ArarmCase) //각종 알람 출력 함수
    {
        ArarmText.gameObject.SetActive(true);
        if(ArarmCase == 0)
        {
            ArarmText.SetText("덱에 5장의 카드가 존재하지 않습니다");
        }
        else if(ArarmCase == 1)
        {
            ArarmText.SetText("덱에 같은 카드가 존재합니다");
        }
        else if (ArarmCase == 2)
        {
            ArarmText.SetText("덱에 빈 공간이 없습니다");
        }
        else if(ArarmCase == 3)
        {
            ArarmText.SetText("저장되었습니다");
        }

        yield return new WaitForSeconds(1);
        ArarmText.gameObject.SetActive(false);
    }

}
