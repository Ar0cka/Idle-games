using System;
using System.Collections.Generic;
using System.Linq;
using Inventory.Events;
using Leopotam.Ecs;
using Scriptable_object.Items;
using UnityEngine;

namespace Inventory
{
    public class InventoryEquip : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _slots = new List<GameObject>(5);
        [SerializeField] private InventorySettings _inventorySettings;
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
                    Instantiate(itemInEquip, slot.slot.transform);
                    Destroy(itemInEquip);
                    slot.ItemIsOccupied(true);
                    break;
                }
            }
        }

        public void ChangeItemToEquipSlot(GameObject itemInEquip,  SlotType slotType, SlotData slotData)
        {
            foreach (var slot in _slotData)
            {
                if (slot.isOccupied && slotType == slot.slotType)
                {
                    var item = GetItemFromEquipSlot(slot);

                    if (slotData._slot.GetComponentInChildren<ItemSettings>().baseAbstractItem is EquipItem equipItem)
                    {
                        Destroy(_inventorySettings.GetItemFromSlot(slotData));
                        _inventorySettings.AddNewNonCollectItemToList(item);
                        Destroy(GetItemFromEquipSlot(slot));
                        AddNewItemToEquipSlot(_inventorySettings.GetItemFromSlot(slotData), equipItem.slotType);
                        
                    }
                    break;
                }
            }
        }

        public void DeleteItemFromEquipSlot(GameObject item, SlotType slotType)
        {
            foreach (var slot in _slotData)
            {
                if (slot.isOccupied && slotType == slot.slotType)
                {
                    Destroy(GetItemFromEquipSlot(slot));
                    slot.ItemIsOccupied(false);
                }
            }
        }

        public GameObject GetItemFromEquipSlot(EquipSlotData slotData)
        {
            GameObject item = slotData.slot.GetComponentInChildren<ItemSettings>().gameObject;
            return item != null ? item.gameObject : null;
        }

        public EquipSlotData GetSlotData(SlotType slotType)
        {
            return _slotData.FirstOrDefault(slot => slot.slotType == slotType);
        }
    }
}