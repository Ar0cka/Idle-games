using Inventory.Events;
using Leopotam.Ecs;
using Scriptable_object.Items;
using UnityEditorInternal.VersionControl;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.Systems
{
    public class ControlColectedStateItemsFromInventory : IEcsRunSystem 
    {
        private InventorySettings _inventorySettings;
        private readonly EcsFilter<ColectItemUsedInInventoryEvent> _itemFilter = null;

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
                    _inventorySettings.UseItemFromSlot(slotData);
                }
                
                entityItem.Destroy();
            }
        }
    }
}