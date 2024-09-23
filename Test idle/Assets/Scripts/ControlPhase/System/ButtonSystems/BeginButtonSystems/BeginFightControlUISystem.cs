using DefaultNamespace.Battle.Components.BattleComponents;
using DefaultNamespace.Battle.Components.Events;
using Leopotam.Ecs;
using NotImplementedException = System.NotImplementedException;

namespace DefaultNamespace.Battle.System
{
    public class BeginFightControlUISystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsFilter<ButtonBattleComponent, HideBeginBattleUIEvent> hideUIFilter = null;

        public void Init()
        {
            foreach (var i in hideUIFilter)
            {
                ref var buttonBattleComponent = ref hideUIFilter.Get1(i).runFromBattle;
                buttonBattleComponent.gameObject.SetActive(false);
            }
        }

        public void Run()
        {
            foreach (var i in hideUIFilter)
            {
                ref var buttonBattleComponent = ref hideUIFilter.Get1(i);
                buttonBattleComponent.beginBattle.gameObject.SetActive(false);
                buttonBattleComponent.runFromBattle.gameObject.SetActive(true);
        
                ref var entity = ref hideUIFilter.GetEntity(i);
                entity.Del<HideBeginBattleUIEvent>();
            }
        }
    }
}