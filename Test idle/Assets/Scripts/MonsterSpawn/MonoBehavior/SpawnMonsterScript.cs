using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace.MonsterSpawn.MonoBehavior
{
    public class SpawnMonsterScript : MonoBehaviour
    {
        private GameObject _monsterObject;

        public void SpawnMonsterOnScene(GameObject _gameObject, RectTransform transform, GameObject parent)
        {
            _monsterObject = Instantiate(_gameObject, transform);
            _monsterObject.transform.SetParent(parent.transform, false);
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

        public MonstersAbstract GetMonsterAbstract()
        {
            if (_monsterObject != null)
                return _monsterObject.GetComponent<MonsterSettings>()._monsterAbstract;
            
            Debug.LogError("Monster don't find");
            return null;
        }

        public void DestroyMonster()
        {
            Destroy(_monsterObject);
        }
    }
}