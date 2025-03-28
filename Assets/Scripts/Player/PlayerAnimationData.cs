using System;
using UnityEngine;

[Serializable]
public class PlayerAnimationData
{
    [SerializeField] private string idlePrameterName = "Idle";
    [SerializeField] private string runPrameterName = "Run";

    [SerializeField] private string comboAttackPrameterName = "ComboAttack";
    [SerializeField] private string comboPrameterName = "Combo";

    public int IdleParameterHash { get; private set; }
    public int RunParameterHash { get; private set; }

    public int ComboAttackParameterHash { get; private set; }
    public int ComboParameterHash { get; private set; }


    public void Initialize()
    {
        IdleParameterHash = Animator.StringToHash(idlePrameterName);
        RunParameterHash = Animator.StringToHash(runPrameterName);

        ComboAttackParameterHash = Animator.StringToHash(comboAttackPrameterName);
        ComboParameterHash = Animator.StringToHash(comboPrameterName);
    }
}