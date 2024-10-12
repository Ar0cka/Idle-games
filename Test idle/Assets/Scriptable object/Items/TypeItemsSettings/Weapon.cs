using UnityEngine;

namespace Scriptable_object.Items
{
    [CreateAssetMenu (fileName = "Weapon", menuName = "Items/Weapon")]
    public class Weapon : EquipItem
    {
        [SerializeField] private float _damage;
        [SerializeField] private float _critChance;
        [SerializeField] private float _critDamage;

        public float damage => _damage;
        public float critChance => _critChance;
        public float critDamage => _critDamage;
    }
}