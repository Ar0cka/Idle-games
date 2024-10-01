using DefaultNamespace.Battle.Components.BattleComponents;
using DefaultNamespace.Battle.Components.MonsterComponents;
using DefaultNamespace.ControlPhase.Components.Events;
using DefaultNamespace.MonsterSpawn.Components;
using DefaultNamespace.MonsterSpawn.Events;
using Leopotam.Ecs;
using UnityEngine;

namespace MonsterSpawn.Systems.DestroyMonster
{
    public class DestroyMonster : IEcsRunSystem
    {
        private readonly EcsFilter<MonsterBattleComponents> _monsterFilter = null;
        private readonly EcsFilter<SpawnSettings, DestroyEnemyEvent> _spawnFilter = null;
        private readonly EcsFilter<MonsterCheckStateComponent> _stateFilter = null;
        private readonly EcsFilter<HpBarComponent, HideHpBarEnemyEvent> _barFilter = null;

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
                    
                    Debug.Log($"Monster deleted {monsterEntity}");
                    
                    spawnEntity.Del<DestroyEnemyEvent>();
                    spawnEntity.Get<RespawnMonsterEvent>();
                        
                    Debug.Log($"Send respawn event");

                }
            }
        }

        private void ChangeStateMonster()
        {
            var stateMonster = _stateFilter.GetEntitiesCount() > 0 ? _stateFilter.Get1(0) : default;

            stateMonster.MonsterAlive = false;
        }

        private void HideHpBarEnemy()
        {
            var hpBarEntity = _barFilter.GetEntitiesCount() > 0 ? _barFilter.GetEntity(0) : default;

            if (hpBarEntity != default)
                hpBarEntity.Get<HideHpBarEnemyEvent>();
        }
    }
}