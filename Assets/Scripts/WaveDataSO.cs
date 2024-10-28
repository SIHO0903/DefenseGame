using UnityEngine;
using System.Collections.Generic;


public class WaveDataSO : ScriptableObject
{
    public int waveIndex;
    public int rewardMoney;
    public int rewardGem;
    public float enemyCastleHealth;
    public List<EnemyData> enemies = new List<EnemyData>();

    [System.Serializable]
    public class EnemyData
    {
        public EnemyName name;
        public float multiplier;
        public int spawnCount;
        public int spawnTime;
        public int spawnDelay;
    }


}
public enum EnemyName
{
    Orc,
    OrcRider,
    EliteOrc,
    ArmoredOrc,
    Skeleton,
    SkeletonArcher,
    ArmoredSkeleton,
    GreatswordSkeleton,
    Slime,
    Werebear,
    Werewolf
}