using System;
using Inventory.Events;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.Systems
{
    public class ControlBaseStateItems : IEcsRunSystem
    {
        private readonly EcsFilter<BaseItemUseEvent> _itemFilter = null;
        private InventorySettings _inventorySettings;

        public void Run()
        {
            foreach (var itemIndex in _itemFilter)
            {
                ref var slotData = ref _itemFilter.Get1(itemIndex);
                ref var entity = ref _itemFilter.GetEntity(itemIndex);
                
                slotData.slotData.DeleteItemFromSlot();
                entity.Destroy();
            }
        }
    }
}