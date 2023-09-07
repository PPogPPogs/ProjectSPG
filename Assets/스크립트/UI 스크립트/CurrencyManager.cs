using UnityEngine;
using UnityEngine.UI; // UI 관련 네임스페이스 추가
using System.Collections.Generic;


public class CurrencyManager : MonoBehaviour
{
    public Currency[] startingCurrencies;
    private Dictionary<Currency.CurrencyType, int> currencyDictionary = new Dictionary<Currency.CurrencyType, int>();

    // UI Text 요소를 연결할 변수 선언
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
        // 이곳에 재화 추가 시 이벤트 또는 로그 기록 등을 추가할 수 있음

        // UI Text 업데이트
        UpdateCurrencyText();
    }

    public bool SpendCurrency(Currency.CurrencyType type, int amount)
    {
        if (currencyDictionary.ContainsKey(type) && currencyDictionary[type] >= amount)
        {
            currencyDictionary[type] -= amount;
            // 이곳에 소비한 재화에 따른 게임 로직을 추가할 수 있음

            // UI Text 업데이트
            UpdateCurrencyText();

            return true;
        }
        return false;
    }

    // UI Text 업데이트 함수
    private void UpdateCurrencyText()
    {
        // UI Text에 현재 골드 재화량 업데이트
        GoldText.text = GetCurrencyAmount(Currency.CurrencyType.Gold).ToString();
        WoodText.text = GetCurrencyAmount(Currency.CurrencyType.Wood).ToString();
        ironText.text = GetCurrencyAmount(Currency.CurrencyType.iron).ToString();

    }

    // 저장 및 로드 기능을 추가할 수 있음
}

