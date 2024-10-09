using System;
using TMPro;
using UnityEditor.Experimental;
using UnityEngine;

namespace Inventory
{
    [Serializable]
    public class SlotData
    {
        public GameObject _slot { get; set; }
        public bool isFull { get; private set; }
        public bool IsOccupied { get; private set; }

        public bool isHaveListener { get; private set; }

        public int countItemToSlot { get; private set; }

        public SlotData(GameObject slot)
        {
            _slot = slot;
            isFull = false;
            IsOccupied = false;
            isHaveListener = false;
            countItemToSlot = 0;
        }

        public void NonCollectObjectAddToInventory()
        {
            isFull = true;
            IsOccupied = true;
            countItemToSlot += 1;
        }

        public void CollectObjectAddToInventory(int countForAddItem)
        {
            IsOccupied = true;
            countItemToSlot += countForAddItem;
        }

        public void CollectAddToInventory(int countForAddItem)
        {
            countItemToSlot += countForAddItem;
            Debug.Log("Количество элексиров " + countItemToSlot);
        }

        public void DeleteOneItemFromColecteSlot(int countForDeletedItemFromSlot)
        {
            if (countItemToSlot > 0)
            {
                countItemToSlot -= countForDeletedItemFromSlot;
            }
        }

        public void DeleteItemFromSlot()
        {
            isFull = false;
            IsOccupied = false;
            countItemToSlot = 0;
        }

        public int GetCount()
        {
            return countItemToSlot;
        }

        public TextMeshProUGUI GetTextObjectFromSlot()
        {
            TextMeshProUGUI itemText = _slot.GetComponentInChildren<TextMeshProUGUI>();
            return itemText;
        }
        
        public bool CheckFullSlot(int maxCountSlot)
        {
            if (countItemToSlot == maxCountSlot)
            {
                IsOccupied = true;
                isFull = true;

                return true;
            }

            return false;
        }

        public void ChangeListener(bool isAddListener)
        {
            isHaveListener = isAddListener;
        }
    }
}