using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
	public float speed = 1.0f; // �̵� �ӵ�
	public SpriteRenderer cloud1; // ù ��° ���� ��������Ʈ ������
	public SpriteRenderer cloud2; // �� ��° ���� ��������Ʈ ������
	public float repeatDistance = 20.0f; // ���� �ݺ� ����

	private void Update()
	{
		// ���������� �̵�
		float moveDelta = speed * Time.deltaTime;
		cloud1.transform.Translate(Vector3.right * moveDelta);
		cloud2.transform.Translate(Vector3.right * moveDelta);

		// ù ��° ������ �ݺ� ���ݸ�ŭ ���������� �̵��ϸ� ��ġ�� �� ��° ���� �ڷ� �ű�
		if (cloud1.transform.position.x >= repeatDistance)
		{
			cloud1.transform.position = new Vector3(cloud2.transform.position.x - repeatDistance, cloud1.transform.position.y, cloud1.transform.position.z);
		}

		// �� ��° ������ �ݺ� ���ݸ�ŭ ���������� �̵��ϸ� ��ġ�� ù ��° ���� �ڷ� �ű�
		if (cloud2.transform.position.x >= repeatDistance)
		{
			cloud2.transform.position = new Vector3(cloud1.transform.position.x - repeatDistance, cloud2.transform.position.y, cloud2.transform.position.z);
		}
	}
}
