using System;
using DefaultNamespace.SceneUI.Menu.Component;
using Leopotam.Ecs;
using UnityEngine;

namespace DefaultNamespace.SceneUI.Menu.Systems
{
    public class OpenMenuOnBattleScene : IEcsInitSystem
    {
        private readonly EcsFilter<MenuUIComponent> _menuFilter = null;

        public void Init()
        {
            foreach (var menuIndex in _menuFilter)
            {
                ref var menuUI = ref _menuFilter.Get1(menuIndex);
                menuUI.menuPanel.gameObject.SetActive(false);
                
                Debug.Log("Add listener openMeny");
                
                menuUI.openMenuButton.onClick.AddListener(OpenMenu);
            }
        }

        private void OpenMenu()
        {
            foreach (var menuIndex in _menuFilter)
            {
                ref var menuUI = ref _menuFilter.Get1(menuIndex);
                ref var menuEntity = ref _menuFilter.GetEntity(menuIndex);

                menuUI.openMenuButton.interactable = false;
                menuUI.menuPanel.gameObject.SetActive(true);
                
            }
        }
    }
}