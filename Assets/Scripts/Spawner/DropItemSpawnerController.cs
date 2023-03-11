using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DropItemSpawnerController :ScriptableObject
{
    public int ExpRateDrop;
    public int HealRateDrop;
    public int totalExpItemNumberToLevelUp;
    public int totalHealItemNumberToHealFull;
    public int DistanceFromEnemyForHeal;
    public int DistanceFromEnemyForExp;
}
