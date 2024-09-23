using System;
using Scriptable_object.Monsters;

namespace DefaultNamespace.Battle.Components.MonsterComponents
{
    [Serializable]
    public struct MonsterBattleComponents
    {
        public DefaultMonster monstersAbstract;

        public int currentHP;
    }
}