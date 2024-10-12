using Inventory.Events;
using Leopotam.Ecs;
using Scriptable_object.Items;
using UnityEngine;

namespace Inventory.Systems
{
    public class TakeActionEquipItem : IEcsRunSystem
    {
        private EcsWorld _ecsWorld;
        private readonly EcsFilter<TakeTypeEquipItemEvent> _itemFilter;
        private InventorySettings _inventorySettings;
        private InventoryEquip _inventoryEquip;

        public void Run()
        {
            foreach (var itemIndex in _itemFilter)
            {
                ref var equipItemData = ref _itemFilter.Get1(itemIndex);
                ref var entity = ref _itemFilter.GetEntity(itemIndex);

                TakeItemToSlot(equipItemData.equipItem, equipItemData.slotData);
                
                SendArmourEquipEvent(equipItemData.equipItem);
                
                entity.Destroy();
            }
        }

        private void SendArmourEquipEvent(EquipItem equipItem)
        {
            var entityAction = _ecsWorld.NewEntity();
        
            entityAction.Get<ArmourActionEvent>()._equipItem = equipItem;
        }

        private void TakeItemToSlot(EquipItem equipItem, SlotData slotData)
        {
            if (!_inventoryEquip.GetSlotData(equipItem.slotType).isOccupied)
            {
                var ecsEntity = _ecsWorld.NewEntity();
                var item = _inventorySettings.GetItemFromSlot(slotData);
                _inventoryEquip.AddNewItemToEquipSlot(equipItem.slotType, ecsEntity, slotData);

                Debug.Log("Add qeuip item");
            }

            else if (_inventoryEquip.GetSlotData(equipItem.slotType).isOccupied)
            {
                var ecsEntity = _ecsWorld.NewEntity();
                _inventoryEquip.ChangeItemToEquipSlot(equipItem.slotType, slotData, ecsEntity);

                Debug.Log("Change item");
            }
            
        }
    }
}