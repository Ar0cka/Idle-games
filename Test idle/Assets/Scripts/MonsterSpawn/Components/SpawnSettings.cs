using System;
using DefaultNamespace.MonsterSpawn.MonoBehavior;
using UnityEngine;

namespace DefaultNamespace.MonsterSpawn.Components
{
    [Serializable]
    public struct SpawnSettings
    {
        public RectTransform monsterPosition;
        public SpawnMonsterScript monsterSpawnScript;
        public GameObject parent;
        public int spawnTimer;
    }
}