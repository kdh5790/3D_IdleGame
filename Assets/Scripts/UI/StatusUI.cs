using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
    [SerializeField] private Button closeButton;

    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI atkText;
    [SerializeField] private TextMeshProUGUI goldText;

    [Header("Health Upgrade")]
    [SerializeField] private Button healthUpgradeButton;
    [SerializeField] private TextMeshProUGUI healthPriceText;
    private BigInteger healthUpgradeCost = 1500;

    [Header("Damage Upgrade")]
    [SerializeField] private Button atkUpgradeButton;
    [SerializeField] private TextMeshProUGUI atkPriceText;
    private BigInteger atkUpgradeCost = 1500;

    void Start()
    {
        closeButton.onClick.AddListener(OnClickCloseButton);
        healthUpgradeButton.onClick.AddListener(OnClickHealthUpgrade);
        atkUpgradeButton.onClick.AddListener(OnClickAtkUpgrade);
    }

    private void OnEnable()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        healthText.text = $"{Utility.FormatBigNumber(Player.Instance.Status.CurrentHealth)}/{Utility.FormatBigNumber(Player.Instance.Status.MaxHealth)}";
        atkText.text = $"{Utility.FormatBigNumber(Player.Instance.Status.AttackPower)}";
        goldText.text = $"{Utility.FormatBigNumber(Player.Instance.Status.Gold)}";

        healthPriceText.text = $"{Utility.FormatBigNumber(healthUpgradeCost)}";
        atkPriceText.text = $"{Utility.FormatBigNumber(atkUpgradeCost)}";
    }

    private void OnClickCloseButton()
    {
        UIManager.Instance.RemoveCurrentOpenUI(gameObject);
    }


    private void OnClickHealthUpgrade()
    {
        if (Player.Instance.Status.Gold >= healthUpgradeCost)
        {
            Player.Instance.Status.Gold -= healthUpgradeCost;

            Player.Instance.Status.MaxHealth += BigInteger.Divide(Player.Instance.Status.MaxHealth * 30, 100);

            healthUpgradeCost = BigInteger.Divide(healthUpgradeCost * 150, 100);

            UpdateUI();
        }
        else
        {
            Debug.Log("골드가 부족합니다.");
        }
    }

    private void OnClickAtkUpgrade()
    {
        if (Player.Instance.Status.Gold >= atkUpgradeCost)
        {
            Player.Instance.Status.Gold -= atkUpgradeCost;

            Player.Instance.Status.AttackPower += BigInteger.Divide(Player.Instance.Status.AttackPower * 30, 100);

            atkUpgradeCost = BigInteger.Divide(atkUpgradeCost * 150, 100);

            UpdateUI();
        }
        else
        {
            Debug.Log("골드가 부족합니다.");
        }
    }
}
