using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    public PlayerStatus Status { get; private set; }
    public PlayerAI AI { get; private set; }
    [field:SerializeField] public PlayerAnimationData AnimationData { get; private set; }

    public Animator Anim;

    private void Awake()
    {
        Status = GetComponent<PlayerStatus>();
        AI = GetComponent<PlayerAI>();
        Anim = GetComponent<Animator>();
    }

    public void StartAnimation(int animatorHash)
    {
        Anim.SetBool(animatorHash, true);
    }

    public void StopAnimation(int animatorHash)
    {
        Anim.SetBool(animatorHash, false);
    }
}
