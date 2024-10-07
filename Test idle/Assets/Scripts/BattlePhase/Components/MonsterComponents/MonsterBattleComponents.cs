using System;
using Scriptable_object.Monsters;

namespace DefaultNamespace.Battle.Components.MonsterComponents
{
    [Serializable]
    public struct MonsterBattleComponents
    {
        public MonstersAbstract monstersAbstract;

        public float currentHP;
    }
}