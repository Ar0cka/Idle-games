using DefaultNamespace.Battle.Components.BattleComponents;
using DefaultNamespace.Battle.Components.Events.BlockAttackEvents.SpawnMonsters;
using DefaultNamespace.ControlPhase.Components.Events;
using Leopotam.Ecs;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace DefaultNamespace.Battle.System
{
    public class OnHpBarEnemySystem : IEcsRunSystem
    {
        private EcsFilter<CheckStateMonster> _monsterState = null;
        private EcsFilter<HpBarComponent, OnHpBarEnemyEvent> _hpBarFilter = null;

        public void Run()
        {
            foreach (var barIndex in _hpBarFilter)
            {
                ref var enemyHpBar = ref _hpBarFilter.Get1(barIndex)._monsterBar;
                ref var entityBar = ref _hpBarFilter.GetEntity(barIndex);

                foreach (var stateIndex in _monsterState)
                {
                    ref var isMonsterAlive = ref _monsterState.Get1(stateIndex).isMonsterAlive;

                    if (isMonsterAlive)
                    {
                        enemyHpBar.gameObject.SetActive(true);
                    }
                    entityBar.Del<OnHpBarEnemyEvent>();
                }
            }
        }
    }
}