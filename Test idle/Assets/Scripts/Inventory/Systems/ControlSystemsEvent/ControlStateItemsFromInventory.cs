using Inventory.Events;
using Leopotam.Ecs;
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
                
                slotData.DeleteOneItemFromColecteSlot(1);
                
                _inventorySettings.UpdateCountToSlot(slotData);

                if (slotData.countItemToSlot == 0)
                {
                    slotData.DeleteItemFromSlot();
                    DeleteListenerFromUsedItem();
                    _inventorySettings.UseItemFromSlot(slotData);
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