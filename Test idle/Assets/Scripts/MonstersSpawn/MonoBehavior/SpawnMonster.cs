using UnityEngine;

namespace DefaultNamespace.Battle.Components.Events.BlockAttackEvents.SpawnMonsters.MonoBehavior
{
    public class SpawnMonster : MonoBehaviour
    {
        private GameObject _spawnObject;

        public void SpawnMonsterToScene(GameObject _monster, Transform _transform, Transform _parent)
        {
            _spawnObject = Instantiate(_monster, _transform);
            _spawnObject.transform.SetParent(_parent, false);
        }

        public void DestroyMonster()
        {
            Destroy(_spawnObject);
        }

        public GameObject GetMonster()
        {
            return _spawnObject;
        }
    }
}