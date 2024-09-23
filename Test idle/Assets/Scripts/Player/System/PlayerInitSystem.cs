using DefaultNamespace.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace DefaultNamespace.Player.System
{
    sealed class PlayerInitSystem : IEcsInitSystem
    {
        private EcsFilter<PlayerSettingsComponent> playerFilter = null;

        public void Init()
        {
            foreach (var i in playerFilter)
            {
                ref var playerSettingsComponent = ref playerFilter.Get1(i);

                if (playerSettingsComponent.currentHP == 0)
                {
                    playerSettingsComponent.currentHP = playerSettingsComponent.playerSettings._hitPoint;
                }
            }
        }
    }
}