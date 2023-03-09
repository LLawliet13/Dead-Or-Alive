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
        nextCollisionDamageTime = Time.time;
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
    public int atk { get; set; }
    
    private float nextCollisionDamageTime;
    public float DelayCollisionDamageTime;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&&Time.time> nextCollisionDamageTime)
        {
            collision.GetComponent<CharacterStatus>().TakeDamage(atk);
            nextCollisionDamageTime = Time.time + DelayCollisionDamageTime;
        }
    }
    private void OnDisable()
    {
        DestroyEvent.Invoke();
    }
    public UnityEvent DestroyEvent;
}
