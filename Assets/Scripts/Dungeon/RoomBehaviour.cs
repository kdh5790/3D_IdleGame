using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject[] walls; // 0 - 위
                                                 // 1 - 아래
                                                 // 2 - 오른쪽
                                                 // 3 - 왼쪽

    private bool hasEnemy = false; // 현재 방에 적이 존재하는지 여부

    // DungeonGenerator 클래스에서 설정된 bool값에 따라 벽 비활성화
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
