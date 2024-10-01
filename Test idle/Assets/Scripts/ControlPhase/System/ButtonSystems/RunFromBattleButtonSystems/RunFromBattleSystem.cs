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
        private EcsFilter<ButtonBattleComponent, isBattlePhaseComponent> _battleFilter = null;
        private EcsFilter<SpawnSettings> _spawnFilter = null;
        
        
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
            var spawnEntity = _spawnFilter.GetEntitiesCount() > 0 ? _spawnFilter.GetEntity(0) : default;

            if (spawnEntity != default)
            {
                spawnEntity.Get<DestroyMonsterOfTheRunFromBattleEvent>();
            }
        }
    }
}