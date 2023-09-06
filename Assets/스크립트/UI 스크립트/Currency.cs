using UnityEngine;


[System.Serializable]
public class Currency
{
    public enum CurrencyType
    {
        Gold,Wood,iron
        // 추가 재화 종류를 정의할 수 있음
    }

    public CurrencyType type;
    public int amount;
    public int initialAmount; // 게임 시작 시 초기값
}
