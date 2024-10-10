using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Scriptable_object.Items;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Inventory
{
    public class InventorySettings : MonoBehaviour
    {
        [SerializeField] private List<GameObject> slotFromScene;
        public List<GameObject> _slotFromScene => slotFromScene;
        private List<SlotData> _slots = new List<SlotData>();

        private void Awake()
        {
            foreach (var slot in slotFromScene)
            {
                _slots.Add(new SlotData(slot));
            }
        }
        
        public void AddNewNonCollectItemToList(GameObject itemForSlot)
        {
            for (int i = 0; i < _slots.Count; i++)
            {
                bool isFull = _slots[i].isFull;
                bool isOccupied = _slots[i].IsOccupied;
                
                if (!isFull && !isOccupied)
                {
                    Instantiate(itemForSlot, _slots[i]._slot.transform);
                    _slots[i].NonCollectObjectAddToInventory();
                    UpdateCountToSlot(_slots[i]);
                    break;
                }
            }
        }

        public void AddCollectedItem(GameObject itemForSlot)
        {
            for (int i = 0; i < _slots.Count; i++)
            {
                bool isFull = _slots[i].isFull;
                bool isOccupied = _slots[i].IsOccupied;
                var itemData = itemForSlot.GetComponent<ItemSettings>().baseAbstractItem;

                if (itemData is CollectedItems collectedItems)
                {
                    if (!isFull && !isOccupied)
                    {
                        CreateItemToCollectSlot(itemForSlot, _slots[i]._slot.transform, collectedItems);
                        _slots[i].CollectObjectAddToInventory(1);
                        UpdateCountToSlot(_slots[i]);
                        break;
                    } 
                    if (isOccupied && _slots[i]._slot.GetComponentInChildren<ItemSettings>().baseAbstractItem.nameItem == itemData.nameItem)
                    {
                        if (!_slots[i].CheckFullSlot(collectedItems.countItemForStack))
                        {
                            _slots[i].CollectAddToInventory(1);
                            UpdateCountToSlot(_slots[i]);
                            break;
                        }
                    }
                }
            }
        }

        public void UseItemFromSlot(SlotData slotData)
        {
            if (slotData.countItemToSlot == 0)
            {
                GameObject gameObject = slotData._slot.GetComponentInChildren<ItemSettings>().gameObject;
                Destroy(gameObject);
            }
        }
        
        private void CreateItemToCollectSlot(GameObject itemForSlot, Transform transforForSpawn, CollectedItems itemData)
        {
             Instantiate(itemForSlot, transforForSpawn);
        }

        public GameObject GetItemFromSlot(SlotData _slotData)
        { 
            GameObject item = _slotData._slot.GetComponentInChildren<ItemSettings>().gameObject;
            return item;
        }
        
        public List<SlotData> GetSlotData()
        {
            return _slots;
        }

        public void UpdateCountToSlot(SlotData slotData)
        {
            if (slotData != null)
            {
                TextMeshProUGUI count = slotData.GetTextObjectFromSlot();
                if (count != null)
                {
                    if (slotData._slot.GetComponentInChildren<ItemSettings>().baseAbstractItem is CollectedItems collectedItems)
                    {
                        count.text = $"{slotData.countItemToSlot}/{collectedItems.countItemForStack}";
                    }
                    else if (slotData._slot.GetComponentInChildren<ItemSettings>().baseAbstractItem is EquipItem equipItem)
                    {
                        count.text = $"{slotData.countItemToSlot}";
                    }
                }
            }
            else
            {
                Debug.LogError("SlotData = null");
            }
        }
    }
}