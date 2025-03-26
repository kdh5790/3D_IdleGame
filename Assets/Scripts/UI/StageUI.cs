using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    [SerializeField] private Button closeButton;

    [SerializeField] private List<Button> stageButtons = new List<Button>();

    void Start()
    {
        closeButton.onClick.AddListener(OnClickCloseButton);

        for (int i = 0; i < stageButtons.Count; i++)
        {
            int stageIndex = i + 1;
            stageButtons[i].onClick.AddListener(() => ChangeStage(stageIndex));
        }
    }

    private void OnEnable()
    {
        int currentStage = StageManager.Instance.CurrentStage;

        for (int i = 0; i < stageButtons.Count; i++)
        {
            int stageIndex = i + 1;
            stageButtons[i].interactable = (stageIndex != currentStage);
        }
    }

    private void OnClickCloseButton()
    {
        UIManager.Instance.RemoveCurrentOpenUI(gameObject);
    }

    private void ChangeStage(int stage)
    {
        StageManager.Instance.ChangeStage(stage);

        int currentStage = StageManager.Instance.CurrentStage;

        for (int i = 0; i < stageButtons.Count; i++)
        {
            int stageIndex = i + 1;
            stageButtons[i].interactable = (stageIndex != currentStage);
        }

        UIManager.Instance.statusViewUi.SetStageText();
    }
}
