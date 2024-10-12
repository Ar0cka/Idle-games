using UnityEngine;

namespace Scriptable_object.Items
{
    [CreateAssetMenu (fileName = "Armour", menuName = "Items/Armour")]
    public class Armour : EquipItem
    {
        [SerializeField] private float _armour;
        public float armour => _armour;
    }
}