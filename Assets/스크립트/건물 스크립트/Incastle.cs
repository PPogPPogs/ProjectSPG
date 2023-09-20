using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Incastle : MonoBehaviour
{
    public Image imageToEnable; // 활성화할 이미지
    

    private void Awake()
    {
        // Buildwall 스크립트를 찾아서 buildwall 변수에 할당
      
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                if (imageToEnable != null)
                {
                    imageToEnable.gameObject.SetActive(true);
                }

                
            }
        }
    }
}
