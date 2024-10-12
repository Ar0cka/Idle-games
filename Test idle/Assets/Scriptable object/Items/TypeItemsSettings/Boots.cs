using UnityEngine;

namespace Scriptable_object.Items
{
    [CreateAssetMenu (fileName = "Boots", menuName = "Items/Boots")]
    public class Boots : EquipItem
    {
        [SerializeField] private float _attackSpeed;
        [SerializeField] private float _armour;
        [SerializeField] private float _critDamage;

        public float attackSpeed => _attackSpeed;
        public float armour => _armour;
        public float critDamage => critDamage;
    }
}