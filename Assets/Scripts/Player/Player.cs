using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;  // 지면 레이어 검사에 사용
    [SerializeField] private float rayLength = 10f;

    [field: SerializeField] public PlayerSO Data { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

    [Header("Sound Effects")]
    [SerializeField] public AudioClip playerDeathSound;
    [SerializeField] public AudioClip playerJumpSound;
    [SerializeField] public AudioClip playerRollSound;
    [SerializeField] public AudioClip playerHitSound;

    public Animator Animator { get; private set; }
    public PlayerController playerController { get; private set; }
    public PlayerForceReceiver PlayerForceReceiver { get; private set; }

    public PlayerStateMachine stateMachine;

    public Health health { get; private set; }

    public Rigidbody2D rb { get; set; }
    public CapsuleCollider2D capsuleCollider { get; set; }
    public SpriteRenderer spriteRenderer { get; set; }

    public float fallThroughDuration = 0.5f; // Time passing through platforms
    public float castLength = 100f;
    public float castDistance = 0.1f; // 레이캐스트 시작 위치의 오프셋 거리
    [SerializeField] private Collider2D currentPlatformCollider; // platform's Collider2D player is at

    private List<Collider2D> colliderList = new List<Collider2D>();


    private void Awake()
    {
        AnimationData.Initialize();
        Animator = GetComponentInChildren<Animator>();
        playerController = GetComponent<PlayerController>();
        PlayerForceReceiver = GetComponentInChildren<PlayerForceReceiver>();

        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponentInChildren<CapsuleCollider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        health = GetComponent<Health>();

        stateMachine = new PlayerStateMachine(this);

        groundLayer = groundLayer = LayerMask.GetMask("Ground");

    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        stateMachine.ChangeState(stateMachine.IdleState);
        health.OnDie += OnDie;
        health.OnHit += OnHurt;
    }

    private void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();

        if (Input.GetKey(KeyCode.DownArrow) && Input.GetKeyDown(KeyCode.Space) && currentPlatformCollider != null)
        {
            // 현재 플랫폼 아래에 다른 플랫폼이 있는 경우에만 통과
            if (IsPlatformBelow())
            {
                StartCoroutine(FallThroughPlatform());
            }
        }

    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }

    void OnDie()
    {
        Animator.SetBool("Die", true);
        Animator.SetBool("Hurt", false);
        enabled = false;
        SoundManager.Instance.PlaySoundAtLocation(playerDeathSound,transform.position);
    }
    public void OnHurt()
    {
        stateMachine.ChangeState(stateMachine.HurtState);
    }

    public bool IsGrounded ()
    {
        Vector2 position = new Vector2(transform.position.x, capsuleCollider.bounds.min.y);
        Vector2 boxSize = new Vector2(capsuleCollider.bounds.size.x, rayLength);
        RaycastHit2D hit = Physics2D.BoxCast(position, boxSize, 0f, Vector2.down, rayLength, groundLayer);

        return hit.collider != null;
    }

    public IEnumerator FallThroughPlatform()
    {
        Physics2D.IgnoreCollision(capsuleCollider, currentPlatformCollider, true);

        yield return new WaitForSeconds(fallThroughDuration);

        Physics2D.IgnoreCollision(capsuleCollider, currentPlatformCollider, false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlatformEffector2D>() != null)
        {
            Debug.Log("Entered Platform");
            currentPlatformCollider = other;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlatformEffector2D>() != null)
        {
            Debug.Log("Exited Platform");
            currentPlatformCollider = null;
        }
    }

    public  bool IsPlatformBelow()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, castLength);

        if (hit.collider != null && hit.collider != currentPlatformCollider && hit.collider.GetComponent<PlatformEffector2D>() != null)
        {
            return true;
        }

        return false;
    }
    public bool IsAnimationFinished(string animationName)
    {
        AnimatorStateInfo currentState = Animator.GetCurrentAnimatorStateInfo(0);
        return currentState.IsName(animationName) && currentState.normalizedTime >= 1f;
    }

}