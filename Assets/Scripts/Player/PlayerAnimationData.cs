using System;
using UnityEngine;

[Serializable]
public class PlayerAnimationData
{
    [SerializeField] private string moveParameterName = "@Move";
    [SerializeField] private string idlePrameterName = "Idle";
    [SerializeField] private string runPrameterName = "Run";

    [SerializeField] private string attackPrameterName = "@Attack";
    [SerializeField] private string comboAttackPrameterName = "ComboAttack";

    public int MoveParameterHash { get; private set; }
    public int IdleParameterHash { get; private set; }
    public int RunParameterHash { get; private set; }


    public int AttackParameterHash { get; private set; }
    public int ComboAttackParameterHash { get; private set; }


    public void Initialize()
    {
        MoveParameterHash = Animator.StringToHash(moveParameterName);
        IdleParameterHash = Animator.StringToHash(idlePrameterName);
        RunParameterHash = Animator.StringToHash(runPrameterName);

        AttackParameterHash = Animator.StringToHash(attackPrameterName);
        ComboAttackParameterHash = Animator.StringToHash(comboAttackPrameterName);
    }
}