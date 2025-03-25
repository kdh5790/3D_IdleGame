using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIButtons : MonoBehaviour
{
    [SerializeField] private Button inventoryButton;
    [SerializeField] private Button equipmentButton;
    [SerializeField] private Button statusButton;

    private void Start()
    {
        inventoryButton.onClick.AddListener(OnClickInventoryButton);
        equipmentButton.onClick.AddListener(OnClickEquipmentButton);
        statusButton.onClick.AddListener(OnClickStatusButton);
    }

    private void OnClickInventoryButton()
    {
        Debug.Log("�κ��丮 ����");
    }

    private void OnClickEquipmentButton()
    {
        Debug.Log("���â ����");

    }

    private void OnClickStatusButton()
    {
        Debug.Log("����â ����");

    }
}
