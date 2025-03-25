using System;
using UnityEngine;

[Serializable]
public class PlayerAnimationData
{
    [SerializeField] private string moveParameterName = "@Move"; // substate 진입을 위한 파라미터
    [SerializeField] private string idlePrameterName = "Idle";
    [SerializeField] private string runPrameterName = "Run";

    [SerializeField] private string attackPrameterName = "@Attack"; // substate 진입을 위한 파라미터
    [SerializeField] private string comboAttackPrameterName = "ComboAttack";
    [SerializeField] private string comboPrameterName = "Combo";

    public int MoveParameterHash { get; private set; }
    public int IdleParameterHash { get; private set; }
    public int RunParameterHash { get; private set; }

    public int AttackParameterHash { get; private set; }
    public int ComboAttackParameterHash { get; private set; }
    public int ComboParameterHash { get; private set; }


    public void Initialize()
    {
        MoveParameterHash = Animator.StringToHash(moveParameterName);
        IdleParameterHash = Animator.StringToHash(idlePrameterName);
        RunParameterHash = Animator.StringToHash(runPrameterName);

        AttackParameterHash = Animator.StringToHash(attackPrameterName);
        ComboAttackParameterHash = Animator.StringToHash(comboAttackPrameterName);
        ComboParameterHash = Animator.StringToHash(comboPrameterName);
    }
}