using System;
using Inventory.Component;
using Inventory.Events;
using Leopotam.Ecs;
using Scriptable_object.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.Systems
{
    public class ControlSlotStates : IEcsRunSystem
    {
        private EcsWorld _ecsWorld;
        private readonly EcsFilter<ItemPanelComponent> _panelFilter;
        
        private InventorySettings _inventorySettings;
        
        private int MaxRetries = 5;

        public void Run()
        {
            foreach (var slot in _inventorySettings.GetSlotData())
            {
                if (!slot.isHaveListener && (slot.isFull || slot.IsOccupied))
                {
                    Button itemWithButtonEvent = slot._slot.GetComponentInChildren<Button>();
                    BaseAbstractItem baseAbstractItem = slot._slot.GetComponentInChildren<ItemSettings>().baseAbstractItem;
                    itemWithButtonEvent.onClick.AddListener(() => OpenItemPanel(baseAbstractItem, slot));
                    Debug.Log($"Slot name {slot._slot.name} and button = {itemWithButtonEvent.name}");
                    Debug.Log("Add listener in button");
                    slot.ChangeListener(true);
                }
            }
        }

        private void OpenItemPanel(BaseAbstractItem item, SlotData slotData)
        {
            foreach (var panelIndex in _panelFilter)
            {
                ref var itemPanel = ref _panelFilter.Get1(panelIndex);

                itemPanel.itemPanel.SetActive(true);
                Debug.Log($"OnPanel");
                itemPanel.itemData = slotData;

                var data = itemPanel.itemData;
                
                itemPanel.useItemButton.onClick.RemoveAllListeners();
                itemPanel.useItemButton.onClick.AddListener(() => SendEventChoiceActionItem(data));
                
                itemPanel.deleteItemButton.onClick.RemoveAllListeners();
                itemPanel.deleteItemButton.onClick.AddListener(() => SendDeleteEvent(slotData));
                
                itemPanel.itemDescription.text = item.description;
            }
        }
        
        private void SendEventChoiceActionItem(SlotData slotData)
        {
            int attempt = 0;
            bool success = false;
            
            Debug.Log($"slotData {slotData._slot.name}");

            while (attempt < MaxRetries && !success)
            {
                try
                {
                    // Попытка получить baseAbstract
                    var baseAbstract = slotData._slot.GetComponentInChildren<ItemSettings>().baseAbstractItem;
                
                    // Если всё прошло успешно, создаём событие
                    var entity = _ecsWorld.NewEntity();
                    var takeActionEvent = new TakeActionEvent()
                    {
                        slotData = slotData,
                        baseAbstract = baseAbstract
                    };

                    entity.Get<TakeActionEvent>() = takeActionEvent;
                    ControlPanel();
                    Debug.Log("Send takeActionEvent");
                    
                    success = true; // Завершаем цикл, если успешна попытка
                }
                catch (Exception e)
                {
                    attempt++;
                    Debug.LogError($"Error on attempt {attempt}: {e.Message}");
                }
            }

            if (!success)
            {
                Debug.LogError("Failed to send event after multiple attempts.");
            }
        }

        private void ControlPanel()
        {
            foreach (var panelIndex in _panelFilter)
            {
                ref var panel = ref _panelFilter.Get1(panelIndex).itemPanel;
                Debug.Log($"Off panel");
                panel.SetActive(false);
            }
        }
        
        private void SendDeleteEvent(SlotData slotData)
        {
            _inventorySettings.DeleteItemFromSlot(slotData);

            foreach (var panelIndex in _panelFilter)
            {
                ref var panel = ref _panelFilter.Get1(panelIndex);
                panel.itemDescription.text = "";
                panel.itemPanel.SetActive(false);
            }
        }
    } 
}