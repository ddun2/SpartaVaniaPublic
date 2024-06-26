using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Characters/Enemy")]
public class EnemySO : ScriptableObject
{
    [field: SerializeField] public int Damage;
    [field: SerializeField] public int Health;
    [field: SerializeField][field: Range(0f, 3f)] public float AttackDelay { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; } = 1.5f;
    [field: SerializeField] public float TargetChasingRange { get; private set; } = 10f;

    [field: SerializeField][field: Range(0f, 10f)] public float BaseSpeed { get; private set; }
    [field: SerializeField][field: Range(0f, 2f)] public float WalkSpeedModifier {  get; private set; }
    [field: SerializeField][field: Range(0f, 2f)] public float RunSpeedModifier {  get; private set; }
        
    // TODO :: 원거리, 근거리 분리하기
    [field: SerializeField] public float projectileSpeed;
    [field: SerializeField] public GameObject projectilePrefab;
    //[field: SerializeField] public Transform projectileSpawnPoint;
}