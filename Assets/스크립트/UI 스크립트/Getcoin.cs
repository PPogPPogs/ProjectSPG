using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Getcoin : MonoBehaviour
{
    public int goldReward = 1;
    private Rigidbody2D rb; // 화살의 Rigidbody2D 컴포넌트


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
            // 화살을 생성한 후 isArrow 변수를 false로 설정하여 추가 생성을 방지합니다.
           
            // 화살의 Rigidbody2D를 중력을 제외하고 모든 움직임과 회전을 얼립니다.
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
