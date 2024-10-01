using UnityEngine;

public class MonsterSettings : MonoBehaviour
{
    [SerializeField] private MonstersAbstract monstersAbstract;
    public MonstersAbstract _monsterAbstract => monstersAbstract;
}
