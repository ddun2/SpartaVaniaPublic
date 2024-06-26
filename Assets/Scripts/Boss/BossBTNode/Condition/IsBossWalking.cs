public class IsBossWalking : BTCondition
{
    private BossWalkAction bossWalkAction;

    public IsBossWalking(BossWalkAction bossWalkAction) : base(() => bossWalkAction.CurrentState == BTNodeState.Running)
    {
        this.bossWalkAction = bossWalkAction;
    }
}