using UnityEngine;
public class MonstersAbstract : ScriptableObject
{
    [SerializeField] protected string nameMonster;
    public string _nameMonster => nameMonster;
    public int hitPoint;
    public int damage;
    public float attackSpeed;
}