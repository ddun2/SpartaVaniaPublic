using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    [Header("���� ���۷���")]
    public Transform bossTransform;
    [field: SerializeField] public BossSO BossData { get; private set; }
    [SerializeField] SpriteRenderer bossSprite;
    public Animator anim;
    public Rigidbody2D rigidBody;
    [SerializeField] GameObject BossSpell;
    public MonoBehaviour monoBehaviour;
    public BossHP bossHP;

    [Header("�÷��̾� ���۷���")]
    public Transform playerTransform;

    [Header("Hit ����Ʈ")]
    public ParticleSystem customEffect;

    private BTSelector rootSelector; //��Ʈ
    private BTSelector specialBehaviourSelector; //Ư�� ����
    private BTRandomSelector normalBehaviourRandomSelector; //�Ϲ� ����
 
    private bool isExecuting = false;

    private void Awake()
    {
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), GetComponentsInChildren<BoxCollider2D>()[1]);
    }

    private void Start()
    {
        bossHP = GetComponent<BossHP>();
        //��Ʈ ���
        rootSelector = new BTSelector();
        
        //Ư�� ���� ���
        specialBehaviourSelector = new BTSelector();
        rootSelector.AddChild(specialBehaviourSelector);

        //�Ϲ� ���� ���
        normalBehaviourRandomSelector = new BTRandomSelector();
        rootSelector.AddChild(normalBehaviourRandomSelector);

        bossHP.OnHit += OnHit;
        bossHP.OnDie += OnDie;
        //SetUpSpecialPattern();

        SetUpNormalPattern();
    }



    //private void SetUpSpecialPattern()
    //{
    //    //Ư�� ���� : ���� ü�� 50% ���� �ߵ�
    //    BTCondition isBossHalfHP = new IsBossHalfHP(this);
    //    BTAction bossInvincible = new BossInvincible(this);
    //    BTTimerDecorator bossInvincibleTimer = new BTTimerDecorator(bossInvincible,10.0f);
    //    BTAction summonMonster = new SummonMonster(this);
    //    BTSequence bossHalfHP = new BTSequence(new List<BTNode> { isBossHalfHP, bossInvincible, summonMonster });
    //    specialBehaviourSelector.AddChild(bossHalfHP);

    //    //Ư�� ���� : ���� ü�� 20% ���� �ߵ�
    //    BTCondition isBossLowHP = new IsBossLowHP(this);
    //    BTCondition isSpecialAttackNotStarted = new IsSpecialAttackNotStarted(this);
    //    BTAction bossSpecialAttack = new SpecialAttack(this);
    //    BTSequence bossLowHP = new BTSequence(new List<BTNode> { isBossLowHP, isSpecialAttackNotStarted, bossSpecialAttack });
    //    specialBehaviourSelector.AddChild(bossLowHP);
    //}

    private void SetUpNormalPattern()
    {
        //�Ϲ� ���� : �÷��̾�� �ɾ��
        BossWalkAction bossWalk = new BossWalkAction(bossTransform, playerTransform, BossData.MovementSpeed,
            BossData.NormalAttackRange, anim,rigidBody,bossSprite);
        IsBossWalking isBossWalking = new IsBossWalking(bossWalk);
        BTInverter notWalking = new BTInverter(isBossWalking);
       //IsPlayerInRange isPlayerInRange = new IsPlayerInRange(bossTransform, playerTransform, BossData.WalkChasingRange);
        BTSequence walkToPlayerSequence = new BTSequence(new List<BTNode> { bossWalk });
        normalBehaviourRandomSelector.AddChild(walkToPlayerSequence);

        //�Ϲ� ���� : �÷��̾�� ����ϱ� 
        BossDashAction bossDash = new BossDashAction(bossTransform, playerTransform, BossData.MovementSpeed,BossData.NormalAttackRange,
            anim, rigidBody,bossSprite);
        IsBossDashing isBossDashing = new IsBossDashing(bossDash);
        BTInverter bossNotDashing = new BTInverter(isBossDashing);
        //IsPlayerInDashRange isPlayerDashRange = new IsPlayerInDashRange(bossTransform, playerTransform, BossData.NormalAttackRange);
        BTSequence dashToPlayerSequence = new BTSequence(new List<BTNode>() {bossDash });
        normalBehaviourRandomSelector.AddChild(dashToPlayerSequence);

        //�Ϲ� ���� : �÷��̾�κ��� �������� -> �۾�
        BossRunawayAction runAwayFromPlayer = new BossRunawayAction(bossTransform, playerTransform, 
        BossData.MovementSpeed,BossData.MagicAttackRange,BossData.NormalAttackRange,anim,rigidBody,bossSprite);
        IsBossRunningAway isBossRunningAway = new IsBossRunningAway(runAwayFromPlayer);
        BTInverter bossNotRunningAway = new BTInverter(isBossRunningAway);
        //BTCondition isPlayerCloseEnough = new IsPlayerCloseEnough(this);      
        BTSequence runAwayFromPlayerSequence = new BTSequence(new List<BTNode> {runAwayFromPlayer });
        normalBehaviourRandomSelector.AddChild(runAwayFromPlayerSequence);

        //�Ϲ� ���� : �Ϲݰ��� ����
        BossNormalAttackAction normalAttack = new BossNormalAttackAction(bossTransform, playerTransform, BossData.NormalAttackRange, anim, bossSprite);
        IsBossAttacking isBossAttacking = new IsBossAttacking(normalAttack);
        BTInverter bossNotAttacking = new BTInverter(isBossAttacking);
        //IsPlayerInAttackRange isPlayerInAttackRange = new IsPlayerInAttackRange(bossTransform, playerTransform, BossData.NormalAttackRange);
        BTSequence startNormalAttackSequence = new BTSequence(new List<BTNode> {normalAttack });
       normalBehaviourRandomSelector.AddChild(startNormalAttackSequence);

        ////�Ϲ� ���� : �������� ����
         BossMagicAttackAction startSpellAttack = new BossMagicAttackAction(monoBehaviour,bossTransform,playerTransform,
        BossData.MagicAttackRange,anim,BossSpell);
        IsBossCastingSpell isBossCastingSpell = new IsBossCastingSpell(startSpellAttack);
        BTInverter bossNotCastingSpell = new BTInverter(isBossCastingSpell);
        //BTCondition isPlayerFarEnoughForSpell = new IsPlayerFarEnoughForSpell(this);

        BTSequence startSpellAttackSequence = new BTSequence(new List<BTNode> { startSpellAttack });
        normalBehaviourRandomSelector.AddChild(startSpellAttackSequence);
    }

    IEnumerator WaitAndExecute(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }

    private void Update()
    {
        //rootSelector.Evaluate();
        

        if (!isExecuting)
        {
            StartCoroutine(WaitAndExecute(1f, () =>
            {
                rootSelector.Evaluate();
                isExecuting = false;
            }));
            isExecuting = true;
        }

    }

    private void OnDie()
    {
        anim.SetTrigger("Death");
        Destroy(gameObject, 1f);
    }

    private void OnHit()
    {
        EffectManager.Instance.PlayOneShot(customEffect, gameObject.transform.position + new Vector3(0, 1f));
    }
}
