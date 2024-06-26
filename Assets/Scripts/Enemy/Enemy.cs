using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyType
{
    Melee,
    Range
}

public class Enemy : MonoBehaviour
{
    [field: Header("EnemyData")]
    [field: SerializeField] public EnemySO Data { get; private set; }
    [field: SerializeField] public Transform projectileSpawnPoint;
    [field: SerializeField] public EnemyType attackType;

    [field: Header("Animation")]
    [field: SerializeField] public EnemyAnimationData AnimationData { get; private set; }
    [field: SerializeField] public GameObject exclamationMark;
    [field: SerializeField] public ParticleSystem customEffect;

    [field: Header("Sound")]
    [field: SerializeField] public AudioClip sword;
    [field: SerializeField] public AudioClip bowFire;
    [field: SerializeField] public AudioClip bowString;
    [field: SerializeField] public AudioClip[] Idlesounds;
    

    public Rigidbody2D Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public SpriteRenderer Renderer { get; private set; }
    public EnemyHealth Health { get; private set; }

    public bool isDie = false;

    private EnemyStateMachine stateMachine;

    private void Awake()
    {
        Physics2D.IgnoreCollision(GetComponent<CapsuleCollider2D>(), GetComponentsInChildren<CapsuleCollider2D>()[1]);

        AnimationData.Initialize();

        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponentInChildren<Animator>();
        Renderer = GetComponentInChildren<SpriteRenderer>();
        Health = GetComponent<EnemyHealth>();
   
        stateMachine = new EnemyStateMachine(this);
    }

    private void Start()
    {
        stateMachine.ChangeState(stateMachine.IdleState);
        Health.OnDie += OnDie;
        Health.OnHit += OnHit;
    }

    private void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }

    public void CreateProjectile()
    {
        GameObject go = Instantiate(stateMachine.Enemy.Data.projectilePrefab);
        go.transform.position = projectileSpawnPoint.position;       
    }

    private void OnHit()
    {
        stateMachine.Enemy.Animator.SetTrigger(stateMachine.Enemy.AnimationData.HitParameterHash);
        EffectManager.Instance.PlayOneShot(customEffect, Rigidbody.transform.position + new Vector3(0, 1f));
        Rigidbody.AddForce(-(stateMachine.Target.transform.position - gameObject.transform.position).normalized * 1f ,ForceMode2D.Impulse);
    }

    private void OnDie()
    {
        stateMachine.Enemy.Animator.SetTrigger(stateMachine.Enemy.AnimationData.DieParameterHash);
        isDie = true;
        Destroy(gameObject, 1f);
    }

    private void OnDestroy()
    {
        GameManager.instance.OnEnemyDie();
    }

    public void OnExclamationMark()
    {
        StartCoroutine(ToggleExclamationMark());
    }

    IEnumerator ToggleExclamationMark()
    {
        exclamationMark.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        exclamationMark.SetActive(false);
    }
}
