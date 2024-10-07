using UnityEngine;
using UnityEngine.Serialization;

namespace Scriptable_object.Items
{
    public abstract class BaseAbstractItem : ScriptableObject
    {
        [Header("Description item")]
        [SerializeField] private string _nameItem;
        public string nameItem => _nameItem;
        
        [SerializeField] private string _description;
        public string description => _description;

        [Header("Types item")]
        [SerializeField] private ItemType _itemType;
        public ItemType itemType => _itemType;

        [SerializeField] private ItemCategory _itemCategory;
        public ItemCategory itemCategory => _itemCategory;
        
        [Header("Buff")] [SerializeField] private float _quantityBuff;
        public float quantityBuff => _quantityBuff;
        
        [Header("Image item")]
        public Sprite iconItem;
    }
}