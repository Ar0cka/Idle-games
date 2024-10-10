using UnityEngine;

namespace Inventory.Events
{
    public struct BaseItemUseEvent
    {
        public SlotData slotData;
        public GameObject _item;
    }
}