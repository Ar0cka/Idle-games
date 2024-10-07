using Inventory.Events;
using Leopotam.Ecs;
using Scriptable_object.Items;

namespace Inventory.Systems
{
    public class TakeActionBuffItem : IEcsRunSystem
    {
        private EcsWorld _ecsWorld;
        private readonly EcsFilter<TakeTypeConsumableItemEvent> _consumableFilter;

        public void Run()
        {
            foreach (var itemIndex in _consumableFilter)
            {
                ref var itemData = ref _consumableFilter.Get1(itemIndex);
                ref var entity = ref _consumableFilter.GetEntity(itemIndex);

                switch (itemData.buffsItems.consumableItem)
                {
                    case ConsumableItemType.Heal:
                        SendHealEvent(itemData.slotData, itemData.buffsItems);
                        entity.Destroy();
                        break;
                    case ConsumableItemType.DamageBuff:
                        break;
                }
            }
        }

        private void SendHealEvent(SlotData _slotData, BuffsItems buffsItems)
        {
            var typeEntity = _ecsWorld.NewEntity();
            
            var healActionEvent = new HealActionEvent()
            {
                slotData = _slotData,
                buffsItemsData = buffsItems
            };

            typeEntity.Get<HealActionEvent>() = healActionEvent;
        }
    }
}