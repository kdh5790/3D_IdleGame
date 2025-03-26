using System;
using UnityEngine;

[Serializable]
public class EnemyAnimationData
{
    // 애니메이터 파라미터 이름
    [SerializeField] private string idlePrameterName = "Idle";
    [SerializeField] private string runPrameterName = "Run";

    [SerializeField] private string attackPrameterName = "Attack";

    [SerializeField] private string dieParameterName = "Die";

    // 애니메이터 파라미터 이름을 int 값으로 받아올 변수
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
