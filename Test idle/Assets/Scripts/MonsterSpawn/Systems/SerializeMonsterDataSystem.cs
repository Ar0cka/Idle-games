using DefaultNamespace.Battle.Components.Events.BlockAttackEvents;
using DefaultNamespace.Battle.Components.MonsterComponents;
using DefaultNamespace.MonsterSpawn.Components;
using DefaultNamespace.MonsterSpawn.Events;
using Leopotam.Ecs;

namespace MonsterSpawn.Systems
{
    public class SerializeMonsterDataSystem : IEcsRunSystem
    {
        private EcsWorld _ecsWorld;
        private readonly EcsFilter<MonsterBattleComponents, MonsterCooldownAttackComponent> _monsterFilter = null;
        private readonly EcsFilter<SpawnSettings, SerializeMonsterEvent> _spawnFilter = null;

        public void Run()
        {
            CreateNewEntity();

            var monster = _monsterFilter.GetEntitiesCount() > 0 ? _monsterFilter.Get1(0) : default;

            if (_monsterFilter.GetEntitiesCount() > 0)
                monster.currentHP = monster.monstersAbstract.hitPoint;
        }

        private void CreateNewEntity()
        {
            var monsterEntity = _ecsWorld.NewEntity();

            foreach (var spawnIndex in _spawnFilter)
            {
                ref var monsterData = ref _spawnFilter.Get2(spawnIndex);

                monsterEntity.Get<MonsterBattleComponents>().monstersAbstract = monsterData.MonsterData;
                monsterEntity.Get<MonsterCooldownAttackComponent>().blockTimer = monsterData.BlockTimer;
            }
        }
    }
}