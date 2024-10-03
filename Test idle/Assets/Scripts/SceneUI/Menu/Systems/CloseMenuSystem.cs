using DefaultNamespace.SceneUI.Menu.Component;
using Leopotam.Ecs;

namespace DefaultNamespace.SceneUI.Menu.Systems
{
    public class CloseMenuSystem : IEcsInitSystem
    {
        private readonly EcsFilter<MenuUIComponent> _menuFilter = null;

        public void Init()
        {
            foreach (var menuIndex in _menuFilter)
            {
                ref var menuUI = ref _menuFilter.Get1(menuIndex);
                
                menuUI.quitFromPanel.onClick.AddListener(QuitFromMenu);
            }
        }

        private void QuitFromMenu()
        {
            foreach (var menuFilter in _menuFilter)
            {
                ref var menuUI = ref _menuFilter.Get1(menuFilter);
                
                menuUI.menuPanel.gameObject.SetActive(false);
                menuUI.openMenuButton.interactable = true;
            }
        }
    }
}