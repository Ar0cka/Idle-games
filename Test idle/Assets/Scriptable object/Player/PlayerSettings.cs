using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "PlayerCreate", order = 0)]
public class PlayerSettings : ScriptableObject
{
    [SerializeField] private int hitPoint;
    public int _hitPoint => hitPoint;
    
    [SerializeField] private int damage;
    public int _damage => damage;

    [SerializeField] private int armour;
    public int _armour => armour;

    [SerializeField] private float attackSpeed;
    public float _attackSpeed => attackSpeed;
}
