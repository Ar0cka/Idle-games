using UnityEngine;
public class MonstersAbstract : ScriptableObject
{
    [SerializeField] protected string nameMonster;
    public string _nameMonster => nameMonster;
    public float hitPoint;
    public float damage;
    public float attackSpeed;
}