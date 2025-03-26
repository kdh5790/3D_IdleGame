using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    private float moveSpeed;

    [SerializeField] private TextMeshProUGUI text;

    public void Initialized(BigInteger damage)
    {
        moveSpeed = 70f;

        text.text = Utility.FormatBigNumber(damage);

        transform.SetAsFirstSibling();

        StartCoroutine(MoveText());

        Destroy(gameObject, 2f);
    }

    private IEnumerator MoveText()
    {
        while (true)
        {
            transform.Translate(new UnityEngine.Vector3(0, moveSpeed * Time.deltaTime, 0));

            yield return null;
        }
    }
}
