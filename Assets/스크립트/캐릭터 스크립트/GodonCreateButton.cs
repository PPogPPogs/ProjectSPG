using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodonCreateButton : MonoBehaviour
{
    public GameObject Godonprefab; // �ν����Ϳ��� �������� �Ҵ��ϼ���.
    public Transform CreatePositon; // �ν����Ϳ��� �������� ������ ��ġ�� �Ҵ��ϼ���.

    // Update is called once per frame
    public void OnClick()
    {
        GameObject Godon = Instantiate(Godonprefab, CreatePositon.position, Quaternion.identity);
    }
    
        
    
}
