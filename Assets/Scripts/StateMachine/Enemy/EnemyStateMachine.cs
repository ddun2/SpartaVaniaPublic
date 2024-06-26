using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    public Enemy Enemy { get; }
    public GameObject Target { get; private set; }
    public Health Health { get; private set; }
    public EnemyIdleState IdleState { get; }
    public EnemyChasingState ChasingState { get; }
    public EnemyAttackState AttackState { get; }
    public EnemyWanderState WanderState { get; }

    public Vector2 MovementInput { get; private set; }
    public float MovementSpeed { get; private set; }
    public float WalkSpeedModifier { get; set; }
    public float RunSpeedModifier { get; set; }

    public EnemyStateMachine(Enemy enemy)
    {
        this.Enemy = enemy;
        Target = GameObject.FindGameObjectWithTag("Player");
        Health = Target.GetComponent<Health>();

        IdleState = new EnemyIdleState(this);
        ChasingState = new EnemyChasingState(this);
        AttackState = new EnemyAttackState(this);
        WanderState = new EnemyWanderState(this);

        MovementSpeed = Enemy.Data.BaseSpeed;
        WalkSpeedModifier = Enemy.Data.WalkSpeedModifier;
        RunSpeedModifier = Enemy.Data.RunSpeedModifier;
    }
}
