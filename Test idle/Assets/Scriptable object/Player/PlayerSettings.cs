using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "PlayerCreate", order = 0)]
public class PlayerSettings : ScriptableObject
{
    [SerializeField] private float hitPoint;
    public float _hitPoint => hitPoint;
    
    [SerializeField] private float damage;
    public float _damage => damage;

    [SerializeField] private float armour;
    public float _armour => armour;

    [SerializeField] private float attackSpeed;
    public float _attackSpeed => attackSpeed;

    public void UpgradeHitPoint(float buffHp)
    {
        hitPoint += buffHp;
    }

    public void UpgradeDamage(float buffDamage)
    {
        damage += buffDamage;
    }

    public void UpgradeArmour(float buffArmour)
    {
        armour += buffArmour;
    }

    public void UpgradeAttackSpeed(float buffSpeedAttack)
    {
        attackSpeed += buffSpeedAttack;
    }
}
