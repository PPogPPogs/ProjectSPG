using UnityEngine;


[System.Serializable]
public class Currency
{
    public enum CurrencyType
    {
        Gold,Wood,iron
        // �߰� ��ȭ ������ ������ �� ����
    }

    public CurrencyType type;
    public int amount;
    public int initialAmount; // ���� ���� �� �ʱⰪ
}
