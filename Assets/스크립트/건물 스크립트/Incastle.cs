using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Incastle : MonoBehaviour
{
    public Image imageToEnable; // Ȱ��ȭ�� �̹���
    

    private void Awake()
    {
        // Buildwall ��ũ��Ʈ�� ã�Ƽ� buildwall ������ �Ҵ�
      
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
