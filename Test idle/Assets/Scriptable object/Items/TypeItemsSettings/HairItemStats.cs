using UnityEngine;

namespace Scriptable_object.Items
{
    [CreateAssetMenu (fileName = "Hair armour", menuName = "Items/HairArmour")]
    public class HairItemStats : EquipItem
    {
        [SerializeField] private float _armour;
        [SerializeField] private float _hpBuff;

        public float armour => _armour;
        public float hp => _hpBuff;
    }
}