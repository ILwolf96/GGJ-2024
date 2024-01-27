using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGraphic : MonoBehaviour
{
    [SerializeField] Animator WalkAnimator;
    [SerializeField] Animator handAnimator;

    public bool _isWalking = true;
    public bool WalkAnimationActive;
    private void Update()
    {
        if (_isWalking == false) { WalkAnimator.Play("Idle"); }
        else if (_isWalking && !WalkAnimationActive) { WalkAnimator.Play("Walk"); WalkAnimationActive = true; }

    }
}
