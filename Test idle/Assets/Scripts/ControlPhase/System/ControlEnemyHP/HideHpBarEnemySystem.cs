﻿using DefaultNamespace.Battle.Components.BattleComponents;
using DefaultNamespace.ControlPhase.Components.Events;
using Leopotam.Ecs;
using UnityEngine;

namespace DefaultNamespace.ControlPhase.System.ControlEnemyHP
{
    public class HideHpBarEnemySystem : IEcsRunSystem
    {
        private readonly EcsFilter<HpBarComponent, HideHpBarEnemyEvent> _hpBarFilter = null;

        public void Run()
        {
            foreach (var barIndex in _hpBarFilter)
            {
                ref var enemyBar = ref _hpBarFilter.Get1(barIndex);
                ref var barEntity = ref _hpBarFilter.GetEntity(barIndex);
                
                enemyBar._monsterBar.gameObject.SetActive(false);
                
                Debug.Log($"Hide hp bar enemy");
                
                barEntity.Del<HideHpBarEnemyEvent>();
            }
        }
    }
}