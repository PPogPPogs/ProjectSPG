using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MonsterzonLight : MonoBehaviour
{
    public Light2D[] sunLights; // 2���� 2D ������ ���
    public Color[] lightColors; // �� ������ �ʱ� ����
    public float transitionDuration = 2.0f; // ���� ��ȯ �ð� (��)

    private void Start()
    {


        
        LoadLightSettings();

       
    }

    // ù ��° ���Ͱ� �׾��� �� ȣ��Ǵ� �Լ�
    public void FirstMonsterDied()
    {
        // ù ��° ���Ͱ� �׾��� �� ���� ���� ����
        SaveLightSettings(0, lightColors[5]);
        sunLights[0].color = lightColors[5];
    }

    // �� ��° ���Ͱ� �׾��� �� ȣ��Ǵ� �Լ�
    public void SecondMonsterDied()
    {
        // �� ��° ���Ͱ� �׾��� �� ���� ���� ����
        SaveLightSettings(0, lightColors[6]);
        sunLights[0].color = lightColors[6];
    }

    public void ThirdMonsterDied()
    {
        // �� ��° ���Ͱ� �׾��� �� ���� ���� ����
        SaveLightSettings(0, lightColors[7]);
        sunLights[0].color = lightColors[7];

    }

    public void Clear()
    {
        SaveLightSettings(1, lightColors[1]);
        sunLights[1].color = lightColors[1];
        // ��ȭ������ �����ܰ��� ������ ���ϴ� ����
    }

    // ���� ���� ����
    private void SaveLightSettings(int lightIndex, Color lightColor)
    {
        // �� ���� ���� ���� �� �ε����� ����Ͽ� ���� ����
        PlayerPrefs.SetFloat("LightColorR" + lightIndex, lightColor.r);
        PlayerPrefs.SetFloat("LightColorG" + lightIndex, lightColor.g);
        PlayerPrefs.SetFloat("LightColorB" + lightIndex, lightColor.b);
        PlayerPrefs.SetFloat("LightColorA" + lightIndex, lightColor.a);

        PlayerPrefs.Save(); // ����� ������ ����
    }

    // ����� ���� ���� �ҷ�����
    private void LoadLightSettings()
    {
        for (int i = 0; i < sunLights.Length; i++)
        {
            float colorR = PlayerPrefs.GetFloat("LightColorR" + i, lightColors[i].r);
            float colorG = PlayerPrefs.GetFloat("LightColorG" + i, lightColors[i].g);
            float colorB = PlayerPrefs.GetFloat("LightColorB" + i, lightColors[i].b);
            float colorA = PlayerPrefs.GetFloat("LightColorA" + i, lightColors[i].a);

            // �ҷ��� ���� ���� ����
            sunLights[i].color = new Color(colorR, colorG, colorB, colorA);
        }
    }
}
