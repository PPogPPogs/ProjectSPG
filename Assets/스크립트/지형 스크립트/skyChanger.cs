using UnityEngine;

public class skyChanger : MonoBehaviour
{
	public SpriteRenderer spriteRenderer; // ½ºÇÁ¶óÀÌÆ® ·»´õ·¯ ÄÄÆ÷³ÍÆ®
	public float fadeDuration = 1.0f; // fade out/in Áö¼Ó ½Ã°£ (ÃÊ)

	private bool fadingOut = false;
	private bool fadingIn = false;

	private void Start()
	{
		// spriteRenderer°¡ ÇÒ´çµÇÁö ¾ÊÀº °æ¿ì ÇöÀç GameObject¿¡¼­ Ã£¾Æ¼­ ÇÒ´ç
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

			if (currentHour >= 0 && currentHour < 12 && !fadingIn)
			{
<<<<<<<< Updated upstream:Assets/ìŠ¤í¬ë¦½íŠ¸/ë°°ê²½ ìŠ¤í¬ë¦½íŠ¸/skyChanger.cs
				Debug.Log("¾ÆÄ§ÀÌ µÇ¾ú½À´Ï´Ù.");

				fadingIn = false;
				fadingOut = true;
				StartCoroutine(FadeOutSprite());
			}
			else if (currentHour >= 12 && !fadingIn)
			{
				Debug.Log("¹ãÀÌ µÇ¾ú½À´Ï´Ù.");

========
>>>>>>>> Stashed changes:Assets/ìŠ¤í¬ë¦½íŠ¸/ì§€í˜• ìŠ¤í¬ë¦½íŠ¸/skyChanger.cs
				fadingOut = false;
				fadingIn = true;
				StartCoroutine(FadeInSprite());
			}
			else if (currentHour >= 12 && !fadingOut)
			{
				Debug.Log("»Í»Í¾²¶ó´øÁö");
				fadingIn = false;
				fadingOut = true;
				StartCoroutine(FadeOutSprite());
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

		spriteRenderer.color = endColor; // ¿ÏÀüÇÑ Åõ¸íÀ¸·Î ¼³Á¤
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

		spriteRenderer.color = endColor; // ¿ÏÀüÇÑ ºÒÅõ¸íÀ¸·Î ¼³Á¤
	}
}
