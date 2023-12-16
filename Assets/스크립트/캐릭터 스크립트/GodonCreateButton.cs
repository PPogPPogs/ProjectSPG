using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodonCreateButton : MonoBehaviour
{
    public GameObject Godonprefab; // �ν����Ϳ��� �������� �Ҵ��ϼ���.

    // �ν����Ϳ��� �������� ������ ��ġ�� �Ҵ��ϼ���.
    public float createPositionX;
    public float createPositionY;
    public float createPositionZ;

    // Update is called once per frame
    public void OnClick()
    {
        // Use the provided coordinates to set the creation position
        Vector3 createPosition = new Vector3(createPositionX, createPositionY, createPositionZ);

        // Instantiate the Godon prefab at the specified position with no rotation
        GameObject Godon = Instantiate(Godonprefab, createPosition, Quaternion.identity);
    }
}
