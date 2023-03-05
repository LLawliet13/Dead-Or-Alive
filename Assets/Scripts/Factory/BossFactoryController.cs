using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BossFactoryController : ScriptableObject
{
    public int[] BossOrder;// thu tu cac boss se xuat hien // vi hien tai chi co 1 boss nen kha vo nghia
    //duong dan luu cac scriptable object cua boss
    public string TypesPath;
}
