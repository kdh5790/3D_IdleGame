using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    public PlayerStatus Status { get; private set; }

    private void Awake()
    {
        Status = GetComponent<PlayerStatus>();
    }
}
