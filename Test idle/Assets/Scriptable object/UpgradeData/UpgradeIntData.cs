using UnityEngine;

namespace Scriptable_object.UpgradeData
{
    [CreateAssetMenu(fileName = "Upgrade data", menuName = "UpgradeStats", order = 0)]
    public class UpgradeIntData : UpgradeEmpty
    {
        [SerializeField] private int upgradeIntStats;

        public int GetIntStats()
        {
            return upgradeIntStats;
        }
    }
}