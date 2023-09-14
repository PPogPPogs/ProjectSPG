using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CurrencyManager : MonoBehaviour
{
    public Currency[] startingCurrencies;
    private Dictionary<Currency.CurrencyType, int> currencyDictionary = new Dictionary<Currency.CurrencyType, int>();

    // UI Text 요소를 연결할 변수 선언
    public Text GoldText;
    public Text WoodText;
    public Text ironText;

    // 각 재화의 변수 추가
    public int Wood { get; private set; }
    public int Gold { get; private set; }
    public int Iron { get; private set; }

    // CurrencyManager 클래스의 인스턴스 변수 추가
    public static CurrencyManager Instance { get; private set; }

    private void Awake()
    {
        InitializeCurrencies();
        UpdateCurrencyText();
        Instance = this; // 인스턴스 설정
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

            // 각 재화에 대한 변수 업데이트
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

        // UI Text 업데이트
        UpdateCurrencyText();
    }

    public bool SpendCurrency(Currency.CurrencyType type, int amount)
    {
        if (currencyDictionary.ContainsKey(type) && currencyDictionary[type] >= amount)
        {
            currencyDictionary[type] -= amount;

            // 각 재화에 대한 변수 업데이트
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

            // UI Text 업데이트
            UpdateCurrencyText();

            return true;
        }
        return false;
    }

    // UI Text 업데이트 함수
    private void UpdateCurrencyText()
    {
        // UI Text에 현재 골드, 나무, 철광석 재화량 업데이트
        GoldText.text = GetCurrencyAmount(Currency.CurrencyType.Gold).ToString();
        WoodText.text = GetCurrencyAmount(Currency.CurrencyType.Wood).ToString();
        ironText.text = GetCurrencyAmount(Currency.CurrencyType.Iron).ToString();
    }

    // 저장 및 로드 기능을 추가할 수 있음
}
