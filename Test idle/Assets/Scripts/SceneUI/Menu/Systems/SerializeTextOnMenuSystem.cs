using DefaultNamespace.Components;
using DefaultNamespace.SceneUI.Menu.Component;
using Leopotam.Ecs;

namespace DefaultNamespace.SceneUI.Menu.Systems
{
    public class SerializeTextOnMenuSystem : IEcsInitSystem
    {
        private readonly EcsFilter<TextMenuComponent> _textFilter = null;
        private readonly EcsFilter<PlayerSettingsComponent> _playerFilter = null;

        public void Init()
        {
            foreach (var playerIndex in _playerFilter)
            {
                ref var player = ref _playerFilter.Get1(playerIndex);
                SerializeUI(player.playerSettings);
            }
        }

        private void SerializeUI(PlayerSettings playerSettings)
        {
            foreach (var textIndex in _textFilter)
            {
                ref var textUI = ref _textFilter.Get1(textIndex);

                textUI.hpText.text = $"Max hp = {playerSettings._hitPoint}";
                textUI.damageText.text = $"Damage = {playerSettings._damage}";
                textUI.attackSpeedText.text = $"Attack speed = {playerSettings._attackSpeed}";
                textUI.armorText.text = $"Armour = {playerSettings._armour}";
            }
        }
    }
}