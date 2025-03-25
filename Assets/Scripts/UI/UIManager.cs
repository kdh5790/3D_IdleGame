public class UIManager : Singleton<UIManager>
{
    public StatusUI statusUi;

    void Start()
    {
        statusUi = GetComponentInChildren<StatusUI>();
    }
}
