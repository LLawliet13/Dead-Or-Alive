using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CreepUpgradeController : ScriptableObject
{
    public CreepState1[] creepState1;
    public CreepState2[] creepState2;
    public CreepState3[] creepState3;
    public CreepState4[] creepState4;
    public CreepState5[] creepState5;

    [System.Serializable]
    public class CreepState1
    {
        public int baseLv;
    }
    [System.Serializable]
    public class CreepState2
    {
        public int baseLv;
    }
    [System.Serializable]
    public class CreepState3
    {
        public int baseLv;
        public float attackRange;
    }
    [System.Serializable]
    public class CreepState4
    {
        public int baseLv;
        public float delayTime;
    }
    [System.Serializable]
    public class CreepState5
    {
        public int baseLv;
        public float delayTime;
        public float angleRange;
        public int numberOfBullet;
    }

}
