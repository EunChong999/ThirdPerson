public class StateWalking : IState
{
    public void OperateEnter()
    {
        PlayerMovementController.Instance.animator.SetBool("Walk", true);
    }

    public void OperateExit()
    {
        PlayerMovementController.Instance.animator.SetBool("Walk", false);
    }

    public void OperateUpdate()
    {

    }
}
