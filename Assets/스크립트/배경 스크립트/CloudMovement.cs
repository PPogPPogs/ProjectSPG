using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
	public float speed = 1.0f; // 이동 속도
	public Camera mainCamera; // 카메라 참조
	public SpriteRenderer cloud1; // 첫 번째 구름 스프라이트 렌더러
	public SpriteRenderer cloud2; // 두 번째 구름 스프라이트 렌더러
	public float repeatDistance = 20.0f; // 구름 반복 간격

	private void Update()
	{
		// 카메라의 상대 좌표계로 이동
		float moveDelta = speed * Time.deltaTime;
		cloud1.transform.Translate(mainCamera.transform.right * moveDelta);
		cloud2.transform.Translate(mainCamera.transform.right * moveDelta);

		// 카메라의 상대 좌표계 위치를 기준으로 반복
		if (cloud1.transform.position.x - mainCamera.transform.position.x >= repeatDistance)
		{
			Vector3 newPosition = mainCamera.transform.TransformPoint(new Vector3(-repeatDistance, 0, 0));
			cloud1.transform.position = new Vector3(newPosition.x, cloud1.transform.position.y, cloud1.transform.position.z);
		}

		if (cloud2.transform.position.x - mainCamera.transform.position.x >= repeatDistance)
		{
			Vector3 newPosition = mainCamera.transform.TransformPoint(new Vector3(-repeatDistance, 0, 0));
			cloud2.transform.position = new Vector3(newPosition.x, cloud2.transform.position.y, cloud2.transform.position.z);
		}
	}
}
