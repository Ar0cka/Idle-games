using DefaultNamespace.MonsterSpawn.Components;
using DefaultNamespace.MonsterSpawn.Events;
using Leopotam.Ecs;
using UnityEngine;


namespace MonsterSpawn.Systems
{
    public class TakeMonsterFromListSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsWorld _ecsWorld;
        private readonly EcsFilter<MonsterFirstFloorComponent, SpawnSettings, ChoiceMonsterFromListEvent> _monsterListFilter = null;
        private readonly EcsFilter<MonsterForSpawnComponent> _monsterForSpawnFilter = null;

        public void Init()
        {
            var monsterForSpawn = _ecsWorld.NewEntity();
            monsterForSpawn.Get<MonsterForSpawnComponent>();
            Debug.Log($"Monster entity creat");
        }

        public void Run()
        {
            foreach (var monsterListIndex in _monsterListFilter)
            {
                ref var monsterList = ref _monsterListFilter.Get1(monsterListIndex);
                ref var spawnEntity = ref _monsterListFilter.GetEntity(monsterListIndex);
                var randomCount = Random.Range(0, monsterList._monstersFromFirstFloor.Count);

                foreach (var monsterIndex in _monsterForSpawnFilter)
                {
                    ref var monster = ref _monsterForSpawnFilter.Get1(monsterIndex);
                    
                    monster.monsterObject = monsterList._monstersFromFirstFloor[randomCount];
                    Debug.Log($"Send spawn event");
                    
                    spawnEntity.Get<SpawnEvent>();
                    spawnEntity.Del<ChoiceMonsterFromListEvent>();
                    
                }
            }
        }
    }
}