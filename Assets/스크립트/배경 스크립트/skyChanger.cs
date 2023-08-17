using UnityEngine;

public class skyChanger : MonoBehaviour
{
	public SpriteRenderer spriteRenderer; // ��������Ʈ ������ ������Ʈ
	public float fadeDuration = 1.0f; // fade out/in ���� �ð� (��)
	public float fadeOutStartTime = 0.0f; // fade out ���� �ð�
	public float fadeInStartTime = 12.0f; // fade in ���� �ð�

	private bool fadingOut = false;
	private bool fadingIn = false;

	private void Start()
	{
		// spriteRenderer�� �Ҵ���� ���� ��� ���� GameObject���� ã�Ƽ� �Ҵ�
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

			if (currentHour >= fadeOutStartTime && currentHour < fadeInStartTime && !fadingOut)
			{
				Debug.Log("��ħ�� �Ǿ����ϴ�.");

				fadingIn = false;
				fadingOut = true;
				StartCoroutine(FadeOutSprite());
			}
			else if (currentHour >= fadeInStartTime && !fadingIn)
			{
				Debug.Log("���� �Ǿ����ϴ�.");

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

		spriteRenderer.color = endColor; // ������ �������� ����
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

		spriteRenderer.color = endColor; // ������ ���������� ����
	}
}
