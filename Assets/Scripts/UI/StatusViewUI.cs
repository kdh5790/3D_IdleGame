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

    [SerializeField] private Image healthImage;

    private void Start()
    {
        Player.Instance.Status.OnStatusChanged += UpdateUI;
    }

    private void UpdateUI()
    {
        SetHealthText();
        SetGoldText();
        SetAtkText();
        UpdateHealthBar();
    }

    public void SetHealthText() => healthText.text = $"{FormatBigNumber(Player.Instance.Status.CurrentHealth)}/{FormatBigNumber(Player.Instance.Status.MaxHealth)}";

    public void SetGoldText() => goldText.text = FormatBigNumber(Player.Instance.Status.Gold);

    public void SetAtkText() => atkText.text = FormatBigNumber(Player.Instance.Status.AttackPower);

    public void UpdateHealthBar()
    {
        float currentFloat = (float)BigInteger.Divide(Player.Instance.Status.CurrentHealth * 1000, Player.Instance.Status.MaxHealth) / 1000f;
        healthImage.fillAmount = Mathf.Clamp01(currentFloat);
    }

    private string FormatBigNumber(BigInteger number)
    {
        if (number < 1000)
        {
            return number.ToString();
        }

        string[] units = { "", "k", "m", "b", "t", "q", "Q", "s", "S", "o", "n" };
        int unitIndex = 0;
        BigInteger divisor = 1000;

        while (number >= divisor && unitIndex < units.Length - 1)
        {
            number /= divisor;
            unitIndex++;
        }

        string formatString = unitIndex == 0 ? "{0}" : "{0:0.#}{1}";
        return string.Format(formatString, number, units[unitIndex]);
    }
}
