using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Text 클래스를 사용하기 위해 추가

public class GetCurrency : MonoBehaviour
{
    public Text GoldText;
    public Text WoodText;
    public Text IronText;

    // Start is called before the first frame update
    void Start()
    {
        // 게임 시작 시 필요한 초기화 코드 추가
    }

    // Update is called once per frame
    void Update()
    {
        CurrencyManager currencyManager = FindObjectOfType<CurrencyManager>();
        if (currencyManager != null)
        {
            // 각 화폐의 양을 가져온 후 Text 메서드를 호출하여 표시
            GoldText.text = currencyManager.GetCurrencyAmount(Currency.CurrencyType.Gold).ToString();
            WoodText.text = currencyManager.GetCurrencyAmount(Currency.CurrencyType.Wood).ToString();
            IronText.text = currencyManager.GetCurrencyAmount(Currency.CurrencyType.Iron).ToString();
        }
    }
}
