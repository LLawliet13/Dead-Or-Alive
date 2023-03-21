using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// truyen vao tung state
/// </summary>
[CreateAssetMenu]

public class BossUpgradeController : ScriptableObject
{
    public BossState0[] bossState0;
    public BossState1[] bossState1;
    public BossState2[] bossState2;
    public BossState3[] bossState3;
    public BossState4[] bossState4;
    public BossState5[] bossState5;

    [System.Serializable]
    
    public class BossState0
    {
        public int baseLv;
        public float bossSpeed;
    }
    [System.Serializable]
    public class BossState1
    {
        public int baseLv;
        public int ableToDo;

    }
    [System.Serializable]
    public class BossState2
    {
        public int baseLv;
        public float delayCollisionDamge;
        public float EarthQuakeRatio;
    }
    [System.Serializable]
    public class BossState3
    {
        public int baseLv;
        public float angleRange;
        public int numberOfRock;
        public bool ableToBounceBack;
        public float existTime;
        public float minSpeed;
        public float maxSpeed;
    }
    [System.Serializable]
    public class BossState4
    {
        public int baseLv;
    }
    [System.Serializable]
    public class BossState5
    {
        public int baseLv;
    }
}
