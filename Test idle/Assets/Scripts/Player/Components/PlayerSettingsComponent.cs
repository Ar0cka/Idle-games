using System;
using UnityEngine;

namespace DefaultNamespace.Components
{
    [Serializable]
    public struct PlayerSettingsComponent
    {
        public PlayerSettings playerSettings;
        public int currentHP;
    }
}