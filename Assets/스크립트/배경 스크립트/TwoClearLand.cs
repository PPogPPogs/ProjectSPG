using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoClearLand : MonoBehaviour
{

    private bool twoClear = false;
    public Sprite newSprite; // ������ ��������Ʈ�� �Ҵ��� ����

    void Start()
    {
        ChangeSprite();
    }
    // �ٸ� �ڵ�� �Բ� ȣ��� �� �ִ� �Լ�
    public void ChangeSprite()
    {
        twoClear = PlayerPrefs.GetInt("TwoClear", 0) == 1;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (twoClear)
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
