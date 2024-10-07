using Scriptable_object.Items;

namespace Inventory.Events
{
    internal struct TakeActionEvent
    {
        public BaseAbstractItem baseAbstract;
        public SlotData slotData;
    }
}