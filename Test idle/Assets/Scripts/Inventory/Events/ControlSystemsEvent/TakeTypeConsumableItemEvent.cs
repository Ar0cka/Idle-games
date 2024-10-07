using Scriptable_object.Items;

namespace Inventory.Events
{
    public struct TakeTypeConsumableItemEvent
    {
        public BuffsItems buffsItems;
        public SlotData slotData;
    }
}