using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject.Asteroids;

namespace Inventory.Component
{
    [Serializable]
    public struct ItemPanelComponent
    {
        public Button useItemButton;
        public Button deleteItemButton;
        public Button removeItemFromEquipButton;
        public GameObject itemPanel;
        public TextMeshProUGUI itemDescription;
        public SlotData itemData;
    }
}