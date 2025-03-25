using System;
using UnityEngine;

[Serializable]
public class EnemyAnimationData
{
    [SerializeField] private string idlePrameterName = "Idle";
    [SerializeField] private string runPrameterName = "Run";

    [SerializeField] private string attackPrameterName = "Attack";

    [SerializeField] private string dieParameterName = "Die";

    public int IdleParameterHash { get; private set; }
    public int RunParameterHash { get; private set; }

    public int AttackParameterHash { get; private set; }


    public void Initialize()
    {
        IdleParameterHash = Animator.StringToHash(idlePrameterName);
        RunParameterHash = Animator.StringToHash(runPrameterName);

        AttackParameterHash = Animator.StringToHash(attackPrameterName);
    }
}
