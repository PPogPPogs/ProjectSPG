using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
	public Transform background; // 배경 Transform
	public Transform player;     // 플레이어 Transform
	public float scrollSpeed = 1.0f; // 스크롤 속도

	private Vector3 initialBackgroundPosition;
	private Vector3 initialPlayerPosition;

	private void Start()
	{
		initialBackgroundPosition = background.position;
		initialPlayerPosition = player.position;
	}

	private void Update()
	{
		// 플레이어의 움직임 변화량 계산
		float movementDelta = player.position.x - initialPlayerPosition.x;

		// 배경을 x 방향으로 스크롤
		Vector3 backgroundPosition = initialBackgroundPosition;
		backgroundPosition.x += movementDelta * scrollSpeed;
		background.position = backgroundPosition;
	}
}
