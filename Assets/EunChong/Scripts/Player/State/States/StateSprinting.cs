public class StateSprinting : IState
{
    public void OperateEnter()
    {
        PlayerMovementController.Instance.animator.SetBool("Sprint", true);
    }

    public void OperateExit()
    {
        PlayerMovementController.Instance.animator.SetBool("Sprint", false);
    }

    public void OperateUpdate()
    {

    }
}
