using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemFactory<T> : MonoBehaviour where T : BaseDropItem
{
    [SerializeField]
    protected T prefab;


    /// <summary>
    /// kiem tra xem factory san sang sinh item chua
    /// </summary>
    /// <returns></returns>
    public bool CheckIfReady()
    {
        return prefab != null;
    }
    public T GetNewInstance()
    {
        return Instantiate(prefab); 
    }
}
