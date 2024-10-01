using DefaultNamespace.Battle.Components.BattleComponents;
using DefaultNamespace.Battle.Components.MonsterComponents;
using DefaultNamespace.ControlPhase.Components.Events;
using DefaultNamespace.MonsterSpawn.Components;
using DefaultNamespace.MonsterSpawn.Events;
using Leopotam.Ecs;
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
            foreach (var monsterIndex in _monsterFilter)
            {
                ref var monsterEntity = ref _monsterFilter.GetEntity(monsterIndex);

                foreach (var spawnIndex in _spawnFilter)
                {
                    ref var spawnSettings = ref _spawnFilter.Get1(spawnIndex);
                    ref var spawnEntity = ref _spawnFilter.GetEntity(spawnIndex);

                    spawnSettings.monsterSpawnScript.DestroyMonster();
                    monsterEntity.Destroy();
                    ChangeStateMonster();
                    HideHpBarEnemy();
                    
                    spawnEntity.Del<DestroyMonsterOfTheRunFromBattleEvent>();
                }
            }
        }
        private void ChangeStateMonster()
        {
            foreach (var stateIndex in _stateFilter)
            {
                ref var stateMonster = ref _stateFilter.Get1(stateIndex);

                stateMonster.MonsterAlive = false;
            }
        }
        private void HideHpBarEnemy()
        {
            var barEntity = _barFilter.GetEntitiesCount() > 0 ? _barFilter.GetEntity(0) : default;
            
            if(barEntity != default) 
                barEntity.Get<HideHpBarEnemyEvent>();
        }
    }
}