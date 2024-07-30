using System;
using UnityEngine;


[CreateAssetMenu(fileName = "MasterStatData", menuName = "Data/MasterStatData")]


public class MasterStatData : ScriptableObject
{

    [SerializeField] StatData fighterStats;
    [SerializeField] StatData rangerStats;
    [SerializeField] StatData mageStats;
    [SerializeField] StatData thiefStats;
    [SerializeField] StatData enchanterStats;
    [SerializeField] StatData tankStats;
    [SerializeField] StatData healerStats;



    public StatData GetStatData(CombatType _type)
    {
        return _type switch
        {

            CombatType.Empty => fighterStats,
            CombatType.Fighter => fighterStats,
            CombatType.Ranger => rangerStats,
            CombatType.Mage => mageStats,
            CombatType.Tank => tankStats,
            CombatType.Thief => thiefStats,
            CombatType.Enchanter => enchanterStats,
            CombatType.Healer => healerStats,

            _ => throw new ArgumentOutOfRangeException(nameof(_type), _type, null)
        };
    }

}
