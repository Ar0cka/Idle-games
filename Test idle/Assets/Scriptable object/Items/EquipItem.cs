using Inventory;
using UnityEngine;

namespace Scriptable_object.Items
{
    [CreateAssetMenu (fileName = "new item", menuName = "Data/Items/EquipItem")]
    public class EquipItem : BaseAbstractItem
    {
        [SerializeField] private SlotType _slotType;
        public SlotType slotType => _slotType;
    }
}