using DefaultNamespace.Battle.Components.BattleComponents;
using DefaultNamespace.Battle.Components.Events.AttackEvents;
using DefaultNamespace.Battle.Components.Events.BlockAttackEvents;
using DefaultNamespace.Battle.Components.MonsterComponents;
using DefaultNamespace.Components;
using DefaultNamespace.MonsterSpawn.Components;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace DefaultNamespace.Battle.System.BattleSystem.BlockSystems
{
    public class PlayerAttackEnemySystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerSettingsComponent, PlayerCooldownComponent> _playerFilter = null;

        private readonly EcsFilter<MonsterCheckStateComponent> _stateFilter = null;
        
        private readonly EcsFilter<isBattlePhaseComponent> _phaseStateFilter = null;

        private readonly EcsFilter<MonsterBattleComponents> _monsterFilter = null;
        
        public void Run()
        {
            foreach (var phaseIndex in _phaseStateFilter)
            {
                ref var isBattlePhase = ref _phaseStateFilter.Get1(phaseIndex);
                
                if (!isBattlePhase.IsBeginBattlePhase) continue;

                foreach (var stateIndex in _stateFilter)
                {
                    ref var monsterAlive = ref _stateFilter.Get1(stateIndex).MonsterAlive;
                    
                    foreach (var playerIndex in _playerFilter)
                    {
                        ref var player = ref _playerFilter.Get1(playerIndex).playerSettings;
                        ref var cooldown = ref _playerFilter.Get2(playerIndex);
                        ref var entity = ref _playerFilter.GetEntity(playerIndex);

                        var monsterHp = _monsterFilter.GetEntitiesCount() > 0 ? _monsterFilter.Get1(0).currentHP : default;

                        if (monsterHp > 0 && monsterAlive)
                        {
                            if (cooldown.Timer <= 0)
                            {
                                cooldown.Timer = player._attackSpeed;

                                foreach (var monsterIndex in _monsterFilter)
                                {
                                    ref var entityMonster = ref _monsterFilter.GetEntity(monsterIndex);

                                    entityMonster.Get<PlayerAttackEvent>().damagePlayer = player._damage;
                                }
                            }
                            else
                            {
                                entity.Get<PlayerBlockEvent>();
                            }
                        }
                        else
                        {
                            cooldown.Timer = player._attackSpeed;
                        }
                    }
                }
            }
        }
    }
}