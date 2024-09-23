using DefaultNamespace.Battle.Components.BattleComponents;
using DefaultNamespace.Battle.Components.Events;
using DefaultNamespace.Battle.Components.Events.BlockAttackEvents;
using DefaultNamespace.Battle.Components.MonsterComponents;
using DefaultNamespace.Components;
using Leopotam.Ecs;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using NotImplementedException = System.NotImplementedException;

namespace DefaultNamespace.Battle.System
{
    public class MonsterUpdateHpBarSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HpBarComponent> _uiFilter = null;

        private readonly EcsFilter<MonsterBattleComponents, UpdateUIEvent> _monsterFilter = null;

        public void Run()
        {
            foreach (var monsterIndex in _monsterFilter)
            {
                ref var _monsterHp = ref _monsterFilter.Get1(monsterIndex).currentHP;
                ref var entity = ref _monsterFilter.GetEntity(monsterIndex);
                    
                foreach (var uiIndex in _uiFilter)
                {
                    ref var _monsterBar = ref _uiFilter.Get1(uiIndex)._monsterBar;

                    _monsterBar.text = _monsterHp.ToString();
                    entity.Del<UpdateUIEvent>();
                }
            }
        }
    }
}