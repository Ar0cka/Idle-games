using UnityEngine;

namespace Scriptable_object.Monsters
{
    [CreateAssetMenu(fileName = "Monster", menuName = "MonsterCreate", order = 0)]
    public class DefaultMonster : MonstersAbstract
    {
        public int _hitPointMonster => hitPoint;
        
        public int _damageMonster => damage;

        public float _attackSpeed => attackSpeed;
    }
}