using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusViewUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI atkText;
    [SerializeField] private TextMeshProUGUI stageText;

    [SerializeField] private Image healthImage;

    private void Start()
    {
        Player.Instance.Status.OnStatusChanged += UpdateUI;

        UpdateUI();
    }

    private void UpdateUI()
    {
        SetHealthText();
        SetGoldText();
        SetAtkText();
        SetStageText();
        UpdateHealthBar();
    }

    public void SetHealthText() => healthText.text = $"{Utility.FormatBigNumber(Player.Instance.Status.CurrentHealth)}/{Utility.FormatBigNumber(Player.Instance.Status.TotalMaxHealth)}";

    public void SetGoldText() => goldText.text = Utility.FormatBigNumber(Player.Instance.Status.Gold);

    public void SetAtkText() => atkText.text = Utility.FormatBigNumber(Player.Instance.Status.TotalAttackPower);

    public void SetStageText() => stageText.text = $"Stage{StageManager.Instance.CurrentStage}";

    public void UpdateHealthBar()
    {
        float currentFloat = (float)BigInteger.Divide(Player.Instance.Status.CurrentHealth * 1000, Player.Instance.Status.TotalMaxHealth) / 1000f;
        healthImage.fillAmount = Mathf.Clamp01(currentFloat);
    }
}
