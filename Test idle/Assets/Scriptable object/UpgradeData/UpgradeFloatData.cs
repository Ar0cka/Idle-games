using DefaultNamespace.UpgradeMenu;
using Unity.VisualScripting;
using UnityEngine;

namespace Scriptable_object.UpgradeData
{
    [CreateAssetMenu(fileName = "Upgrade data", menuName = "UpgradeStatsFloat", order = 0)]
    public class UpgradeFloatData : ScriptableObject
    {
        [SerializeField] private StatesType _statesType;
        public StatesType statesType => _statesType;
        
        [SerializeField] private float _upgradeFloatStats;
        public float upgradeFloatStats => _upgradeFloatStats;

    }
}