using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[System.Serializable]
public class EnemyData
{
    public string enemyName;
    public List<int> enemyCardSOIndex;
    public int StageIndex;
    public int backgroundIndex;
    public int BGMIndex;
    public int cardBackIndex;
    public Enemy enemy;

}



[CreateAssetMenu(fileName = "EnemyDataSO", menuName = "Scriptable Object/EnemyDataSO")]
public class EnemyDataSO : ScriptableObject
{
    public List<EnemyData> Enemies;
}
