using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCaculation : MonoBehaviour
{
    public static float caculateAnimationDuration(Animator animator, string animationName)
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if(clip.name== animationName)
                    return clip.length;
        }
        return 0;
    }
}
