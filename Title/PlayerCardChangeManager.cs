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

    void LoadPlayerCard() // �÷��̾��� ī�� ������ ���
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

    public void MakeCardScrollView(CardSOList cardSOList) //��� ī�带 ���� ��ũ�Ѻ� ���� �Լ� 
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


    public void ChangeCard(int index) //ī�� ��Ͽ��� ī�带 �������� ��
    {
        for(int i = 0; i < 5; i++)
        {
            if (currentCardIndexList[i] == index) //ī�尡 �̹� ���õ� ���� ��� ���õ� ī�� ��Ͽ��� ī�� ����
            {
                currentCardIndexList[i] = -1;
                playerCards[i].SetActive(false);
                return;
            }
        }
        for(int i = 0;i < 5; i++)
        {
            if (currentCardIndexList[i] == -1) // ī�尡 ���õǾ� ���� ���� ���, ù ��ĭ�� ī�� �߰�
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
        StartCoroutine(SetArarmText(2)); //��ĭ�� ���� ī�� ��ϺҰ� �ȳ� ǥ��
    }

    public void SelectPlayerCard() //�÷��̾� ī�� ���� �Լ� 
    {
        for (int i = 0; i < 5; i++)
        {
            if(currentCardIndexList[i] == -1)
            {
                StartCoroutine(SetArarmText(0)); //�÷��̾��� ī�尡 5���� ���� ���� �� ���� ǥ��
                return;
            }
           
        }
        for (int i = 0; i < 5; i++)
        {
            playerData.playerCardSOIndex[i] = currentCardIndexList[i];
        }
        StartCoroutine(SetArarmText(3)); //����Ϸ� ǥ��
    }

    public void SelectedCardClick(int index) //���õ��ִ� ī�带 ���ý� ����Ʈ���� ī�� ���� �Լ�
    {
        currentCardIndexList[index] = -1;
        playerCards[index].SetActive(false);
    }


    public void DropNonSelectCard(int slotindex, int cardIndex) //���õ����� ���� ī�带 ī�� ���Կ� ������ �� �Լ�
    {
        for (int i = 0; i < 5; i++)
        {
            if (currentCardIndexList[i] == cardIndex)
            {
                StartCoroutine(SetArarmText(1)); //���� ī�尡 ��ϵ� ���� �� �ȳ� ǥ��
                return;
            }
        }
        //ī�彽�Կ� �ִ� ī��� ���� ������ ī�� ��ȯ
        currentCardIndexList[slotindex] = cardIndex;
        playerCards[slotindex].SetActive(true);
        CardSO newCardSO = stageSettingManager.LoadCardSO(cardIndex);
        TextMeshProUGUI cardNameText = playerCards[slotindex].GetComponentInChildren<TextMeshProUGUI>();
        cardNameText.SetText(newCardSO.cards.Cardname);
        Image[] images = playerCards[slotindex].GetComponentsInChildren<Image>();
        images[1].sprite = newCardSO.cards.CharacterImage.sprite;
        currentCardIndexList[slotindex] = cardIndex;
    }

    public void DropSelectedCard(int nowIndex, int changeIndex) //���õ��ִ� ī�带 �ٸ� ���õ��ִ� ī�忡 ������ �� �� ī���� ��ġ�� �����ϴ� �Լ�
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

    public void DropSelectedCardToSlot(int nowIndex,  int changeIndex) //���õ��ִ� ī�尡 ī�彽�Կ� �������� �� �ش� ī�� �������� ���õ� ī���� ��ġ�� �����ϴ� �Լ�
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

    IEnumerator SetArarmText(int ArarmCase) //���� �˶� ��� �Լ�
    {
        ArarmText.gameObject.SetActive(true);
        if(ArarmCase == 0)
        {
            ArarmText.SetText("���� 5���� ī�尡 �������� �ʽ��ϴ�");
        }
        else if(ArarmCase == 1)
        {
            ArarmText.SetText("���� ���� ī�尡 �����մϴ�");
        }
        else if (ArarmCase == 2)
        {
            ArarmText.SetText("���� �� ������ �����ϴ�");
        }
        else if(ArarmCase == 3)
        {
            ArarmText.SetText("����Ǿ����ϴ�");
        }

        yield return new WaitForSeconds(1);
        ArarmText.gameObject.SetActive(false);
    }

}
