using DefaultNamespace.Battle.Components.BattleComponents;
using DefaultNamespace.Battle.Components.Events.BlockAttackEvents.SpawnMonsters;
using DefaultNamespace.ControlPhase.Components.Events;
using Leopotam.Ecs;
using NotImplementedException = System.NotImplementedException;

namespace DefaultNamespace.Battle.System
{
    public class HideHpBarEnemySystem : IEcsRunSystem
    {
        private EcsFilter<CheckStateMonster, HideHpBarEnemyEvent> _monsterState = null;
        private EcsFilter<HpBarComponent> _hpBarFilter = null;

        public void Run()
        {
            foreach (var stateIndex in _monsterState)
            {
                ref var isMonsterAlive = ref _monsterState.Get1(stateIndex).isMonsterAlive;
                ref var stateEntity = ref _monsterState.GetEntity(stateIndex);

                foreach (var barIndex in _hpBarFilter)
                {
                    ref var enemyHpBar = ref _hpBarFilter.Get1(barIndex)._monsterBar;
                    if (!isMonsterAlive)
                    {
                        enemyHpBar.gameObject.SetActive(false);
                    }
                }
                
               stateEntity.Del<HideHpBarEnemyEvent>();
            }
            
          
        }
    }
}