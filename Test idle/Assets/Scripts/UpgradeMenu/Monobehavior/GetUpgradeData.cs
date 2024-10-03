using Scriptable_object.UpgradeData;
using UnityEngine;

namespace DefaultNamespace.UpgradeMenu.Monobehavior
{

    public class GetUpgradeData : MonoBehaviour
    {
        [SerializeField] private UpgradeEmpty upgradeData;

        private UpgradeEmpty GetData()
        {
            return upgradeData;
        }
    }
}