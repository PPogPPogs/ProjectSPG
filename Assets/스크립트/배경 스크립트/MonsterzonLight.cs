using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MonsterzonLight : MonoBehaviour
{
    public Light2D[] sunLights; // 2개의 2D 조명을 사용
    public Color[] lightColors; // 각 조명의 초기 색상
    public float transitionDuration = 2.0f; // 색상 전환 시간 (초)

    private void Start()
    {


        
        LoadLightSettings();

       
    }

    // 첫 번째 몬스터가 죽었을 때 호출되는 함수
    public void FirstMonsterDied()
    {
        // 첫 번째 몬스터가 죽었을 때 조명 설정 저장
        SaveLightSettings(0, lightColors[5]);
        sunLights[0].color = lightColors[5];
    }

    // 두 번째 몬스터가 죽었을 때 호출되는 함수
    public void SecondMonsterDied()
    {
        // 두 번째 몬스터가 죽었을 때 조명 설정 저장
        SaveLightSettings(0, lightColors[6]);
        sunLights[0].color = lightColors[6];
    }

    public void ThirdMonsterDied()
    {
        // 세 번째 몬스터가 죽었을 때 조명 설정 저장
        SaveLightSettings(0, lightColors[7]);
        sunLights[0].color = lightColors[7];

    }

    public void Clear()
    {
        SaveLightSettings(1, lightColors[1]);
        sunLights[1].color = lightColors[1];
        // 정화했을때 다음단계의 구역이 변하는 색깔
    }

    // 조명 설정 저장
    private void SaveLightSettings(int lightIndex, Color lightColor)
    {
        // 각 조명에 대한 색상 및 인덱스를 사용하여 설정 저장
        PlayerPrefs.SetFloat("LightColorR" + lightIndex, lightColor.r);
        PlayerPrefs.SetFloat("LightColorG" + lightIndex, lightColor.g);
        PlayerPrefs.SetFloat("LightColorB" + lightIndex, lightColor.b);
        PlayerPrefs.SetFloat("LightColorA" + lightIndex, lightColor.a);

        PlayerPrefs.Save(); // 변경된 데이터 저장
    }

    // 저장된 조명 설정 불러오기
    private void LoadLightSettings()
    {
        for (int i = 0; i < sunLights.Length; i++)
        {
            float colorR = PlayerPrefs.GetFloat("LightColorR" + i, lightColors[i].r);
            float colorG = PlayerPrefs.GetFloat("LightColorG" + i, lightColors[i].g);
            float colorB = PlayerPrefs.GetFloat("LightColorB" + i, lightColors[i].b);
            float colorA = PlayerPrefs.GetFloat("LightColorA" + i, lightColors[i].a);

            // 불러온 조명 설정 적용
            sunLights[i].color = new Color(colorR, colorG, colorB, colorA);
        }
    }
}
