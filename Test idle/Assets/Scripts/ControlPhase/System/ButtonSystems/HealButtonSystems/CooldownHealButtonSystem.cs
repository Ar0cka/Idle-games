using DefaultNamespace.Battle.Components.BattleComponents;
using DefaultNamespace.ControlPhase.Components.Events;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;
using NotImplementedException = System.NotImplementedException;

namespace DefaultNamespace.Battle.System
{
    public class CooldownHealButtonSystem : IEcsRunSystem
    {
        private EcsFilter<ButtonBattleComponent, CheckCooldownHpHealComponent, CooldownButtonEvent> _buttonFilter = null;

        public void Run()
        {
            foreach (var buttonIndex in _buttonFilter)
            {
                ref var buttonHeal = ref _buttonFilter.Get1(buttonIndex).healButton;
                ref var cooldownButton = ref _buttonFilter.Get2(buttonIndex).cooldownButton;
                ref var entityButton = ref _buttonFilter.GetEntity(buttonIndex);

                cooldownButton -= Time.deltaTime;
                Debug.Log($"Cooldown = {cooldownButton}");

                if (cooldownButton <= 0)
                {
                    buttonHeal.interactable = true;
                    cooldownButton = 10f;
                    entityButton.Del<CooldownButtonEvent>();
                }
            }
        }
    }
}