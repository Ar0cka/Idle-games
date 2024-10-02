using DefaultNamespace.Battle.Components.BattleComponents;
using DefaultNamespace.Battle.Components.Events;
using DefaultNamespace.ControlPhase.Components.Events;
using DefaultNamespace.MonsterSpawn.Components;
using DefaultNamespace.MonsterSpawn.Events;
using Leopotam.Ecs;
using NotImplementedException = System.NotImplementedException;

namespace DefaultNamespace.Battle.System
{
    public class RunFromBattleSystem : IEcsInitSystem
    {
        private readonly EcsFilter<ButtonBattleComponent, isBattlePhaseComponent> _battleFilter = null;
        private readonly EcsFilter<MonsterCheckStateComponent> _stateFilter = null;
        private readonly EcsFilter<SpawnSettings> _spawnFilter = null;
        
        
        public void Init()
        {
            foreach (var i in _battleFilter)
            {
                ref var buttonRunFromBattle = ref _battleFilter.Get1(i).runFromBattle;
                buttonRunFromBattle.onClick.AddListener(RunFromBattle);
            }
        }

        private void RunFromBattle()
        {
            foreach (var i in _battleFilter)
            {
                ref var isBattlePhase = ref _battleFilter.Get2(i);
                isBattlePhase.IsBeginBattlePhase = false;

                ref var entity = ref _battleFilter.GetEntity(i);
                
                SendDestroyEvent();
                
                entity.Get<HideRunFromBattleUIEvent>();
                entity.Get<OnButtonHealEvent>();
            }
        }

        private void SendDestroyEvent()
        {
            foreach (var spawnIndex in _spawnFilter)
            {
                ref var spawnEntity = ref _spawnFilter.GetEntity(spawnIndex);

                foreach (var stateIndex in _stateFilter)
                {
                    ref var monsterAlive = ref _stateFilter.Get1(stateIndex).MonsterAlive;

                    if (monsterAlive)
                    {
                        spawnEntity.Get<DestroyMonsterOfTheRunFromBattleEvent>();
                    }
                }
            }
        }
    }
}