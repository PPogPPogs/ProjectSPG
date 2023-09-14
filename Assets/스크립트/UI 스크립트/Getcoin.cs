using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Getcoin : MonoBehaviour
{
    public int goldReward = 1;
    private Rigidbody2D rb; // ȭ���� Rigidbody2D ������Ʈ


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GetCoin();
            Destroy(gameObject);

        }

        if (other.CompareTag("Ground"))
        {
            // ȭ���� ������ �� isArrow ������ false�� �����Ͽ� �߰� ������ �����մϴ�.
           
            // ȭ���� Rigidbody2D�� �߷��� �����ϰ� ��� �����Ӱ� ȸ���� �󸳴ϴ�.
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.gravityScale = 0f;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;

        }
    }
        public void GetCoin()
    {
        CurrencyManager currencyManager = FindObjectOfType<CurrencyManager>();
        if (currencyManager != null)
        {
            currencyManager.AddCurrency(Currency.CurrencyType.Gold, goldReward);
           
        }
    }
}
