using System;
using DefaultNamespace.Battle.Components.BattleComponents;
using DefaultNamespace.Components;
using DefaultNamespace.ControlPhase.Components.Events;
using Inventory.Events;
using Leopotam.Ecs;
using UnityEngine;

namespace Inventory.Systems
{
    public class HealFromPotionSystems : IEcsRunSystem
    {
        private EcsWorld _ecsWorld;
        private readonly EcsFilter<HealActionEvent> _healFilter = null;
        private readonly EcsFilter<PlayerSettingsComponent> _playerFilter;
        private readonly EcsFilter<HpBarComponent> _barFilter;
        private InventorySettings _inventorySettings;

        public void Run()
        {
            foreach (var healIndex in _healFilter)
            {
                ref var healData = ref _healFilter.Get1(healIndex);
                ref var healEntity = ref _healFilter.GetEntity(healIndex);

                foreach (var playerIndex in _playerFilter)
                {
                    ref var player = ref _playerFilter.Get1(playerIndex);

                    float amountNeedHeal = player.playerSettings._hitPoint - player.currentHP;
                    float healAmount = Math.Min(amountNeedHeal, healData.buffsItemsData.quantityBuff);

                    Debug.Log($"heal amount = {healAmount}");
                    
                    player.currentHP += healAmount;
                    
                    UpdatePlayerUI();
                    
                    SendItemUsedEvent(healData.slotData);
                    
                    Debug.Log($"player hp = {player.currentHP}");
                    
                    healEntity.Destroy();
                }
            }
        }

        private void SendItemUsedEvent(SlotData slotData)
        {
            var entity = _ecsWorld.NewEntity();
            entity.Get<ColectItemUsedInInventoryEvent>().slotData = slotData;
        }
        
        private void UpdatePlayerUI()
        {
            foreach (var barIndex in _barFilter)
            {
                ref var barEntity = ref _barFilter.GetEntity(barIndex);
                barEntity.Get<UpdatePlayerUIEvent>();
            }
        }
    }
}