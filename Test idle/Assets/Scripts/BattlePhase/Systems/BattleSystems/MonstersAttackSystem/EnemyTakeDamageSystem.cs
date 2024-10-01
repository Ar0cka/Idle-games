using DefaultNamespace.Battle.Components.BattleComponents;
using DefaultNamespace.Battle.Components.Events;
using DefaultNamespace.Battle.Components.Events.AttackEvents;
using DefaultNamespace.Battle.Components.Events.BlockAttackEvents;
using DefaultNamespace.Battle.Components.MonsterComponents;
using DefaultNamespace.ControlPhase.Components.Events;
using DefaultNamespace.MonsterSpawn.Components;
using DefaultNamespace.MonsterSpawn.Events;
using Leopotam.Ecs;
using Unity.VisualScripting;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace DefaultNamespace.Battle.System.BattleSystems.MonstersAttackSystem
{
    sealed class EnemyTakeDamageSystem : IEcsRunSystem
    {
        private readonly EcsFilter<MonsterBattleComponents, PlayerAttackEvent> _monsterFilter = null;
        private readonly EcsFilter<SpawnSettings> _spawnFilter = null;
        private readonly EcsFilter<HpBarComponent> _hpBarFilter = null;

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
                    
                    foreach (var barIndex in _hpBarFilter)
                    {
                        ref var barEntity = ref _hpBarFilter.GetEntity(barIndex);
                        barEntity.Get<UpdateMonsterUIEvent>();
                    }
                    
                    entity.Del<PlayerAttackEvent>();
                }

                if (monster.currentHP < 0)
                {
                    foreach (var spawnIndex in _spawnFilter)
                    {
                        ref var spawnEntity = ref _spawnFilter.GetEntity(spawnIndex);

                        spawnEntity.Get<DestroyEnemyEvent>();
                    }
                }
            }
        }
        
    }
}