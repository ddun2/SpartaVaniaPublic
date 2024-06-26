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
        // ��ġ �÷��̾� �߾����� �����ϱ�
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
        // �浹 �� �Ҹ� ó��
        if (collision.CompareTag("Player"))
        {
            if (collision.gameObject.TryGetComponent(out Health health))
            {
                // ����� SO���� ��������
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
    // ��������Ʈ ���� (���� �̹����� ����� ����Ʈ�� �ȹٲ�)
    // �ٰŸ� ���� ���� (Boxcast Ȱ��?)
    // �ǰ� ó�� (ü�� ����, �˹�, �ִϸ��̼� ���)
    // ��� ó�� (�ִϸ��̼� ���, �Ҹ�)
    // ��ƼŬ (�÷��̾� �߰�, �ǰ�, ����)

    // �ð��� ���´ٸ� =>
    // ���� ���� �ø��� (�ݹ��ҵ�)
    // ���� ���� �߰� (�ʱ� ����, �����ϰ� �ߵ� or ���� �������� ������ ����?)
    // +++
    // ���������� ����غ���
    // UI (������ ���� ����, ���� �޴�)

}
