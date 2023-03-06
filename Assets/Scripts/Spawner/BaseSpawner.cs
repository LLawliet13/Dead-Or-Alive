using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpawner : MonoBehaviour
{
    protected Controller status;
    public enum Controller
    {
        TurnOn,
        TurnOff
    }
    public void SettingController(Controller value)
    {
        status = value;
    }
    [SerializeField]
    protected float RangeFromPlayer;
    protected Transform player;
    protected Vector3 RandomLocation()
    {
        float randomAngle = Random.Range(0f, 360f);
        Vector3 position = player.position + Quaternion.Euler(0, 0, randomAngle) * new Vector3(RangeFromPlayer, 0, 0);
        return position;
    }
}
