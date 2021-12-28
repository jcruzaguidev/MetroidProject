using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationId
{
    Idle = 0,
    Run = 1,
    PrepareJump = 2,
    Jump = 3
}

public class AnimatorController : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Play(AnimationId animationId)
    {
        _animator.Play(animationId.ToString());
    }
}