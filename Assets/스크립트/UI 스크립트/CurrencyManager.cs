using UnityEngine;
using UnityEngine.UI; // UI ���� ���ӽ����̽� �߰�
using System.Collections.Generic;


public class CurrencyManager : MonoBehaviour
{
    public Currency[] startingCurrencies;
    private Dictionary<Currency.CurrencyType, int> currencyDictionary = new Dictionary<Currency.CurrencyType, int>();

    // UI Text ��Ҹ� ������ ���� ����
    public Text GoldText;
    public Text WoodText;
    public Text ironText;

    private void Awake()
    {
        InitializeCurrencies();
        UpdateCurrencyText();
    }

    private void InitializeCurrencies()
    {
        foreach (var currency in startingCurrencies)
        {
            currencyDictionary[currency.type] = currency.initialAmount;
        }
    }

    public int GetCurrencyAmount(Currency.CurrencyType type)
    {
        if (currencyDictionary.ContainsKey(type))
        {
            return currencyDictionary[type];
        }
        return 0;
    }

    public void AddCurrency(Currency.CurrencyType type, int amount)
    {
        if (currencyDictionary.ContainsKey(type))
        {
            currencyDictionary[type] += amount;
        }
        // �̰��� ��ȭ �߰� �� �̺�Ʈ �Ǵ� �α� ��� ���� �߰��� �� ����

        // UI Text ������Ʈ
        UpdateCurrencyText();
    }

    public bool SpendCurrency(Currency.CurrencyType type, int amount)
    {
        if (currencyDictionary.ContainsKey(type) && currencyDictionary[type] >= amount)
        {
            currencyDictionary[type] -= amount;
            // �̰��� �Һ��� ��ȭ�� ���� ���� ������ �߰��� �� ����

            // UI Text ������Ʈ
            UpdateCurrencyText();

            return true;
        }
        return false;
    }

    // UI Text ������Ʈ �Լ�
    private void UpdateCurrencyText()
    {
        // UI Text�� ���� ��� ��ȭ�� ������Ʈ
        GoldText.text = GetCurrencyAmount(Currency.CurrencyType.Gold).ToString();
        WoodText.text = GetCurrencyAmount(Currency.CurrencyType.Wood).ToString();
        ironText.text = GetCurrencyAmount(Currency.CurrencyType.iron).ToString();

    }

    // ���� �� �ε� ����� �߰��� �� ����
}

