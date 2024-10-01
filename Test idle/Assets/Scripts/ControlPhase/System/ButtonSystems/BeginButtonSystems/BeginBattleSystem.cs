using DefaultNamespace.Battle.Components.BattleComponents;
using DefaultNamespace.Battle.Components.Events;
using DefaultNamespace.Battle.Components.Events.BlockAttackEvents;
using DefaultNamespace.ControlPhase.Components.Events;
using DefaultNamespace.MonsterSpawn.Components;
using DefaultNamespace.MonsterSpawn.Events;
using Leopotam.Ecs;
using UnityEngine;

namespace DefaultNamespace.Battle.System
{
    public class BeginBattleSystem : IEcsInitSystem
    {
        private readonly EcsFilter<ButtonBattleComponent, isBattlePhaseComponent>.Exclude<SerializeAttackCooldownEvent> _battleFilter = null;
        private readonly EcsFilter<MonsterFirstFloorComponent> _monsterListFilter = null;
        private readonly EcsFilter<PlayerCooldownComponent> _playerAttackCooldownSystem;

        public void Init()
        {
            foreach (var i in _battleFilter)
            {
                ref var beginButton = ref _battleFilter.Get1(i).beginBattle;
                beginButton.onClick.AddListener(BeginBattlePhase);
            }
        }

        private void BeginBattlePhase()
        {
            foreach (var i in _battleFilter)
            {
                ref var isBattlePhase = ref _battleFilter.Get2(i).IsBeginBattlePhase;
                isBattlePhase = true;

                ref var entity = ref _battleFilter.GetEntity(i);
                entity.Get<HideBeginBattleUIEvent>();
                entity.Get<HideHealButtonEvent>();

               SendEvents();
            }
        }

        private void SendEvents()
        {
            // Spawn enemy
            var monsterListEntity =
                _monsterListFilter.GetEntitiesCount() > 0 ? _monsterListFilter.GetEntity(0) : default;

            if (monsterListEntity != default)
                monsterListEntity.Get<ChoiceMonsterFromListEvent>();

            var playerEntity = _playerAttackCooldownSystem.GetEntitiesCount() > 0
                ? _playerAttackCooldownSystem.GetEntity(0)
                : default;

            if (playerEntity != default)
                playerEntity.Get<SerializeAttackCooldownEvent>();
            
        }
    }
}