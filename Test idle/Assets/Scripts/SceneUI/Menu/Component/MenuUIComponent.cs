using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DefaultNamespace.SceneUI.Menu.Component
{
    [Serializable]
    public struct MenuUIComponent
    {
        public GameObject menuPanel;
        public Button quitFromPanel;
        public Button openMenuButton;
        
    }
}