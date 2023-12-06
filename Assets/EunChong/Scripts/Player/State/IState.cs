//상태들의 최상위 인터페이스.
public interface IState
{
    void OperateEnter();
    void OperateUpdate();
    void OperateExit();
}