using UnityEngine;

public class DayNightCycleManager : MonoBehaviour
{
	public float startX = -5.0f; // ���� x ��ǥ
	public float endX = 5.0f;    // ���� x ��ǥ
	public float duration = 12.0f; // �̵��� �ɸ��� �ð� (12�ð�)

	private Vector3 startPosition;
	private Vector3 endPosition;

	private CalendarManager calendarManager; // CalendarManager ������Ʈ ����

	private void Start()
	{
		startPosition = new Vector3(startX, 0, 0);
		endPosition = new Vector3(endX, 0, 0);

		// CalendarManager ������Ʈ ����
		calendarManager = GetComponent<CalendarManager>();
	}

	private void Update()
	{
		if (calendarManager != null)
		{
			float progress = Mathf.Clamp01(calendarManager.GetHour() / 12.0f);
			transform.position = Vector3.Lerp(startPosition, endPosition, progress);
		}
	}
}
