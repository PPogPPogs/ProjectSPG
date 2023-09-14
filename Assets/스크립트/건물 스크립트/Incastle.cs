using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Incastle : MonoBehaviour
{
    public Canvas canvasToEnable; // Ȱ��ȭ�� ĵ����

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                if (canvasToEnable != null)
                {
                    canvasToEnable.gameObject.SetActive(true);
                }
            }
        }
    }
}
