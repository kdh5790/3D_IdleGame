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
    /// ������ �ؽ�Ʈ �ʱ�ȭ ����
    /// </summary>
    /// <param name="damage">���� ������</param>
    public void Initialized(BigInteger damage)
    {
        moveSpeed = 70f; // �ؽ�Ʈ�� ���� �̵��ϴ� �ӵ�

        text.text = Utility.FormatBigNumber(damage);

        // ���� ������Ʈ�� ������ ���� ���� �̵�(�ؽ�Ʈ�� UI�� �����°��� ����)
        transform.SetAsFirstSibling();

        StartCoroutine(MoveText());

        Destroy(gameObject, 2f);
    }

    private IEnumerator MoveText()
    {
        // �ı� �Ǳ� ������ ����ؼ� ���� �̵�
        while (true)
        {
            transform.Translate(new UnityEngine.Vector3(0, moveSpeed * Time.deltaTime, 0));

            yield return null;
        }
    }
}
