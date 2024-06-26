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
    private bool isCasting = false; // 추가된 변수
    private MonoBehaviour monoBehaviour; // 코루틴을 시작하기 위한 MonoBehaviour 참조

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
            // 보스가 플레이어에게 도달했는지 확인
            if (Vector3.Distance(bossTransform.position, playerTransform.position) <= magicAttackRange && !isCasting)
            {
                // 보스가 플레이어에게 도달하면 마법 공격 애니메이션을 실행하고 상태를 성공으로 반환
                anim.SetTrigger("Cast");
                monoBehaviour.StartCoroutine(CastMagicAfterDelay(0.35f)); // 0.2초 후에 마법을 생성
                isCasting = true; // 마법 공격이 시작됨
                monoBehaviour.StartCoroutine(Cooldown()); // 쿨다운 코루틴 시작
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

    // 쿨다운 코루틴
    private IEnumerator Cooldown()
    {
        CurrentState = BTNodeState.Running;
        yield return new WaitForSeconds(1.5f); // 2초 동안 대기
        isCasting = false; // 쿨다운이 끝나면 isCasting을 false로 설정
    }
}

