using Inventory.Events;
using Leopotam.Ecs;
using Scriptable_object.Items;

namespace Inventory.Systems
{
    public class TakeActionEquipItem : IEcsRunSystem
    {
        private EcsWorld _ecsWorld;
        private readonly EcsFilter<TakeTypeEquipItemEvent> _itemFilter;

        public void Run()
        {
            foreach (var itemIndex in _itemFilter)
            {
                ref var equipItemData = ref _itemFilter.Get1(itemIndex);
                ref var entity = ref _itemFilter.GetEntity(itemIndex);

                switch (equipItemData.equipItem.slotType)
                {
                    case SlotType.HairArmour:
                        break;
                    case SlotType.Armour:
                        SendArmourEquipEvent(equipItemData.equipItem, equipItemData.slotData);
                        break;
                    case SlotType.Boots:
                        break;
                    case SlotType.Weapon:
                        break;
                    case SlotType.SecondWeapon:
                        break;
                }
                entity.Destroy();
            }
        }

        private void SendArmourEquipEvent(EquipItem equipItem, SlotData slotData)
        {
            var entityAction = _ecsWorld.NewEntity();
            var armourAction = new ArmourActionEvent()
            {
                _equipItem = equipItem,
                _slotData = slotData
            };

            entityAction.Get<ArmourActionEvent>() = armourAction;
        }
    }
}