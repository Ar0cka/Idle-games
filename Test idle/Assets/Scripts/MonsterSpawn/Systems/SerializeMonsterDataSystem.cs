using DefaultNamespace.Battle.Components.Events.BlockAttackEvents;
using DefaultNamespace.Battle.Components.MonsterComponents;
using DefaultNamespace.MonsterSpawn.Components;
using DefaultNamespace.MonsterSpawn.Events;
using Leopotam.Ecs;
using UnityEngine;

namespace MonsterSpawn.Systems
{
    public class SerializeMonsterDataSystem : IEcsRunSystem
    {
        private EcsWorld _ecsWorld;
        private readonly EcsFilter<MonsterBattleComponents, MonsterCooldownAttackComponent> _monsterFilter = null;
        private readonly EcsFilter<SpawnSettings, SerializeMonsterEvent> _spawnFilter = null;

        public void Run()
        {
            foreach (var spawnIndex in _spawnFilter)
            {
                ref var spawnSettings = ref _spawnFilter.Get2(spawnIndex);
                ref var spawnEntity = ref _spawnFilter.GetEntity(spawnIndex);
                
                CreateNewEntity(spawnSettings.MonsterData, spawnSettings.BlockTimer, spawnSettings.MonsterData.hitPoint);
                
                Debug.Log($"Data downoland");
                
                if (spawnEntity != default)
                    spawnEntity.Del<SerializeMonsterEvent>();
            }
        }

        private void CreateNewEntity(MonstersAbstract monster, float blockTimer, int hitPoint)
        {
            var monsterEntity = _ecsWorld.NewEntity();
            
            monsterEntity.Get<MonsterBattleComponents>().monstersAbstract = monster;
            monsterEntity.Get<MonsterCooldownAttackComponent>().blockTimer = blockTimer;
            
            Debug.Log($"Create monster data entity");

            foreach (var monsterIndex in _monsterFilter)
            {
                ref var hpMonster = ref _monsterFilter.Get1(monsterIndex).currentHP;
                hpMonster = hitPoint;
            }
        }
    }
}