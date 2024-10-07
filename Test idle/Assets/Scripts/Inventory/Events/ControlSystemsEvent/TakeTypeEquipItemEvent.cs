using Scriptable_object.Items;

namespace Inventory.Events
{
    internal struct TakeTypeEquipItemEvent
    {
        public EquipItem equipItem;
        public SlotData slotData;
    }
}