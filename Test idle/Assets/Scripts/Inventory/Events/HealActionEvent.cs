using Scriptable_object.Items;

namespace Inventory.Events
{
    public struct HealActionEvent
    {
        public BuffsItems buffsItemsData;
        public SlotData slotData;
    }
}