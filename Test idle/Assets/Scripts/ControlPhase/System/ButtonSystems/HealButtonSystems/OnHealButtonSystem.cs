using DefaultNamespace.Battle.Components.BattleComponents;
using DefaultNamespace.ControlPhase.Components.Events;
using Leopotam.Ecs;
using NotImplementedException = System.NotImplementedException;

namespace DefaultNamespace.Battle.System
{
    public class OnHealButtonSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ButtonBattleComponent, OnButtonHealEvent> _buttonFilter = null;

        public void Run()
        {
            foreach (var buttonIndex in _buttonFilter)
            {
                ref var healButton = ref _buttonFilter.Get1(buttonIndex).healButton;
                ref var entityButton = ref _buttonFilter.GetEntity(buttonIndex);
                
                healButton.gameObject.SetActive(true);
            }
        }
    }
}