using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
	public float speed = 1.0f; // 이동 속도
	public SpriteRenderer cloud1; // 첫 번째 구름 스프라이트 렌더러
	public SpriteRenderer cloud2; // 두 번째 구름 스프라이트 렌더러
	public float repeatDistance = 20.0f; // 구름 반복 간격

	private void Update()
	{
		// 오른쪽으로 이동
		float moveDelta = speed * Time.deltaTime;
		cloud1.transform.Translate(Vector3.right * moveDelta);
		cloud2.transform.Translate(Vector3.right * moveDelta);

		// 첫 번째 구름이 반복 간격만큼 오른쪽으로 이동하면 위치를 두 번째 구름 뒤로 옮김
		if (cloud1.transform.position.x >= repeatDistance)
		{
			cloud1.transform.position = new Vector3(cloud2.transform.position.x - repeatDistance, cloud1.transform.position.y, cloud1.transform.position.z);
		}

		// 두 번째 구름이 반복 간격만큼 오른쪽으로 이동하면 위치를 첫 번째 구름 뒤로 옮김
		if (cloud2.transform.position.x >= repeatDistance)
		{
			cloud2.transform.position = new Vector3(cloud1.transform.position.x - repeatDistance, cloud2.transform.position.y, cloud2.transform.position.z);
		}
	}
}
