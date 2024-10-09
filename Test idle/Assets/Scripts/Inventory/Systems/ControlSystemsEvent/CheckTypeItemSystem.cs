using Inventory.Events;
using Leopotam.Ecs;
using Scriptable_object.Items;
using UnityEngine;

namespace Inventory.Systems
{
    public class CheckTypeItemSystem : IEcsRunSystem
    {
        private EcsWorld _ecsWorld;
        private readonly EcsFilter<TakeActionEvent> _takeAction = null;
        
        public void Run()
        {
            foreach (var index in _takeAction)
            {
                ref var takeAction = ref _takeAction.Get1(index);
                ref var entity = ref _takeAction.GetEntity(index);

                var typeEntity = _ecsWorld.NewEntity();
                
                switch (takeAction.baseAbstract.itemCategory)
                {
                    case ItemCategory.Consumable:
                        if (takeAction.baseAbstract is BuffsItems buffsItems)
                        {
                            var takeType = new TakeTypeConsumableItemEvent()
                            {
                                slotData = takeAction.slotData,
                                buffsItems = buffsItems
                            };
                            
                            typeEntity.Get<TakeTypeConsumableItemEvent>() = takeType;
                            entity.Destroy();
                        }
                        break;
                    case ItemCategory.Equipment:
                        if (takeAction.baseAbstract is EquipItem equipItem)
                        {
                            var takeType = new TakeTypeEquipItemEvent()
                            {
                                equipItem = equipItem,
                                slotData = takeAction.slotData
                            };
                            
                            typeEntity.Get<TakeTypeEquipItemEvent>() = takeType;
                            entity.Destroy();
                        }
                        break;
                }
            }
        }
    }
}