using UnityEngine;
public class MonstersAbstract : ScriptableObject
{
    [SerializeField] protected string nameMonster;
    [SerializeField] protected int hitPoint;
    [SerializeField] protected int damage;
    [SerializeField] protected float attackSpeed;
}