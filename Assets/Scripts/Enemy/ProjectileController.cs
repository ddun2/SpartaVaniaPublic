using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private GameObject target;
    
    private float speed = 10.0f;
    private Rigidbody2D _rigidbody;
    private Vector2 dir;
    private float angle;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // 위치 플레이어 중앙으로 수정하기
        dir = (target.transform.position + new Vector3(0f, 1.5f) - transform.position).normalized; 
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }

    void Update()
    {
        _rigidbody.velocity = dir * speed;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌 시 소멸 처리
        if (collision.CompareTag("Player"))
        {
            if (collision.gameObject.TryGetComponent(out Health health))
            {
                // 대미지 SO에서 가져오기
                health.TakeDamage(10);                
            }            
            Destroy(gameObject);
        }

        if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

    // TODO :: 
    // 스폰포인트 수정 (현재 이미지만 뒤집어서 포인트는 안바뀜)
    // 근거리 공격 구현 (Boxcast 활용?)
    // 피격 처리 (체력 감소, 넉백, 애니메이션 재생)
    // 사망 처리 (애니메이션 재생, 소멸)
    // 파티클 (플레이어 발견, 피격, 공격)

    // 시간이 남는다면 =>
    // 몬스터 종류 늘리기 (금방할듯)
    // 근접 공격 추가 (초기 구상, 랜덤하게 발동 or 일정 패턴으로 강공격 느낌?)
    // +++
    // 레벨디자인 고민해보기
    // UI (디자인 보기 좋게, 설정 메뉴)

}
