using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneClearLand : MonoBehaviour
{
    
    private bool oneClear = false;
    public Sprite newSprite; // ������ ��������Ʈ�� �Ҵ��� ����

    void Start()
    {
        ChangeSprite();
    }
    // �ٸ� �ڵ�� �Բ� ȣ��� �� �ִ� �Լ�
    public void ChangeSprite()
    {
        oneClear = PlayerPrefs.GetInt("OneClear", 0) == 1;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (oneClear)
        {
            // ��������Ʈ ����
            if (spriteRenderer != null && newSprite != null)
            {
                spriteRenderer.sprite = newSprite;
            }
            else
            {
                Debug.LogError("SpriteRenderer �Ǵ� newSprite�� ��� �ֽ��ϴ�.");
            }
        }
    }

    
}
