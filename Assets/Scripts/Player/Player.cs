using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    public PlayerStatus Status { get; private set; }
    public PlayerAI AI { get; private set; }
    [field:SerializeField] public PlayerAnimationData AnimationData { get; private set; }

    private Animator anim;

    private void Awake()
    {
        Status = GetComponent<PlayerStatus>();
        AI = GetComponent<PlayerAI>();

        anim = GetComponentInChildren<Animator>();
    }

    public void StartAnimation(int animatorHash)
    {
        anim.SetBool(animatorHash, true);
    }

    public void StopAnimation(int animatorHash)
    {
        anim.SetBool(animatorHash, false);
    }
}
