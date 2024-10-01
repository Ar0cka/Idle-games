using DefaultNamespace.Battle.Components.BattleComponents;
using DefaultNamespace.Battle.Components.Events;
using DefaultNamespace.Battle.Components.Events.BlockAttackEvents;
using DefaultNamespace.Battle.Components.MonsterComponents;
using DefaultNamespace.Components;
using DefaultNamespace.ControlPhase.Components.Events;
using Leopotam.Ecs;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using NotImplementedException = System.NotImplementedException;

namespace DefaultNamespace.Battle.System
{
    public class MonsterUpdateHpBarSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HpBarComponent, UpdateMonsterUIEvent> _uiFilter = null;

        private readonly EcsFilter<MonsterBattleComponents> _monsterFilter = null;

        public void Run()
        {
            foreach (var monsterIndex in _monsterFilter)
            {
                ref var _monsterHp = ref _monsterFilter.Get1(monsterIndex).currentHP;
                
                    
                foreach (var uiIndex in _uiFilter)
                {
                    ref var _monsterBar = ref _uiFilter.Get1(uiIndex)._monsterBar;
                    ref var entity = ref _uiFilter.GetEntity(monsterIndex);

                    Debug.Log("UpdateMonsterUI");
                    _monsterBar.text = _monsterHp.ToString();
                    entity.Del<UpdateMonsterUIEvent>();
                }
            }
        }
    }
}