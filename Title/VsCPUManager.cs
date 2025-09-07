using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class VsCPUManager : MonoBehaviour
{
    public TextMeshProUGUI selectedRavalName;
    public List<Button> ravalBtns;
    public EnemyDataSO EnemyDataSO;
    public GameObject ravalGroup;

    private int selectRavalIndex;
    private Title title;
    private bool isRavalGroupOpen;

    private void Awake()
    {
        title = GetComponent<Title>();
        title.dataManager.enemyData = EnemyDataSO.Enemies[0];
        LoadEnemy();
    }

    void LoadEnemy()
    {
        EnemyData enemyData = title.dataManager.enemyData;
        selectedRavalName.SetText(enemyData.enemyName);
        for (int i = 1;i<ravalBtns.Count;i++)
        {
            TextMeshProUGUI ravalName = ravalBtns[i].GetComponentInChildren<TextMeshProUGUI>();
            ravalName.SetText(EnemyDataSO.Enemies[i - 1].enemyName);
        }
    }

    public void SelectRaval(int i)
    {
        if(i == 0)
        {
            int randomIndex = Random.Range(0, EnemyDataSO.Enemies.Count);
            ChangeRaval(randomIndex);
            ravalGroup.SetActive(false);
        }
        else
        {
            ChangeRaval(i-1);
            ravalGroup.SetActive(false);
        }
    }

    public void OpenRavalGroupBtnClick()
    {
        if(!isRavalGroupOpen)
        {
            ravalGroup.SetActive(true);
            isRavalGroupOpen = true;
        }
        else
        {
            ravalGroup.SetActive(false);
            isRavalGroupOpen = false;
        }

    }



    void ChangeRaval(int index)
    {
        selectedRavalName.SetText(EnemyDataSO.Enemies[index].enemyName);
        title.dataManager.enemyData = EnemyDataSO.Enemies[index];
    }
}
