using Unity.VisualScripting;
using UnityEngine;

namespace Inventory
{
    public class EquipSlotData
    {
        public bool isOccupied { get; private set; }
        public SlotType slotType { get; private set; }
        public GameObject slot { get; private set; }

        public EquipSlotData(GameObject _slot, SlotType _slotType)
        {
            slot = _slot;
            slotType = _slotType;
            isOccupied = false;
        }

        public void ItemIsOccupied(bool occupied)
        {
            isOccupied = occupied;
        }
    }
}