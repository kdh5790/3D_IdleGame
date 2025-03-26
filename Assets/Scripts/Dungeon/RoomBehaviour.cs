using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject[] walls; // 0 - ��
                                                 // 1 - �Ʒ�
                                                 // 2 - ������
                                                 // 3 - ����

    private bool hasEnemy = false; // ���� �濡 ���� �����ϴ��� ����

    // DungeonGenerator Ŭ�������� ������ bool���� ���� �� ��Ȱ��ȭ
    public void UpdateRoom(bool[] status)
    {
        for (int i = 0; i < status.Length; i++)
        {
            walls[i].SetActive(!status[i]);
        }
    }

    public bool GetHasEnemy() => hasEnemy;

    public void SetHasEnemy(bool hasEnemy) => this.hasEnemy = hasEnemy;
}
