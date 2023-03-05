using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CreepFactoryController : ScriptableObject
{
    //ti le ve so luong cua cac loai quai duoc tao ra
    public int[] OccurrenceRates;
    //duong dan luu cac scriptable object cua creep
    public string TypesPath;
}
