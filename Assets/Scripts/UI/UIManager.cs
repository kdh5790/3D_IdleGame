public class UIManager : Singleton<UIManager>
{
    public StatusViewUI statusViewUi;

    void Start()
    {
        statusViewUi = GetComponentInChildren<StatusViewUI>();
    }
}
