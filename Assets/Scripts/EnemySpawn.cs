using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class EnemySpawn : MonoBehaviour
{
    public WaveDataSO waveData;

    public List<UnitData> unitDatas = new List<UnitData>();
    float[] spawnTimer;
    int[] curSpawnCount;
    [SerializeField] Castle enemycastle;
    int tempCount;
    private void Awake()
    {
        enemycastle.castleData.Health = waveData.enemyCastleHealth;
        spawnTimer = new float[waveData.enemies.Count];
        curSpawnCount = new int[waveData.enemies.Count];
    }
    private void Start()
    {
        for (int i = 0; i < waveData.enemies.Count; i++)
        {
            GameObject unit = PoolManager.Instance.Get(PoolEnum.Enemy, waveData.enemies[i].name.ToString(), MyUtil.SpawnRandomPos(47f, 54f, -4f, -6.5f), Quaternion.identity);
            UnitData unitData = unit.GetComponent<IUnitData>().UnitData;

            CopyUnitData(unitData, unitDatas[i]);
            EnemyMultiplier(unitDatas[i], waveData.enemies[i].multiplier);
            unit.GetComponent<IUnitData>().UnitData = unitDatas[i];
            unit.SetActive(false);           
        }
    }

    private void Update()
    {
        for (int i = 0; i < waveData.enemies.Count; i++)
            Timer(i, waveData.enemies[i].spawnCount, waveData.enemies[i].spawnDelay);
    }
    void Timer(int index,int spawnCount,float spawnDelay)
    {
        if (curSpawnCount[index] >= spawnCount)
            return;
        if (waveData.enemies[index].spawnTime >= (enemycastle.Health / enemycastle.castleData.Health) * 100)
        {
            spawnTimer[index] += Time.deltaTime;
            if (spawnTimer[index] >= spawnDelay && curSpawnCount[index] <= spawnCount)
            {
                Debug.Log(waveData.enemies[index].name.ToString() + " 스폰");
                Spawn(index);
                curSpawnCount[index]++;
                spawnTimer[index] = 0;
            }
        }
    }
    void Spawn(int index)
    {
        tempCount++;
        GameObject unit = PoolManager.Instance.Get(PoolEnum.Enemy, waveData.enemies[index].name.ToString(), MyUtil.SpawnRandomPos(56.5f, 57.5f, -4f, -6f), Quaternion.identity);
        unit.GetComponent<IUnitData>().UnitData = unitDatas[index];
        unit.GetComponent<IUnitHeatlh>().Init();
        unit.name = waveData.enemies[index].name.ToString() + tempCount;
    }
    private void CopyUnitData(UnitData origin, UnitData copy)
    {
        copy.Image = origin.Image;
        copy.UnitKorName = origin.UnitKorName;
        copy.UnitName = origin.UnitName;
        copy.Level = origin.Level;
        copy.Health = origin.Health;
        copy.Damage = origin.Damage;
        copy.AttackSpeed = origin.AttackSpeed;
        copy.MoveSpeed = origin.MoveSpeed;
        copy.DetectRange = origin.DetectRange;
        copy.AttackRange = origin.AttackRange;
        copy.Cost = origin.Cost;
        copy.MoveDir = origin.MoveDir;
        copy.unitUpgradeData = origin.unitUpgradeData;
    }
    void EnemyMultiplier(UnitData unitData, float multiplier)
    {
        unitData.Damage *= multiplier/100;
        unitData.Health *= multiplier/100;
        unitData.Damage = Mathf.Round(unitData.Damage);
        unitData.Health = Mathf.Round(unitData.Health);
    }
}