using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(transform.gameObject);

        }
        else
        {
            instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }
    }
}
