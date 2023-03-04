using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using UnityEngine.Pool;


public class GenericFactory<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField]
    private T prefab;


    
    public T GetNewInstance()
    {
        return Instantiate(prefab);
    }
}
