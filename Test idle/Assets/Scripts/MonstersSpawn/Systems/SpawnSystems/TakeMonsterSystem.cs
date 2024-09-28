using DefaultNamespace.Battle.Components.Events.BlockAttackEvents.SpawnMonsters;
using DefaultNamespace.BattlePhase.Components.Events.SpawnEvents;
using Leopotam.Ecs;
using UnityEngine;

namespace DefaultNamespace.BattlePhase.Systems.BattleSystems.SpawnSystems
{
    public class TakeMonsterSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsWorld _monsterWorld;
        
        private readonly EcsFilter<MonsterFromFirstFloorComponent, ChoiceMonsterEvent> _monstersListFilter = null;
        private readonly EcsFilter<MonsterSpawnComponent> _monster = null;

        public void Init()
        {
            var monster = _monsterWorld.NewEntity();
            monster.Get<MonsterSpawnComponent>();
        }

        public void Run()
        {
            foreach (var i in _monstersListFilter)
            {
                var _listWithMonsters = _monstersListFilter.Get1(i)._prefabsMonsters;
                var entity = _monstersListFilter.GetEntity(i);
                    
                var randomNumber = Random.Range(0, _listWithMonsters.Count);

                var entityMonster = _monster.GetEntitiesCount() > 0 ? _monster.GetEntity(0) : default;

                if (entityMonster != default)
                    entityMonster.Get<LoadingMonsterEvent>()._prefab = _listWithMonsters[randomNumber];
                
                Debug.Log($"Take monster");
                
                entity.Del<ChoiceMonsterEvent>();
            }
        }
    }
}