using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EarthQuakeRockBoss : MonoBehaviour
{
    float earthQuakeRadiusRatio;
    Vector3 targetLocalScale;
    internal void setScaleTarget(float earthQuakeRadiusRatio)

    {
        this.earthQuakeRadiusRatio = earthQuakeRadiusRatio;
        targetLocalScale = transform.localScale * earthQuakeRadiusRatio;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(transform.localScale, targetLocalScale) <= 0.2)
        {
            Destroy(gameObject);
        }
        transform.localScale = Vector3.Lerp(transform.localScale, targetLocalScale, 2 * Time.deltaTime);

        
    }

    private void OnDisable()
    {
        DestroyEvent.Invoke();
    }
    public UnityEvent DestroyEvent;
}
