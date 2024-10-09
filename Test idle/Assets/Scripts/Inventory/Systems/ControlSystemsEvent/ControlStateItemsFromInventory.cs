using Inventory.Events;
using Leopotam.Ecs;
using Scriptable_object.Items;
using UnityEditorInternal.VersionControl;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.Systems
{
    public class ControlStateItemsFromInventory : IEcsRunSystem 
    {
        private InventorySettings _inventorySettings;
        private readonly EcsFilter<ItemUsedEvent> _itemFilter = null;

        public void Run()
        {
            foreach (var itemIndex in _itemFilter)
            {
                ref var slotData = ref _itemFilter.Get1(itemIndex).slotData;
                ref var entityItem = ref _itemFilter.GetEntity(itemIndex);

                var typeItem = slotData._slot.GetComponentInChildren<ItemSettings>().baseAbstractItem;

                if (typeItem.itemType == ItemType.CollectItem)
                {
                    slotData.DeleteOneItemFromColecteSlot(1);
                
                    _inventorySettings.UpdateCountToSlot(slotData);

                    if (slotData.countItemToSlot == 0)
                    {
                        slotData.DeleteItemFromSlot();
                        DeleteListenerFromUsedItem();
                        _inventorySettings.UseItemFromSlot(slotData);
                    }
                    
                }
                else if (typeItem.itemType == ItemType.BaseItem)
                {
                    DeleteListenerFromUsedItem();
                }
                entityItem.Destroy();
            }
        }

        private void DeleteListenerFromUsedItem()
        {
            foreach (var slot in _inventorySettings.GetSlotData())
            {
                if (!slot.IsOccupied && slot.isHaveListener)
                {
                    Button itemWithButtonEvent = slot._slot.GetComponentInChildren<Button>();
                    Debug.Log($"Slot name {slot._slot.name} and button = {itemWithButtonEvent.name}");
                    itemWithButtonEvent.onClick.RemoveAllListeners();
                    slot.ChangeListener(false);
                }
            }
        }
    }
}