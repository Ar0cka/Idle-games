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

        public void AddNewItemToEquipSlot(SlotType slotType, EcsEntity _ecsEntity, SlotData slotData)
        {
            foreach (var slot in _slotData)
            {
                if (!slot.isOccupied && slotType == slot.slotType)
                {
                    // Перенос предмета в эквип. Нам нужно достать нужный объект и перенести его в Equip при этом, поменять состояние слота, в котором находился предмет, будто бы он удален.
                    var item = slotData._slot.GetComponentInChildren<ItemSettings>().gameObject;
                    Transform slotTransform = slot.slot.transform;

                    var itemSlotSettings = new BaseItemUseEvent()
                    {
                        _item = item,
                        slotData = slotData
                    };
                    
                    _ecsEntity.Get<BaseItemUseEvent>() = itemSlotSettings;
                    Debug.Log("Ecs entity get BaseItemUseEvent");
                    
                    item.transform.SetParent(slotTransform, false); 
                    slot.ItemIsOccupied(true);
                    break;
                }
            }
        }

        public void ChangeItemToEquipSlot(SlotType slotType, SlotData slotData, EcsEntity _ecsEntity)
        {
            foreach (var slot in _slotData)
            {
                if (slot.isOccupied && slotType == slot.slotType)
                {
                    var item = GetItemFromEquipSlot(slot);

                    if (item.GetComponentInChildren<ItemSettings>().baseAbstractItem is EquipItem equipItem)
                    {
                        _inventorySettings.ReturnItemFromEquipSlot(slot, _ecsEntity);
                        AddNewItemToEquipSlot(equipItem.slotType, _ecsEntity, slotData);
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