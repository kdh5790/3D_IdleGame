using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject[] walls; // 0 - Up 1 -Down 2 - Right 3- Left

    private bool hasEnemy = false;

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
