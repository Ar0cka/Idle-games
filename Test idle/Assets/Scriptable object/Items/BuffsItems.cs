using UnityEngine;

namespace Scriptable_object.Items
{
    [CreateAssetMenu (fileName = "new item", menuName = "Data/Items/BuffItem")]
    public class BuffsItems : CollectedItems
    {
        [Header("Consumable type")]
        [SerializeField] private ConsumableItemType _consumableItem;
        
        public ConsumableItemType consumableItem => _consumableItem;
    }
}