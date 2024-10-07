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
            var entity = _ecsWorld.NewEntity();

            var takeActionEvent = new TakeActionEvent()
            {
                slotData = slotData,
                baseAbstract = slotData._slot.GetComponentInChildren<ItemSettings>().baseAbstractItem
            };
            
            entity.Get<TakeActionEvent>() = takeActionEvent;
            Debug.Log("Send takeActionEvent");
        }
    }
}