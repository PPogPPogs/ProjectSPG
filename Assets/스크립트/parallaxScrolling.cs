using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
	public Transform background; // ��� Transform
	public Transform player;     // �÷��̾� Transform
	public float scrollSpeed = 1.0f; // ��ũ�� �ӵ�

	private Vector3 initialBackgroundPosition;
	private Vector3 initialPlayerPosition;

	private void Start()
	{
		initialBackgroundPosition = background.position;
		initialPlayerPosition = player.position;
	}

	private void Update()
	{
		// �÷��̾��� ������ ��ȭ�� ���
		float movementDelta = player.position.x - initialPlayerPosition.x;

		// ����� x �������� ��ũ��
		Vector3 backgroundPosition = initialBackgroundPosition;
		backgroundPosition.x += movementDelta * scrollSpeed;
		background.position = backgroundPosition;
	}
}
