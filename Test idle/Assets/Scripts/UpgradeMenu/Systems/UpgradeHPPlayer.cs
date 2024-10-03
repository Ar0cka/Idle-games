using DefaultNamespace.UpgradeMenu.Components;
using Leopotam.Ecs;

namespace DefaultNamespace.UpgradeMenu.Systems
{
    public class UpgradeHPPlayer : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerObjectComponent> _player = null;

        public void Run()
        {
            var statesType = StatesType.Health;
            
            
        }
    }
} 