using Inventory.Events.ControlItemEquipSlot;
using Leopotam.Ecs;

namespace Inventory.Systems.ControlItemEquipSlots
{
    public class RemoveItemFromEquipSlot : IEcsRunSystem
    {
        private readonly EcsFilter<RemoveItemFromEquipSlotEvent> _deleteFilter = null;
        private InventorySettings _inventorySettings;

        public void Run()
        {
            foreach (var itemIndex in _deleteFilter)
            {
                ref var item = ref _deleteFilter.Get1(itemIndex).slotData;
                ref var entity = ref _deleteFilter.GetEntity(itemIndex);
                _inventorySettings.ReturnItemFromEquipSlot(item);
                entity.Destroy();
                
            }
        }
    }
}