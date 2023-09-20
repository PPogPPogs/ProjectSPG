using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Text Ŭ������ ����ϱ� ���� �߰�

public class GetCurrency : MonoBehaviour
{
    public Text GoldText;
    public Text WoodText;
    public Text IronText;

    // Start is called before the first frame update
    void Start()
    {
        // ���� ���� �� �ʿ��� �ʱ�ȭ �ڵ� �߰�
    }

    // Update is called once per frame
    void Update()
    {
        CurrencyManager currencyManager = FindObjectOfType<CurrencyManager>();
        if (currencyManager != null)
        {
            // �� ȭ���� ���� ������ �� Text �޼��带 ȣ���Ͽ� ǥ��
            GoldText.text = currencyManager.GetCurrencyAmount(Currency.CurrencyType.Gold).ToString();
            WoodText.text = currencyManager.GetCurrencyAmount(Currency.CurrencyType.Wood).ToString();
            IronText.text = currencyManager.GetCurrencyAmount(Currency.CurrencyType.Iron).ToString();
        }
    }
}
