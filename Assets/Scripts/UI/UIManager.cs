using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public StatusViewUI statusViewUi;
    public InventoryUI inventoryUI;
    public EquipmentUI equipmentUI;
    public StatusUI statusUI;

    private Stack<GameObject> openUIStack = new Stack<GameObject>();

    void Start()
    {
        statusViewUi = GetComponentInChildren<StatusViewUI>(true);
        inventoryUI = GetComponentInChildren<InventoryUI>(true);
        equipmentUI = GetComponentInChildren<EquipmentUI>(true);
        statusUI = GetComponentInChildren<StatusUI>(true);
    }

    public void AddCurrentOpenUI(GameObject uiObject)
    {
        if (openUIStack.Contains(uiObject))
        {
            Debug.Log($"�̹� {uiObject.name}�� �����ֽ��ϴ�.");
            return;
        }

        openUIStack.Push(uiObject);
        uiObject.SetActive(true);
        Debug.Log($"{uiObject.name}�� ���Ƚ��ϴ�.");
    }

    public void RemoveCurrentOpenUI(GameObject uiObject)
    {
        if (openUIStack.Contains(uiObject))
        {
            if (openUIStack.Peek() != uiObject)
            {
                var tempStack = new Stack<GameObject>();
                while (openUIStack.Count > 0 && openUIStack.Peek() != uiObject)
                {
                    tempStack.Push(openUIStack.Pop());
                }

                if (openUIStack.Count > 0)
                {
                    var target = openUIStack.Pop();
                    target.SetActive(false);
                    Debug.Log($"{target.name}�� �������ϴ�.");
                }

                while (tempStack.Count > 0)
                {
                    openUIStack.Push(tempStack.Pop());
                }
            }
            else
            {
                var target = openUIStack.Pop();
                target.SetActive(false);
                Debug.Log($"{target.name}�� �������ϴ�.");
            }
        }
        else
        {
            Debug.LogWarning($"{uiObject.name}�� ���� ���� ���� �ʽ��ϴ�.");
        }
    }

    private void CloseUI()
    {
        if (openUIStack.Count > 0)
        {
            var topmostUI = openUIStack.Pop();
            topmostUI.SetActive(false);
            Debug.Log($"{topmostUI.name}�� �������ϴ�.");
        }
        else
        {
            Debug.Log("���� UI�� �����ϴ�.");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseUI();
        }
    }
}