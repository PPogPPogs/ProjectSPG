using UnityEngine;

public class TimeBasedFade : MonoBehaviour
{
	public SpriteRenderer backgroundImageRenderer; // ��� �̹����� SpriteRenderer ������Ʈ
	public Sprite targetSprite;    // ������ ��������Ʈ
	public float fadeDuration = 1.0f; // ���̵� ȿ�� ���� �ð�

	private float fadeStartTime; // ���̵� ���� �ð�
	private bool isFading = false; // ���̵� �� ����

	private Color initialColor; // �ʱ� ����

	private void Start()
	{
		initialColor = backgroundImageRenderer.color; // �ʱ� ���� ����
	}

	private void Update()
	{
		if (isFading)
		{
			float elapsed = Time.time - fadeStartTime;
			float normalizedTime = Mathf.Clamp01(elapsed / fadeDuration);

			Color color = backgroundImageRenderer.color;
			color.a = Mathf.Lerp(1.0f - initialColor.a, 1.0f, normalizedTime); // ����

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
			targetSprite = null; // ���̵� �ƿ� �� ����� �����
		}
	}
}
