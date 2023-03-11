using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class GenericEnemyFactory<T> : MonoBehaviour where T : EnemyStatus
{
    [SerializeField]
    protected T prefab;
    protected MonsterType[] monsterTypes;
    //loai monster - so luong // so quai tren man se duoc thiet lap co dinh max bn con de phong truong hop treo giat game vi qua tai ram
    protected Dictionary<MonsterType, int> GenerateMonster;
    /// <summary>
    /// so luong quai se tao ra, spawner se quyet dinh dieu nay
    /// </summary>
    [Header("Khong can thiet lap")]
    public int TotalGenerateMonster;
    //khoi chay factory
    public abstract void Enable();
    
    

    public T GetNewInstance()
    {
        
        return GenerateMonsterStrategy();
    }
    //quy dinh quai co the tao ra trong lan tao tiep theo
    protected abstract T GenerateMonsterStrategy();
    
    

}
