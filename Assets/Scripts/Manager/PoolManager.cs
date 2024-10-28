using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoolEnum
{
    Friendly,
    Enemy,
    Projectile,
    HitScan
}

[System.Serializable]
public class PoolType
{
    public PoolEnum poolType;
    public GameObject[] prefabs;
    [HideInInspector] public List<GameObject>[] pools;

    public Dictionary<string, int> poolNames;
}

public class PoolManager : Singleton<PoolManager>
{
    [SerializeField] List<PoolType> objectDatas = new List<PoolType>();

    public override void Awake()
    {
        for (int dataIdx = 0; dataIdx < objectDatas.Count; dataIdx++)
        {
            PoolType poolData = objectDatas[dataIdx];
            poolData.pools = new List<GameObject>[poolData.prefabs.Length];
            poolData.poolNames = new Dictionary<string, int>();

            for (int i = 0; i < poolData.pools.Length; i++)
            {
                poolData.pools[i] = new List<GameObject>();
            }
            
            for (int i = 0; i < poolData.pools.Length; i++)
            {
                poolData.poolNames.Add(poolData.prefabs[i].name, i);

            }

        }

    }
    public GameObject Get(PoolEnum prefabTypes, string name, Vector3 startPos, Quaternion quaternion, Transform transform = null)
    {
        GameObject select = null;

        if (transform == null)
            transform = this.transform;

        //해당 프리팹의 리스트에서 비활성화된것이 잇다면 활성화
        foreach (GameObject item in objectDatas[(int)prefabTypes].pools[objectDatas[(int)prefabTypes].poolNames[name]])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.transform.position = startPos;
                select.SetActive(true);
                break;
            }
        }
        //프리팹이 전부 활성화상태일시 생성후 리스트에 추가
        if (select == null)
        {
            select = Instantiate(objectDatas[(int)prefabTypes].prefabs[objectDatas[(int)prefabTypes].poolNames[name]], startPos, quaternion, transform);
            objectDatas[(int)prefabTypes].pools[objectDatas[(int)prefabTypes].poolNames[name]].Add(select);
        }

        return select;
    }

    public void DeactivateAllChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

}

