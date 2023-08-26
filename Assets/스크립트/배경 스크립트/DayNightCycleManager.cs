using UnityEngine;

public class TimeBasedFade : MonoBehaviour
{
	public SpriteRenderer backgroundImageRenderer; // 배경 이미지의 SpriteRenderer 컴포넌트
	public Sprite targetSprite;    // 변경할 스프라이트
	public float fadeDuration = 1.0f; // 페이드 효과 지속 시간

	private float fadeStartTime; // 페이드 시작 시간
	private bool isFading = false; // 페이드 중 여부

	private Color initialColor; // 초기 색상

	private void Start()
	{
		initialColor = backgroundImageRenderer.color; // 초기 색상 저장
	}

	private void Update()
	{
		if (isFading)
		{
			float elapsed = Time.time - fadeStartTime;
			float normalizedTime = Mathf.Clamp01(elapsed / fadeDuration);

			Color color = backgroundImageRenderer.color;
			color.a = Mathf.Lerp(1.0f - initialColor.a, 1.0f, normalizedTime); // 변경

			backgroundImageRenderer.color = color;

			if (normalizedTime >= 1.0f)
			{
				isFading = false;
			}
		}
	}

	public void StartFadeIn()
	{
		if (!isFading)
		{
			fadeStartTime = Time.time;
			isFading = true;
		}
	}

	public void StartFadeOut()
	{
		if (!isFading)
		{
			fadeStartTime = Time.time;
			isFading = true;
			targetSprite = null; // 페이드 아웃 후 배경이 사라짐
		}
	}
}
