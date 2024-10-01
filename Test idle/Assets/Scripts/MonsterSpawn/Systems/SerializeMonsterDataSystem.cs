using DefaultNamespace.Battle.Components.BattleComponents;
using DefaultNamespace.Battle.Components.Events.BlockAttackEvents;
using DefaultNamespace.Battle.Components.MonsterComponents;
using DefaultNamespace.ControlPhase.Components.Events;
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
        private readonly EcsFilter<HpBarComponent> _uiFilter = null;

        public void Run()
        {
            foreach (var spawnIndex in _spawnFilter)
            {
                ref var spawnSettings = ref _spawnFilter.Get2(spawnIndex);
                ref var spawnEntity = ref _spawnFilter.GetEntity(spawnIndex);

                if (spawnSettings.MonsterData != null)
                {
                    CreateNewEntity(spawnSettings.MonsterData, spawnSettings.MonsterData.hitPoint);
                    
                    OnAndUpdateUI();
                
                    Debug.Log($"Data downoland");
                
                    if (spawnEntity != default)
                        spawnEntity.Del<SerializeMonsterEvent>();
                }
                else
                {
                    Debug.LogError("Monster dont serialize");
                }
            }
        }

        private void CreateNewEntity(MonstersAbstract monster, int hitPoint)
        {
            var monsterEntity = _ecsWorld.NewEntity();
            
            monsterEntity.Get<MonsterBattleComponents>().monstersAbstract = monster;
            monsterEntity.Get<MonsterCooldownAttackComponent>().blockTimer = monster.attackSpeed;
            
            Debug.Log($"Create monster data entity where monster abstract = {monster} and block time = {monster.attackSpeed}");

            foreach (var monsterIndex in _monsterFilter)
            {
                ref var hpMonster = ref _monsterFilter.Get1(monsterIndex).currentHP;
                
                hpMonster = hitPoint;
                
                Debug.Log($"Serialize hp = {hpMonster}");
            }
        }

        private void OnAndUpdateUI()
        {
            foreach (var barIndex in _uiFilter)
            {
                ref var entityUI = ref _uiFilter.GetEntity(barIndex);
                entityUI.Get<OnHpBarEnemyEvent>();
                entityUI.Get<UpdateMonsterUIEvent>();
            }
        }
    }
}