using UnityEngine;
[CreateAssetMenu(fileName = "Boss", menuName ="Characters/Boss")]
public class BossSO : ScriptableObject
{
    [field: SerializeField] public int NormalAttackDamage;
    [field: SerializeField] public float NormalAttackRange { get; private set; } = 1.5f;
    [field: SerializeField] public float MagicAttackRange { get; private set; } = 4f;
    [field: SerializeField] public float WalkChasingRange { get; private set; } = 10f;
    [field: SerializeField] public float DashChasingRange { get; private set; } = 20f;

    [field: SerializeField][field: Range(0f, 50f)] public float MovementSpeed { get; private set; }
    [field: SerializeField][field: Range(0f, 2f)] public float WalkSpeedModifier { get; private set; }
    [field: SerializeField][field: Range(0f, 2f)] public float DashSpeedModifier { get; private set; }
}
