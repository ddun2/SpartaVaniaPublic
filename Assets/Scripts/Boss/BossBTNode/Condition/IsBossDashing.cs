public class IsBossDashing : BTCondition
{
    private BossDashAction bossDashAction;

    public IsBossDashing(BossDashAction bossDashAction) : base(() => bossDashAction.CurrentState == BTNodeState.Running)
    {
        this.bossDashAction = bossDashAction;
    }
}
