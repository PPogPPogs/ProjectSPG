using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeClearLand : MonoBehaviour
{

    private bool threeClear = false;
    public Sprite newSprite; // 변경할 스프라이트를 할당할 변수

    void Start()
    {
        ChangeSprite();
    }
    // 다른 코드와 함께 호출될 수 있는 함수
    public void ChangeSprite()
    {
        threeClear = PlayerPrefs.GetInt("ThreeClear", 0) == 1;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (threeClear)
        {
            // 스프라이트 변경
            if (spriteRenderer != null && newSprite != null)
            {
                spriteRenderer.sprite = newSprite;
            }
            else
            {
                Debug.LogError("SpriteRenderer 또는 newSprite가 비어 있습니다.");
            }
        }
    }


}
