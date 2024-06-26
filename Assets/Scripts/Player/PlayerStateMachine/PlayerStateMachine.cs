using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }

    public Vector2 MovementInput { get; set; }
    public float MovementSpeed { get; private set; }
    public float RotationDamping { get; private set; }
    public float MovementSpeedModifier { get; set; } = 1f;

    public float JumpForce { get; set; }

    public bool IsAttacking { get; set; }

    public bool IsRoll { get; set; } = false;
    public bool Jumped { get; set; } = false;
    public bool DoubleJumped { get; set; } = false;
    public int ComboIndex { get; set; }
    public Transform MainCamTransform { get; set; }

    public PlayerHurtState HurtState { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerWalkState WalkState { get; private set; }
    public PlayerRollState RollState { get; private set; }

    public PlayerJumpState JumpState { get; private set; }
    public PlayerDoubleJumpState DoubleJumpState { get; private set; }
    public PlayerFallState FallState { get; private set; }

    public PlayerComboAttackState ComboAttackState { get; private set; }

    public PlayerStateMachine(Player player)
    {
        Player = player;

        MainCamTransform = Camera.main.transform;

        HurtState = new PlayerHurtState(this);

        IdleState = new PlayerIdleState(this);
        WalkState = new PlayerWalkState(this);
        RollState = new PlayerRollState(this);

        DoubleJumpState = new PlayerDoubleJumpState(this);
        JumpState = new PlayerJumpState(this);
        FallState = new PlayerFallState(this);

        ComboAttackState = new PlayerComboAttackState(this);

        MovementSpeed = player.Data.GroundData.BaseSpeed;
    }
}
