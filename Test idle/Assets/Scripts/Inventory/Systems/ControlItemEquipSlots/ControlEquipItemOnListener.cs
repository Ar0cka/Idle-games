using System;
using Inventory.Component;
using Inventory.Events.ControlItemEquipSlot;
using Leopotam.Ecs;
using UnityEngine;

namespace Inventory.Systems.ControlItemEquipSlots
{
    public class ControlEquipItemOnListener : IEcsRunSystem
    {
        private EcsWorld _ecsWorld;
        private EcsFilter<ItemPanelComponent> _panelFilter;
        private InventoryEquip _inventoryEquip;
        
        public void Run()
        {
            foreach (var slot in _inventoryEquip.ReturnSlots())
            {
                if (!slot.isHaveListener && slot.isOccupied)
                {
                    AddListenerToButton(slot);
                }
            }
        }

        private void AddListenerToButton(EquipSlotData slotData)
        {
            foreach (var panelIndex in _panelFilter)
            {
                ref var itemPanel = ref _panelFilter.Get1(panelIndex);

                itemPanel.itemPanel.SetActive(true);
                itemPanel.useItemButton.enabled = false;
                itemPanel.deleteItemButton.enabled = false;
                itemPanel.itemDescription.text = slotData.slot.GetComponentInChildren<ItemSettings>().baseAbstractItem.description;
                itemPanel.removeItemFromEquipButton.enabled = true;
                
                itemPanel.removeItemFromEquipButton.onClick.RemoveAllListeners();
                itemPanel.removeItemFromEquipButton.onClick.AddListener(() => SendEventDeleteItemFromEquipSlot(slotData));
                
                slotData.IsHaveListener(true);
            }
        }

        private void SendEventDeleteItemFromEquipSlot(EquipSlotData slotData)
        {
            try
            {
                var entity = _ecsWorld.NewEntity();
                entity.Get<RemoveItemFromEquipSlotEvent>().slotData = slotData;
            }
            catch (Exception e)
            {
                Debug.LogError($"failed send remove item event {e}");
            }
        }
    }
}