using Scriptable_object.Items;
using Unity.VisualScripting;

namespace DefaultNamespace.Components
{
    public class PlayerData
    {
        //Класс для временных статов персонажа, которые будут браться, для разных математических расчетов

        #region currentData

        public int maxHp { get; private set; }
        public float damage { get; private set; }
        
        public float armour { get; private set; }
        public float attackSpeed { get; private set; }
        public float critChanse { get; private set; }
        public float critBuff { get; private set; }

        #endregion

        private PlayerSettings _playerBaseStats;

        public PlayerData(PlayerSettings playerSettings)
        {
            _playerBaseStats = playerSettings;
            SerializeCurrentMaxStats();
        }

        public void UpdatePlayerBaseStats(PlayerSettings playerSettings)
        {
            _playerBaseStats = playerSettings;
        }

        public void EquipItem(EquipItem equipItem)
        {
            if (equipItem is Armour armourData)
            {
                IncreaseArmour(armourData.armour);
            }

            if (equipItem is Boots boots)
            {
                IncreaseArmour(boots.armour);
                IncreaseCritBuff(boots.critDamage);
                DecreaseAttackSpeed(boots.attackSpeed);
            }

            if (equipItem is HairItemStats hairItemStats)
            {
                IncreaseMaxHp((int)hairItemStats.hp);
                IncreaseArmour(hairItemStats.armour);
            }
            
            if (equipItem is Weapon weapon)
            {
                IncreaseDamage(weapon.damage);
                IncreaseCritChance(weapon.critChance);
                IncreaseCritBuff(weapon.critDamage);
            }

            if (equipItem is SecondWeapon secondWeapon)
            {
                IncreaseDamage(secondWeapon.damage);
                IncreaseCritBuff(secondWeapon.critDamage);
                DecreaseAttackSpeed(secondWeapon.attackSpeed);
            }
        }

        public void DeleteItemFromEquip(EquipItem equipItem)
        {
            if (equipItem is Armour armourData)
            {
                DecreaseArmour(armourData.armour);
            }

            if (equipItem is Boots boots)
            {
                DecreaseArmour(boots.armour);
                DecreaseCritBuff(boots.critDamage);
                IncreaseAttackSpeed(boots.attackSpeed);
            }

            if (equipItem is HairItemStats hairItemStats)
            {
                DecreaseArmour(hairItemStats.armour);
                DecreaseMaxHp((int)hairItemStats.hp);
            }

            if (equipItem is Weapon weapon)
            {
                DecreaseDamage(weapon.damage);
                DecreaseCritChance(weapon.critChance);
                DecreaseCritBuff(weapon.critDamage);
            }

            if (equipItem is SecondWeapon secondWeapon)
            {
                DecreaseDamage(secondWeapon.damage);
                DecreaseCritBuff(secondWeapon.critDamage);
                IncreaseAttackSpeed(secondWeapon.attackSpeed);
            }
        }

        public void SerializeCurrentMaxStats()
        {
            maxHp = _playerBaseStats._hitPoint;
            armour = _playerBaseStats._armour;
            damage = _playerBaseStats._damage;
            attackSpeed = _playerBaseStats._attackSpeed;
            critChanse = _playerBaseStats._damage;
            critBuff = _playerBaseStats._damage;
        }

        #region IncreaseAndDecreaseMethods

        private void IncreaseMaxHp(int amount) => maxHp += amount;
        private void DecreaseMaxHp(int amount) => maxHp -= amount;

        private void IncreaseArmour(float amount) => armour += amount;
        private void DecreaseArmour(float amount) => armour -= amount;

        private void IncreaseAttackSpeed(float amount) => attackSpeed += amount;
        private void DecreaseAttackSpeed(float amount) => attackSpeed -= amount;
        
        private void IncreaseDamage(float amount) => damage += amount;
        private void DecreaseDamage(float amount) => damage -= amount;
        
        private void IncreaseCritChance(float amount) => critChanse += amount;
        private void DecreaseCritChance(float amount) => critChanse -= amount;

        private void IncreaseCritBuff(float amount) => critBuff += amount;
        private void DecreaseCritBuff(float amount) => critBuff -= amount;

        #endregion
        
    }
}