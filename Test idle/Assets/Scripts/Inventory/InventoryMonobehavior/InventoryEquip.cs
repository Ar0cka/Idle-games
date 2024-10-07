using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class InventoryEquip : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _slots = new List<GameObject>(5);
        private List<EquipSlotData> _slotData = new List<EquipSlotData>();

        private void Awake()
        {
            _slotData.Add(new EquipSlotData(_slots[0], SlotType.HairArmour));
            _slotData.Add(new EquipSlotData(_slots[1], SlotType.Armour));
            _slotData.Add(new EquipSlotData(_slots[2], SlotType.Boots));
            _slotData.Add(new EquipSlotData(_slots[3], SlotType.Weapon));
            _slotData.Add(new EquipSlotData(_slots[4], SlotType.SecondWeapon));
        }

        public void AddNewItemToEquipSlot(GameObject itemInEquip, SlotType slotType)
        {
            foreach (var slot in _slotData)
            {
                if (!slot.isOccupied && slotType == slot.slotType)
                {
                    slot.onSlotGameObject = Instantiate(itemInEquip, slot.slot.transform);
                    slot.ItemIsOccupied(true);
                    break;
                }
            }
        }

        public void ChangeItemToEquipSlot(GameObject itemInEquip,  SlotType slotType, Transform inventorySlot)
        {
            foreach (var slot in _slotData)
            {
                if (slot.isOccupied && slotType == slot.slotType)
                {
                    Instantiate(slot.onSlotGameObject, inventorySlot);
                    Destroy(slot.onSlotGameObject);
                    slot.onSlotGameObject = Instantiate(itemInEquip, slot.slot.transform);
                }
            }
        }

        public void DeleteItemFromEquipSlot(GameObject item, SlotType slotType)
        {
            foreach (var slot in _slotData)
            {
                if (slot.isOccupied && slotType == slot.slotType)
                {
                    Destroy(slot.onSlotGameObject);
                    slot.ItemIsOccupied(false);
                }
            }
        }
    }
}