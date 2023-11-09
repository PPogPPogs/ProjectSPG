using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodonCreateButton : MonoBehaviour
{
    public GameObject Godonprefab; // 인스펙터에서 프리팹을 할당하세요.
    public Transform CreatePositon; // 인스펙터에서 프리팹을 생성할 위치를 할당하세요.

    // Update is called once per frame
    public void OnClick()
    {
        GameObject Godon = Instantiate(Godonprefab, CreatePositon.position, Quaternion.identity);
    }
    
        
    
}
