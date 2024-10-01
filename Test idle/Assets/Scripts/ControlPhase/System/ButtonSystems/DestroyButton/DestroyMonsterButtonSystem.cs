using DefaultNamespace.Battle.Components.BattleComponents;
using DefaultNamespace.MonsterSpawn.Components;
using DefaultNamespace.MonsterSpawn.Events;
using Leopotam.Ecs;
using UnityEngine;

namespace DefaultNamespace.ControlPhase.System.DestroyButton
{
    public class DestroyMonsterButtonSystem : IEcsInitSystem
    {
        private readonly EcsFilter<ButtonBattleComponent> _buttonFilter = null;
        private readonly EcsFilter<SpawnSettings> _spawnSettings = null;
        private readonly EcsFilter<MonsterCheckStateComponent> _stateFilter = null;

        public void Init()
        {
            foreach (var buttonIndex in _buttonFilter)
            {
                ref var destroyButton = ref _buttonFilter.Get1(buttonIndex).destroyButton;
                
                destroyButton.onClick.AddListener(DestroyAction);
            }
        }

        public void DestroyAction()
        {
            
            foreach (var stateIndex in _stateFilter)
            {
                ref var monsterAlive = ref _stateFilter.Get1(stateIndex).MonsterAlive;
                
                Debug.Log($"Monster alive = {monsterAlive}");
                
                if (!monsterAlive) continue;
                
                foreach (var spawnIndex in _spawnSettings)
                {
                    ref var spawnEntity = ref _spawnSettings.GetEntity(spawnIndex);

                    spawnEntity.Get<DestroyEnemyEvent>();
                    
                    monsterAlive = false;
                }
            }
        }
    }
}