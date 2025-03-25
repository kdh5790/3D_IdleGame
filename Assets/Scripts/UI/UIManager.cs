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
            Debug.Log($"이미 {uiObject.name}가 열려있습니다.");
            return;
        }

        openUIStack.Push(uiObject);
        uiObject.SetActive(true);
        Debug.Log($"{uiObject.name}가 열렸습니다.");
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
                    Debug.Log($"{target.name}가 닫혔습니다.");
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
                Debug.Log($"{target.name}가 닫혔습니다.");
            }
        }
        else
        {
            Debug.LogWarning($"{uiObject.name}는 현재 열려 있지 않습니다.");
        }
    }

    private void CloseUI()
    {
        if (openUIStack.Count > 0)
        {
            var topmostUI = openUIStack.Pop();
            topmostUI.SetActive(false);
            Debug.Log($"{topmostUI.name}가 닫혔습니다.");
        }
        else
        {
            Debug.Log("닫을 UI가 없습니다.");
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