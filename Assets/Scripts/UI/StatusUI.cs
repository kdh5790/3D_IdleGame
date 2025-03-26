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
        healthText.text = $"{Utility.FormatBigNumber(Player.Instance.Status.CurrentHealth)}/{Utility.FormatBigNumber(Player.Instance.Status.TotalMaxHealth)}";
        atkText.text = $"{Utility.FormatBigNumber(Player.Instance.Status.TotalAttackPower)}";
        goldText.text = $"{Utility.FormatBigNumber(Player.Instance.Status.Gold)}";

        healthPriceText.text = $"{Utility.FormatBigNumber(healthUpgradeCost)}";
        atkPriceText.text = $"{Utility.FormatBigNumber(atkUpgradeCost)}";
    }

    private void OnClickCloseButton()
    {
        UIManager.Instance.RemoveCurrentOpenUI(gameObject);
    }


    // 체력 업그레이드 버튼 클릭 시 실행 되는 함수
    private void OnClickHealthUpgrade()
    {
        if (Player.Instance.Status.Gold >= healthUpgradeCost)
        {
            Player.Instance.Status.Gold -= healthUpgradeCost;
            // Divide : 하나의 BigInteger 값을 다른 값으로 나눈 후 결과를 반환합니다.
            // 현재 최대 체력의 30% 증가
            Player.Instance.Status.BaseMaxHealth += BigInteger.Divide(Player.Instance.Status.BaseMaxHealth * 30, 100);

            // 현재 업그레이드 가격의 150% 증가
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
            Player.Instance.Status.BaseAttackPower += BigInteger.Divide(Player.Instance.Status.BaseAttackPower * 30, 100);

            atkUpgradeCost = BigInteger.Divide(atkUpgradeCost * 150, 100);

            UpdateUI();
        }
        else
        {
            Debug.Log("골드가 부족합니다.");
        }
    }
}
