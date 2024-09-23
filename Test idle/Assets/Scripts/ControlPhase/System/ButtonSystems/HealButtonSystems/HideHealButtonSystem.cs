using DefaultNamespace.Battle.Components.BattleComponents;
using DefaultNamespace.ControlPhase.Components.Events;
using Leopotam.Ecs;
using NotImplementedException = System.NotImplementedException;

namespace DefaultNamespace.Battle.System
{
    public class HideHealButtonSystem : IEcsRunSystem
    {
        private EcsFilter<ButtonBattleComponent, HideHealButtonEvent> _buttonFilter;

        public void Run()
        {
            foreach (var i in _buttonFilter)
            {
                ref var buttonHeal = ref _buttonFilter.Get1(i).healButton;
                ref var entityButton = ref _buttonFilter.GetEntity(i);

                buttonHeal.gameObject.SetActive(false);
                entityButton.Del<HideHealButtonEvent>();
            }
        }
    }
}