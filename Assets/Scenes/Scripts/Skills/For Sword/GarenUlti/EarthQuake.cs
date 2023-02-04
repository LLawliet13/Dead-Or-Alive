using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthQuake : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    float duration;
    void Start()
    {
        Animator animator = GetComponent<Animator>();
        duration = AnimationCaculation.caculateAnimationDuration(animator, "run");
        animator.Play("run");
        Destroy(gameObject, duration);
        Destroy(sword, duration);

    }
    public GameObject sword;
    // Update is called once per frame
    void Update()
    {
        
    }
}
