using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectItemManager : MonoBehaviour
{
    public StageChangeManager stageChangeManager;
    public PlayerCardChangeManager playerCardChangeManager;

    public void ChangeItem(int index)
    {
        stageChangeManager.changePlayerCurrentItem(index);
    }

    public void ChangeCard(int index)
    {
        playerCardChangeManager.ChangeCard(index);  
    }

    public void SelectedCardClick(int index)
    {
        playerCardChangeManager.SelectedCardClick(index);
    }

    public void DropNonSelectCard(int slotindex, int cardIndex)
    {
        playerCardChangeManager.DropNonSelectCard(slotindex, cardIndex);  
    }

    public void DropSelectedCard(int nowIndex, int changeIndex)
    {
        playerCardChangeManager.DropSelectedCard(nowIndex, changeIndex);
    }

    public void DropSelectedCardToSlot(int nowIndex, int changeIndex)
    {
        playerCardChangeManager.DropSelectedCardToSlot(nowIndex, changeIndex);
    }
}
