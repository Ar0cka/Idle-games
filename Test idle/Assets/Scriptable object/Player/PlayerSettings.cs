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

    public void UpgradeHitPoint(int buffHp)
    {
        hitPoint += buffHp;
    }

    public void UpgradeDamage(int buffDamage)
    {
        damage += buffDamage;
    }

    public void UpgradeArmour(int buffArmour)
    {
        armour += buffArmour;
    }

    public void UpgradeAttackSpeed(float buffSpeedAttack)
    {
        attackSpeed += buffSpeedAttack;
    }
}
