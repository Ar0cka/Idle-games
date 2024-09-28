using DefaultNamespace.Battle.Components.BattleComponents;
using DefaultNamespace.Battle.Components.Events;
using DefaultNamespace.ControlPhase.Components.Events;
using Leopotam.Ecs;
using NotImplementedException = System.NotImplementedException;

namespace DefaultNamespace.Battle.System
{
    public class RunFromBattleSystem : IEcsInitSystem
    {
        private EcsFilter<ButtonBattleComponent, isBattlePhaseComponent> _battleFilter = null;
        private bool isRunCanWork = false;
        
        
        public void Init()
        {
            foreach (var i in _battleFilter)
            {
                ref var buttonRunFromBattle = ref _battleFilter.Get1(i).runFromBattle;
                buttonRunFromBattle.onClick.AddListener(RunFromBattle);
                isRunCanWork = false;
            }
        }

        private void RunFromBattle()
        {
            foreach (var i in _battleFilter)
            {
                ref var isBattlePhase = ref _battleFilter.Get2(i);
                isBattlePhase.IsBeginBattlePhase = false;
                isRunCanWork = true;

                ref var entity = ref _battleFilter.GetEntity(i);
                entity.Get<HideRunFromBattleUIEvent>();
                entity.Get<OnButtonHealEvent>();
            }
        }
    }
}