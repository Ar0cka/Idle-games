using UnityEngine;

namespace Scriptable_object.UpgradeData
{
    [CreateAssetMenu(fileName = "Upgrade data", menuName = "UpgradeStats", order = 0)]
    public class UpgradeFloatData : UpgradeEmpty
    {
        [SerializeField] private float upgradeFloatStats;

        public float GetFloatStats()
        {
            return upgradeFloatStats;
        }
    }
}