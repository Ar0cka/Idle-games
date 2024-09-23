using DefaultNamespace.Battle.Components.BattleComponents;
using DefaultNamespace.Battle.Components.Events;
using DefaultNamespace.Battle.Components.Events.AttackEvents;
using DefaultNamespace.Battle.Components.Events.BlockAttackEvents;
using DefaultNamespace.Battle.Components.Events.BlockAttackEvents.SpawnMonsters;
using DefaultNamespace.Battle.Components.MonsterComponents;
using DefaultNamespace.BattlePhase.Components.Events.SpawnEvents;
using Leopotam.Ecs;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace DefaultNamespace.Battle.System.BattleSystems.MonstersAttackSystem
{
    sealed class EnemyTakeDamageSystem : IEcsRunSystem
    {
        private readonly EcsFilter<MonsterBattleComponents, PlayerAttackEvent> _monsterFilter = null;

        private readonly EcsFilter<SpawnSettings> _spawnFilter = null;

        private readonly EcsFilter<CheckStateMonster> _monsterStateFilter = null;

        public void Run()
        {
            foreach (var i in _monsterFilter)
            {
                ref var monster = ref _monsterFilter.Get1(i);
                ref var attackEvent = ref _monsterFilter.Get2(i);
                ref var entity = ref _monsterFilter.GetEntity(i);

                if (attackEvent.damagePlayer > 0)
                {
                    monster.currentHP -= attackEvent.damagePlayer;
                    attackEvent.damagePlayer = 0;
                    entity.Get<UpdateUIEvent>();
                    entity.Del<PlayerAttackEvent>();
                }

                if (monster.currentHP <= 0)
                {
                    foreach (var stateIndex in _monsterStateFilter)
                    {
                        ref var monsterState = ref _monsterStateFilter.Get1(stateIndex);

                        entity.Destroy();
                        MonsterDeadActions();
                        
                        monsterState.isMonsterAlive = false;
                        monsterState.CanRespawn = true;
                    }
                }
            }
        }

        private void MonsterDeadActions()
        {
            var spawnEntity = _spawnFilter.GetEntitiesCount() > 0 ? _spawnFilter.GetEntity(0) : default;

            if (spawnEntity != default)
            {
                spawnEntity.Get<DestroyMonsterEvent>();
                spawnEntity.Get<RespawnEvent>();
            }
        }
    }
}