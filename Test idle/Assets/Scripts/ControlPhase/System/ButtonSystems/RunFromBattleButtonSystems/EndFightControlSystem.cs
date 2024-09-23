using DefaultNamespace.Battle.Components.BattleComponents;
using DefaultNamespace.Battle.Components.Events;
using Leopotam.Ecs;
using Unity.VisualScripting;
using NotImplementedException = System.NotImplementedException;

namespace DefaultNamespace.Battle.System
{
    public class EndFightControlSystem : IEcsRunSystem
    {
        private EcsFilter<ButtonBattleComponent, HideRunFromBattleUIEvent> _buttonFilter = null;
        
        public void Run()
        {
            foreach (var i in _buttonFilter)
            {
                ref var battleButtons = ref _buttonFilter.Get1(i);
                battleButtons.beginBattle.gameObject.SetActive(true);
                battleButtons.runFromBattle.gameObject.SetActive(false);
            }
        }
    }
}