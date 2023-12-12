using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeClearLand : MonoBehaviour
{

    private bool threeClear = false;
    public Sprite newSprite; // ������ ��������Ʈ�� �Ҵ��� ����

    void Start()
    {
        ChangeSprite();
    }
    // �ٸ� �ڵ�� �Բ� ȣ��� �� �ִ� �Լ�
    public void ChangeSprite()
    {
        threeClear = PlayerPrefs.GetInt("ThreeClear", 0) == 1;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (threeClear)
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
