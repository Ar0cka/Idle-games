using UnityEngine;

namespace DefaultNamespace.MonsterSpawn.MonoBehavior
{
    public class SpawnMonsterScript : MonoBehaviour
    {
        private GameObject _monsterObject;

        public void SpawnMonsterOnScene(GameObject _gameObject, RectTransform transform, GameObject parent)
        {
            Instantiate(_gameObject, transform, parent);
        }

        public GameObject GetMonsterFromScene()
        {
            if (_monsterObject != null)
            {
                return _monsterObject;
            }
            Debug.LogError("Error get monster from scene");
            return null;
        }
    }
}