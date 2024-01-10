using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
	public float speed = 1.0f; // �̵� �ӵ�
	public Camera mainCamera; // ī�޶� ����
	public SpriteRenderer cloud1; // ù ��° ���� ��������Ʈ ������
	public SpriteRenderer cloud2; // �� ��° ���� ��������Ʈ ������
	public float repeatDistance = 20.0f; // ���� �ݺ� ����

	private void Update()
	{
		// ī�޶��� ��� ��ǥ��� �̵�
		float moveDelta = speed * Time.deltaTime;
		cloud1.transform.Translate(mainCamera.transform.right * moveDelta);
		cloud2.transform.Translate(mainCamera.transform.right * moveDelta);

		// ī�޶��� ��� ��ǥ�� ��ġ�� �������� �ݺ�
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
