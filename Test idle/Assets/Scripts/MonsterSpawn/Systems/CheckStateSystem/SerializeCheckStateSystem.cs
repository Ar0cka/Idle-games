using DefaultNamespace.MonsterSpawn.Components;
using Leopotam.Ecs;
using UnityEngine;
using Zenject.Asteroids;

namespace MonsterSpawn.Systems.CheckStateSystem
{
    public class SerializeCheckStateSystem : IEcsInitSystem
    {
        private EcsWorld _ecsWorld;
        private EcsFilter<MonsterCheckStateComponent> _stateFilter;

        public void Init()
        {
            var stateEntity = _ecsWorld.NewEntity();
            stateEntity.Get<MonsterCheckStateComponent>();
            
            foreach (var stateIndex in _stateFilter)
            {
                ref var stateSettings = ref _stateFilter.Get1(stateIndex);
                stateSettings.MonsterAlive = false;
                stateSettings.CanSerializeMonsterData = false;
            }
        }
    }
}