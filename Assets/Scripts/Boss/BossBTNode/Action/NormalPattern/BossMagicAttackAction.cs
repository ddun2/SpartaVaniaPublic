using System.Collections;
using UnityEngine;

public class BossMagicAttackAction : BTAction<BossMagicAttackAction>
{
    private Transform bossTransform;
    private Transform playerTransform;
    private Animator anim;
    private float magicAttackRange;
    public GameObject lightningMagicPrefab;
    public BTNodeState CurrentState { get; private set; }
    private bool isCasting = false; // �߰��� ����
    private MonoBehaviour monoBehaviour; // �ڷ�ƾ�� �����ϱ� ���� MonoBehaviour ����

    public BossMagicAttackAction(MonoBehaviour monoBehaviour, Transform bossTransform, Transform playerTransform,
        float magicAttackRange, Animator anim, GameObject lightningMagicPrefab)
         : base(self => self.MagicAttack(bossTransform, playerTransform, magicAttackRange, anim, lightningMagicPrefab))
    {
        this.monoBehaviour = monoBehaviour;
        this.bossTransform = bossTransform;
        this.playerTransform = playerTransform;
        this.magicAttackRange = magicAttackRange;
        this.anim = anim;
        this.lightningMagicPrefab = lightningMagicPrefab;
    }

    private BTNodeState MagicAttack(Transform bossTransform,
        Transform playerTransform, float magicAttackRange, Animator anim, GameObject lightningMagicPrefab)
    {
        if(CurrentState == BTNodeState.Running)
        {
            CurrentState = BTNodeState.Failure;
            return BTNodeState.Failure;
        }
        else
        {
            // ������ �÷��̾�� �����ߴ��� Ȯ��
            if (Vector3.Distance(bossTransform.position, playerTransform.position) <= magicAttackRange && !isCasting)
            {
                // ������ �÷��̾�� �����ϸ� ���� ���� �ִϸ��̼��� �����ϰ� ���¸� �������� ��ȯ
                anim.SetTrigger("Cast");
                monoBehaviour.StartCoroutine(CastMagicAfterDelay(0.35f)); // 0.2�� �Ŀ� ������ ����
                isCasting = true; // ���� ������ ���۵�
                monoBehaviour.StartCoroutine(Cooldown()); // ��ٿ� �ڷ�ƾ ����
                CurrentState = BTNodeState.Success;
                anim.SetBool("IsIdle", true);
                return BTNodeState.Success;
            }
        }

        CurrentState = BTNodeState.Success;
        return BTNodeState.Success;
    }

    
    private IEnumerator CastMagicAfterDelay(float delay)
    {
        CurrentState = BTNodeState.Running;
        yield return new WaitForSeconds(delay);
        GameObject magic = GameObject.Instantiate(lightningMagicPrefab, playerTransform.position + Vector3.up * 1.8f, Quaternion.identity);
        GameObject.Destroy(magic, 1.5f);
    }

    // ��ٿ� �ڷ�ƾ
    private IEnumerator Cooldown()
    {
        CurrentState = BTNodeState.Running;
        yield return new WaitForSeconds(1.5f); // 2�� ���� ���
        isCasting = false; // ��ٿ��� ������ isCasting�� false�� ����
    }
}

