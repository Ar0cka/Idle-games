using Scriptable_object.Items;
using UnityEngine;

namespace Inventory
{
    public class DropLifePotion : MonoBehaviour
    {
        [SerializeField] private GameObject _itemForDrop;
        [SerializeField] private InventorySettings _inventorySettings;

        public void DropItem()
        {
            var ItemType = _itemForDrop.GetComponent<ItemSettings>().baseAbstractItem.itemType;

            switch (ItemType)
            {
                case ItemType.BaseItem:
                    _inventorySettings.AddNewNonCollectItemToList(_itemForDrop);
                    break;
                case ItemType.CollectItem:
                    _inventorySettings.AddCollectedItem(_itemForDrop);
                    break;
            }
        }
    }
}