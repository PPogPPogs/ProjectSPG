using UnityEngine;

public class skyChanger : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float fadeDuration = 1.0f;
    public float fadeOutStartTime = 6.0f;
    public float fadeInStartTime = 20.0f;

    private bool fadingOut = false;
    private bool fadingIn = false;

    private void Start()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    private void Update()
    {
        CalendarManager calendarManager = GetComponent<CalendarManager>();

        if (calendarManager != null)
        {
            int currentHour = calendarManager.GetHour();

            if (currentHour >= fadeOutStartTime && currentHour <= fadeInStartTime && !fadingOut && !fadingIn)
            {
                Debug.Log("아침이 되었습니다.");

                fadingIn = false;
                fadingOut = true;
                StartCoroutine(FadeOutSprite());
            }
            else if (currentHour >= fadeInStartTime && !fadingIn && !fadingOut)
            {
                Debug.Log("밤이 되었습니다.");

                fadingOut = false;
                fadingIn = true;
                StartCoroutine(FadeInSprite());
            }
        }
    }

    private System.Collections.IEnumerator FadeOutSprite()
    {
        float elapsedTime = 0.0f;
        Color startColor = spriteRenderer.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0.0f);

        while (elapsedTime < fadeDuration)
        {
            float t = elapsedTime / fadeDuration;
            spriteRenderer.color = Color.Lerp(startColor, endColor, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        spriteRenderer.color = endColor;
        fadingOut = false; // 페이드 아웃이 완료되면 상태 변수를 업데이트
    }

    private System.Collections.IEnumerator FadeInSprite()
    {
        float elapsedTime = 0.0f;
        Color startColor = spriteRenderer.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1.0f);

        while (elapsedTime < fadeDuration)
        {
            float t = elapsedTime / fadeDuration;
            spriteRenderer.color = Color.Lerp(startColor, endColor, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        spriteRenderer.color = endColor;
        fadingIn = false; // 페이드 인이 완료되면 상태 변수를 업데이트
    }
}
