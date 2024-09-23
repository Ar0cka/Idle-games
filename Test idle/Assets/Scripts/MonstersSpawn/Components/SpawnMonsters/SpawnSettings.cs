using System;
using DefaultNamespace.Battle.Components.Events.BlockAttackEvents.SpawnMonsters.MonoBehavior;
using UnityEngine;

namespace DefaultNamespace.Battle.Components.Events.BlockAttackEvents.SpawnMonsters
{
    [Serializable]
    public struct SpawnSettings
    {
        public Transform _spawPosition;
        public GameObject _parent;
        public SpawnMonster _spawnMonster;
    }
}