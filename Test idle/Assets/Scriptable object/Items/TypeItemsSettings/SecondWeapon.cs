using UnityEngine;

namespace Scriptable_object.Items
{
    [CreateAssetMenu (fileName = "Second weapon", menuName = "Items/SecondWeapon")]
    public class SecondWeapon : EquipItem
    {
        [SerializeField] private float _damage;
        [SerializeField] private float _critDamage;
        [SerializeField] private float _attackSpeed;

        public float damage => _damage;
        public float critDamage => _critDamage;
        public float attackSpeed => _attackSpeed;

    }
}