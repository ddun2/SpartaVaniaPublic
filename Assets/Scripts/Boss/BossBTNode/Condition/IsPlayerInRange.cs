using UnityEngine;

public class IsPlayerInRange : BTCondition
{
    private Transform bossTransform;
    private Transform playerTransform;
    private float detectionRange;

    public IsPlayerInRange(Transform bossTransform, Transform playerTransform, float detectionRange)
        : base(() => Vector3.Distance(bossTransform.position, playerTransform.position) <= detectionRange)
    {
        this.bossTransform = bossTransform;
        this.playerTransform = playerTransform;
        this.detectionRange = detectionRange;
    }
}
