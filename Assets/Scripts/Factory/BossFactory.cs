using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossFactory : GenericEnemyFactory<BossStatus>
{
    [SerializeField]
    private BossFactoryController controller;

    public override void Enable()
    {
        Debug.Log("Status:Load All Boss Type");
        monsterTypes = Resources.LoadAll<MonsterType>(controller.TypesPath).OrderBy<MonsterType, int>(mt => mt.EnemyType).ToArray();
        GenerateMonster = new Dictionary<MonsterType, int>();
       
    }

    protected override BossStatus GenerateMonsterStrategy()
    {
        BossStatus go = Instantiate(prefab);
        Debug.LogWarning("OPTIONAL TO-DO: Hien tai boss sinh ra chi co 1 con nen khong co order xuat hien, co the them boss de su dung bossOrder cua controller");
        go.BaseStats = monsterTypes[0];
        return go;
    }
}
