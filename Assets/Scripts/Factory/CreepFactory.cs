using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreepFactory : GenericEnemyFactory<CreepStatus>
{
    [SerializeField]
    private CreepFactoryController controller;
    protected int[] OccurrenceRateTypeList;

    public void ResetFactory()
    {
        if (OccurrenceRateTypeList == null || OccurrenceRateTypeList.Length == 0)
            throw new System.Exception("Rate cua cac type chua duoc xet");
        GenerateMonster.Clear();

        for (int i = 0; i < OccurrenceRateTypeList.Length; i++)
        {
            // lam tron so luong quai
            float value = Mathf.RoundToInt(OccurrenceRateTypeList[i] / 100f * TotalGenerateMonster);
            if (value == 0) continue;
            //thiet lap so luong quai moi loai theo ti le da chia
            GenerateMonster.Add(monsterTypes[i], (int)value);
        }
    }
    public override void Enable()
    {
        Debug.Log("Status:Load All Creep Type");
        monsterTypes = Resources.LoadAll<MonsterType>(controller.TypesPath).OrderBy<MonsterType, int>(mt => mt.EnemyType).ToArray();
        GenerateMonster = new Dictionary<MonsterType, int>();
        OccurrenceRateTypeList = controller.OccurrenceRates;
        ResetFactory();
    }

    protected override CreepStatus GenerateMonsterStrategy()
    {
        CreepStatus go = Instantiate(prefab);
        int r = Random.Range(0, GenerateMonster.Count);
        MonsterType key = GenerateMonster.ElementAt(r).Key;
        go.BaseStats = key;
        GenerateMonster[key] = GenerateMonster[key] - 1;
        RefreshGenerateMonsterDictionary();
        return go;
    }
    protected void RefreshGenerateMonsterDictionary()
    {
        if (TotalGenerateMonster == 0)
        {
            throw new System.Exception("So Luong Monster Cua Factory Chua Duoc Thiet Lap");
        }
        foreach (var type in GenerateMonster)
        {
            if (type.Value == 0)
            {
                GenerateMonster.Remove(type.Key);
                break;
            }
        }
    }
}
