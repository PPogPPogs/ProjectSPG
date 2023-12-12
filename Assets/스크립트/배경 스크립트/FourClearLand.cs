using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourClearLand : MonoBehaviour
{

    private bool fourClear = false;
    public Sprite newSprite; // ������ ��������Ʈ�� �Ҵ��� ����

    void Start()
    {
        ChangeSprite();
    }
    // �ٸ� �ڵ�� �Բ� ȣ��� �� �ִ� �Լ�
    public void ChangeSprite()
    {
        fourClear = PlayerPrefs.GetInt("fourClear", 0) == 1;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (fourClear)
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
