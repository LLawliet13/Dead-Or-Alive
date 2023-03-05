using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class GenericFactory<T> : MonoBehaviour where T : EnemyStatus
{
    [SerializeField]
    protected T prefab;
    protected MonsterType[] monsterTypes;
    //loai monster - so luong 
    protected Dictionary<MonsterType, int> GenerateMonster;
    // so luong quai se tao ra
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
