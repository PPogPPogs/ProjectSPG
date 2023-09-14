using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CurrencyManager : MonoBehaviour
{
    public Currency[] startingCurrencies;
    private Dictionary<Currency.CurrencyType, int> currencyDictionary = new Dictionary<Currency.CurrencyType, int>();

    // UI Text ��Ҹ� ������ ���� ����
    public Text GoldText;
    public Text WoodText;
    public Text ironText;

    // �� ��ȭ�� ���� �߰�
    public int Wood { get; private set; }
    public int Gold { get; private set; }
    public int Iron { get; private set; }

    // CurrencyManager Ŭ������ �ν��Ͻ� ���� �߰�
    public static CurrencyManager Instance { get; private set; }

    private void Awake()
    {
        InitializeCurrencies();
        UpdateCurrencyText();
        Instance = this; // �ν��Ͻ� ����
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

            // �� ��ȭ�� ���� ���� ������Ʈ
            if (type == Currency.CurrencyType.Wood)
            {
                Wood += amount;
            }
            else if (type == Currency.CurrencyType.Gold)
            {
                Gold += amount;
            }
            else if (type == Currency.CurrencyType.Iron)
            {
                Iron += amount;
            }
        }

        // UI Text ������Ʈ
        UpdateCurrencyText();
    }

    public bool SpendCurrency(Currency.CurrencyType type, int amount)
    {
        if (currencyDictionary.ContainsKey(type) && currencyDictionary[type] >= amount)
        {
            currencyDictionary[type] -= amount;

            // �� ��ȭ�� ���� ���� ������Ʈ
            if (type == Currency.CurrencyType.Wood)
            {
                Wood -= amount;
            }
            else if (type == Currency.CurrencyType.Gold)
            {
                Gold -= amount;
            }
            else if (type == Currency.CurrencyType.Iron)
            {
                Iron -= amount;
            }

            // UI Text ������Ʈ
            UpdateCurrencyText();

            return true;
        }
        return false;
    }

    // UI Text ������Ʈ �Լ�
    private void UpdateCurrencyText()
    {
        // UI Text�� ���� ���, ����, ö���� ��ȭ�� ������Ʈ
        GoldText.text = GetCurrencyAmount(Currency.CurrencyType.Gold).ToString();
        WoodText.text = GetCurrencyAmount(Currency.CurrencyType.Wood).ToString();
        ironText.text = GetCurrencyAmount(Currency.CurrencyType.Iron).ToString();
    }

    // ���� �� �ε� ����� �߰��� �� ����
}
