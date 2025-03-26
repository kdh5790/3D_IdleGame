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

    // 스테이지 변경 함수(버튼 이벤트)
    private void ChangeStage(int stage)
    {
        // 스테이지 변경
        StageManager.Instance.ChangeStage(stage);

        int currentStage = StageManager.Instance.CurrentStage;

        // 현재 스테이지의 버튼 비활성화
        for (int i = 0; i < stageButtons.Count; i++)
        {
            int stageIndex = i + 1;
            stageButtons[i].interactable = (stageIndex != currentStage);
        }

        // 메인 UI의 스테이지 텍스트 업데이트
        UIManager.Instance.statusViewUi.SetStageText();
    }
}
