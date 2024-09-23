using DefaultNamespace.Battle.Components.BattleComponents;
using DefaultNamespace.Battle.Components.Events.AttackEvents;
using DefaultNamespace.Battle.Components.Events.BlockAttackEvents;
using DefaultNamespace.Battle.Components.MonsterComponents;
using DefaultNamespace.Components;
using Leopotam.Ecs;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace DefaultNamespace.Battle.System.BattleSystems.MonstersAttackSystem
{
    public class EnemyAttackSystem : IEcsRunSystem
    {
        private readonly EcsFilter<MonsterBattleComponents, MonsterCooldownAttackComponent> _monster = null;
        private readonly EcsFilter<isBattlePhaseComponent> _cheakBattleFilter = null;
        private readonly EcsFilter<PlayerSettingsComponent> _player = null;

        public void Run()
        {
            foreach (var boolIndex in _cheakBattleFilter)
            {
                ref var isBattle = ref _cheakBattleFilter.Get1(boolIndex).IsBeginBattlePhase;
                if (!isBattle) continue;
                
                foreach (var monsterIndex in _monster)
                {
                    ref var monster = ref _monster.Get1(monsterIndex);
                    ref var time = ref _monster.Get2(monsterIndex);
                    ref var entityTimer = ref _monster.GetEntity(monsterIndex);
                    
                    foreach (var playerIndex in _player)
                    {
                        ref var entityPlayer = ref _player.GetEntity(playerIndex);
                        ref var playerHp = ref _player.Get1(playerIndex).currentHP;
                        
                        if (time.blockTimer <= 0)
                        {
                            if (playerHp > 0)
                            {
                                time.blockTimer = monster.monstersAbstract._attackSpeed; 
                                entityPlayer.Get<MonsterAttackEvent>().damageMonster = monster.monstersAbstract._damageMonster;
                            }
                        }
                        else
                        {
                            entityTimer.Get<EnemyBlockAttackEvent>();
                        }
                    }
                }
            }
        }
    }
}