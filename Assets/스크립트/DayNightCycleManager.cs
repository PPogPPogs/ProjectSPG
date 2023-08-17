using UnityEngine;

public class DayNightCycleManager : MonoBehaviour
{
	public float startX = -5.0f; // 시작 x 좌표
	public float endX = 5.0f;    // 종료 x 좌표
	public float duration = 12.0f; // 이동에 걸리는 시간 (12시간)

	private Vector3 startPosition;
	private Vector3 endPosition;

	private CalendarManager calendarManager; // CalendarManager 컴포넌트 참조

	private void Start()
	{
		startPosition = new Vector3(startX, 0, 0);
		endPosition = new Vector3(endX, 0, 0);

		// CalendarManager 컴포넌트 참조
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
