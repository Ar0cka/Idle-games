using DefaultNamespace.Battle.Components.BattleComponents;
using DefaultNamespace.Components;
using DefaultNamespace.ControlPhase.Components.Events;
using Leopotam.Ecs;

namespace BattlePhase.Components.AttackHero
{
    public class AttackHero : IEcsInitSystem
    {
        private readonly EcsFilter<ButtonBattleComponent> _buttonFilter;
        private readonly EcsFilter<PlayerSettingsComponent> _player;
        private readonly EcsFilter<HpBarComponent> _barFilter;

        public void Init()
        {
            foreach (var buttonIndex in _buttonFilter)
            {
                ref var buttonAttackPlayer = ref _buttonFilter.Get1(buttonIndex).attackHero;
                
                buttonAttackPlayer.onClick.AddListener(Attack);
            }
        }

        private void Attack()
        {
            foreach (var playerIndex in _player)
            {
                ref var player = ref _player.Get1(playerIndex);

                foreach (var barIndex in _barFilter)
                {
                    ref var barEntity = ref _barFilter.GetEntity(barIndex);

                    player.currentHP -= 10;
                    barEntity.Get<UpdatePlayerUIEvent>();
                }
            }
        }
    }
}