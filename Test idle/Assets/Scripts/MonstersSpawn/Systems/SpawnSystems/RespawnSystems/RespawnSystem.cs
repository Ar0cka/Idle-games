using DefaultNamespace.Battle.Components.Events.BlockAttackEvents.SpawnMonsters;
using DefaultNamespace.BattlePhase.Components.Events.SpawnEvents;
using Leopotam.Ecs;
using TMPro;
using UnityEngine;

namespace DefaultNamespace.BattlePhase.Systems.BattleSystems.SpawnSystems.RespawnSystems
{
    public class RespawnSystem : IEcsRunSystem
    {
        private readonly EcsFilter<SpawnSettings, RespawnEvent> _spawnFilter = null;
        private readonly EcsFilter<MonsterFromFirstFloorComponent> _monsterListFilter = null;

        public void Run()
        {
            foreach (var spawnIndex in _spawnFilter)
            {
                ref var spawnEntity = ref _spawnFilter.GetEntity(spawnIndex);

                spawnEntity.Get<DestroyMonsterEvent>();


                var monsterEntity = _monsterListFilter.GetEntitiesCount() > 0
                    ? _monsterListFilter.GetEntity(0)
                    : default;

                if (monsterEntity != default)
                {
                    monsterEntity.Get<ChoiceMonsterEvent>();
                }
                
                spawnEntity.Del<RespawnEvent>();
            }
        }
    }
}