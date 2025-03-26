using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    private float moveSpeed;

    [SerializeField] private TextMeshProUGUI text;

    /// <summary>
    /// 데미지 텍스트 초기화 설정
    /// </summary>
    /// <param name="damage">입힌 데미지</param>
    public void Initialized(BigInteger damage)
    {
        moveSpeed = 70f; // 텍스트가 위로 이동하는 속도

        text.text = Utility.FormatBigNumber(damage);

        // 현재 오브젝트의 순서를 가장 위로 이동(텍스트가 UI를 가리는것을 방지)
        transform.SetAsFirstSibling();

        StartCoroutine(MoveText());

        Destroy(gameObject, 2f);
    }

    private IEnumerator MoveText()
    {
        // 파괴 되기 전까지 계속해서 위로 이동
        while (true)
        {
            transform.Translate(new UnityEngine.Vector3(0, moveSpeed * Time.deltaTime, 0));

            yield return null;
        }
    }
}
