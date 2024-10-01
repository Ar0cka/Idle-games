using DefaultNamespace.Battle.Components.BattleComponents;
using DefaultNamespace.ControlPhase.Components.Events;
using Leopotam.Ecs;
using NotImplementedException = System.NotImplementedException;

namespace DefaultNamespace.ControlPhase.System.ControlEnemyHP
{
    public class OnHpBarEnemySystem : IEcsRunSystem
    {
        private readonly EcsFilter<HpBarComponent, OnHpBarEnemyEvent> _hpBarFilter = null;

        public void Run()
        {
            foreach (var barIndex in _hpBarFilter)
            {
                ref var hpBarEnemy = ref _hpBarFilter.Get1(barIndex)._monsterBar;
                ref var entity = ref _hpBarFilter.GetEntity(barIndex);

                hpBarEnemy.gameObject.SetActive(true);
                entity.Del<OnHpBarEnemyEvent>();
            }
        }
    }
}