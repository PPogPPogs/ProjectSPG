using UnityEngine;

public class PlatformController : MonoBehaviour
{
	public float highlightBrightness = 1.5f; // ���̶���Ʈ ��� ������
	public float normalBrightness = 1.0f;     // �Ϲ����� ��� ��

	private bool isHighlighted = false;
	private Color originalColor; // ���� ������ �����ϴ� ����
	private SpriteRenderer platformRenderer;

	private void Start()
	{
		platformRenderer = GetComponent<SpriteRenderer>();
		originalColor = platformRenderer.color; // �ʱ� ���� ����
		SetHighlight(false); // �ʱ� ���·� ���̶���Ʈ ����
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			SetHighlight(true); // �÷��̾ �÷����� ������ ���̶���Ʈ �ѱ�
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			SetHighlight(false); // �÷��̾ �÷������� ����� ���̶���Ʈ ����
		}
	}

	private void SetHighlight(bool highlight)
	{
		if (isHighlighted != highlight)
		{
			isHighlighted = highlight;
			// ��������Ʈ �̹����� ������ ��� ���� �����Ͽ� ���̶���Ʈ ȿ�� �ֱ�
			Color color = originalColor;
			color.r = highlight ? color.r * highlightBrightness : color.r * normalBrightness;
			color.g = highlight ? color.g * highlightBrightness : color.g * normalBrightness;
			color.b = highlight ? color.b * highlightBrightness : color.b * normalBrightness;
			platformRenderer.color = color;
		}
	}
}
