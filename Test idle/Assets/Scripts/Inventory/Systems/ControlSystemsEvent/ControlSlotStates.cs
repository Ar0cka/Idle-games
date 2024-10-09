using System;
using Inventory.Events;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.Systems
{
    public class ControlSlotStates : IEcsRunSystem
    {
        private EcsWorld _ecsWorld;
        private InventorySettings _inventorySettings;
        private int MaxRetries = 5;

        public void Run()
        {
            foreach (var slot in _inventorySettings.GetSlotData())
            {
                if (!slot.isHaveListener && (slot.isFull || slot.IsOccupied))
                {
                    Button itemWithButtonEvent = slot._slot.GetComponentInChildren<Button>();
                    itemWithButtonEvent.onClick.AddListener(() => SendEventChoiceActionItem(slot));
                    Debug.Log($"Slot name {slot._slot.name} and button = {itemWithButtonEvent.name}");
                    Debug.Log("Add listener in button");
                    slot.ChangeListener(true);
                }
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
    } 
}