using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MonsterType : ScriptableObject
{
    public int Atk;
    public int Hp;
    public int Def;
    public int Speed;
    public string AvatarPath;
    public int EnemyType;
    public string EnemyName;
    public float HeSoNangCapHp;
    public float HeSoNangCapAtk;
    public float HeSoNangCapDef;
    public float HeSoNangCapSpeed;
    public float AttackRange;
    public int[] state_ids;
}
