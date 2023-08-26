using UnityEngine;

public class PlatformController : MonoBehaviour
{
	public float highlightBrightness = 1.5f; // 하이라이트 밝기 조절값
	public float normalBrightness = 1.0f;     // 일반적인 밝기 값

	private bool isHighlighted = false;
	private Color originalColor; // 원래 색상을 저장하는 변수
	private SpriteRenderer platformRenderer;

	private void Start()
	{
		platformRenderer = GetComponent<SpriteRenderer>();
		originalColor = platformRenderer.color; // 초기 색상 저장
		SetHighlight(false); // 초기 상태로 하이라이트 끄기
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			SetHighlight(true); // 플레이어가 플랫폼에 닿으면 하이라이트 켜기
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			SetHighlight(false); // 플레이어가 플랫폼에서 벗어나면 하이라이트 끄기
		}
	}

	private void SetHighlight(bool highlight)
	{
		if (isHighlighted != highlight)
		{
			isHighlighted = highlight;
			// 스프라이트 이미지의 색상의 밝기 값을 조정하여 하이라이트 효과 주기
			Color color = originalColor;
			color.r = highlight ? color.r * highlightBrightness : color.r * normalBrightness;
			color.g = highlight ? color.g * highlightBrightness : color.g * normalBrightness;
			color.b = highlight ? color.b * highlightBrightness : color.b * normalBrightness;
			platformRenderer.color = color;
		}
	}
}
