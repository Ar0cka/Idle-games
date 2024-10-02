using DefaultNamespace.Battle.Components.BattleComponents;
using DefaultNamespace.Battle.Components.MonsterComponents;
using DefaultNamespace.ControlPhase.Components.Events;
using DefaultNamespace.MonsterSpawn.Components;
using DefaultNamespace.MonsterSpawn.Events;
using Leopotam.Ecs;
using UnityEditor;
using UnityEngine;

namespace MonsterSpawn.Systems.DestroyMonster
{
    public class DestroyMonsterOfTheRun : IEcsRunSystem
    {
        private EcsFilter<SpawnSettings, DestroyMonsterOfTheRunFromBattleEvent> _spawnFilter;
        private readonly EcsFilter<MonsterBattleComponents> _monsterFilter = null;
        private readonly EcsFilter<MonsterCheckStateComponent> _stateFilter = null;
        private readonly EcsFilter<HpBarComponent> _barFilter = null;

        public void Run()
        {
            foreach (var stateIndex in _stateFilter)
            {
                ref var stateSettings = ref _stateFilter.Get1(stateIndex);
                
                foreach (var monsterIndex in _monsterFilter)
                {
                    ref var monsterEntity = ref _monsterFilter.GetEntity(monsterIndex);

                    foreach (var spawnIndex in _spawnFilter)
                    {
                        ref var spawnSettings = ref _spawnFilter.Get1(spawnIndex);
                        ref var spawnEntity = ref _spawnFilter.GetEntity(spawnIndex);
                        
                        if (stateSettings.MonsterAlive)
                        {
                            Destroy(ref monsterEntity, ref spawnEntity, spawnSettings);
                            stateSettings.MonsterAlive = false;
                        }
                        else
                        {
                            spawnEntity.Del<DestroyMonsterOfTheRunFromBattleEvent>();
                        }
                    }
                }
            }
        }
        
        private void HideHpBarEnemy()
        {
            var barEntity = _barFilter.GetEntitiesCount() > 0 ? _barFilter.GetEntity(0) : default;
            
            if(barEntity != default) 
                barEntity.Get<HideHpBarEnemyEvent>();
        }

        private void Destroy(ref EcsEntity monsterEntity, ref EcsEntity spawnEntity, SpawnSettings spawnSettings )
        {
            spawnSettings.monsterSpawnScript.DestroyMonster();
            monsterEntity.Destroy();
            HideHpBarEnemy();
                    
            spawnEntity.Del<DestroyMonsterOfTheRunFromBattleEvent>();
        }
    }
}