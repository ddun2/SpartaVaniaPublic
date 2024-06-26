using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyAnimationData
{
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string walkParameterName = "Walk";
    [SerializeField] private string attack1ParameterName = "Attack1";
    [SerializeField] private string attack2ParameterName = "Attack2";
    [SerializeField] private string HitParameterName = "Hit";
    [SerializeField] private string DieParameterName = "Die";

    public int IdleParameterHash { get; private set; }
    public int WalkParameterHash { get; private set; }
    public int Attack1ParameterHash { get; private set; }
    public int Attack2ParameterHash { get; private set; }
    public int HitParameterHash { get; private set; }
    public int DieParameterHash { get; private set; }

    public void Initialize()
    {
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        WalkParameterHash = Animator.StringToHash(walkParameterName);
        Attack1ParameterHash = Animator.StringToHash(attack1ParameterName);
        Attack2ParameterHash = Animator.StringToHash(attack2ParameterName);
        HitParameterHash = Animator.StringToHash(HitParameterName);
        DieParameterHash = Animator.StringToHash(DieParameterName);

    }
}
