using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DropItemSpawnerController :ScriptableObject
{
    public float ExpRateDrop;
    public float HealRateDrop;
    public int totalExpItemNumberToLevelUp;
    public int totalHealItemNumberToHealFull;
    public int DistanceFromEnemyForHeal;
    public int DistanceFromEnemyForExp;
}
