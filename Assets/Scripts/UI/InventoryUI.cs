using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Button closeButton;

    void Start()
    {
        closeButton.onClick.AddListener(OnClickCloseButton);
    }

    private void OnClickCloseButton()
    {
        UIManager.Instance.RemoveCurrentOpenUI(gameObject);
    }
}
