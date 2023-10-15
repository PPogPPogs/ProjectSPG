using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fogmove : MonoBehaviour
{
    public float speed = 0.5f;
    private float width;
    public float maxX = 5.0f; // 최대 X 위치
    public float fadeDuration = 2.0f; // 나타나는데 걸릴 시간
    private float currentAlpha = 0f; // 현재 알파값
    private Renderer objectRenderer; // 오브젝트의 Renderer 컴포넌트

    // Start is called before the first frame update
    void Start()
    {
        width = transform.localScale.x;
        objectRenderer = GetComponent<Renderer>();
        objectRenderer.material.color = new Color(objectRenderer.material.color.r, objectRenderer.material.color.g, objectRenderer.material.color.b, objectRenderer.material.color.a);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        if (transform.position.x >= maxX)
        {
            
            StartCoroutine(FadeIn());
            Vector2 offset = new Vector2(width * 8f, 0);
            transform.position = (Vector2)transform.position - offset;
        }
    }

    IEnumerator FadeIn()
    {
        float timer = 0f;

        while (currentAlpha < 1.0f)
        {
            currentAlpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            objectRenderer.material.color = new Color(objectRenderer.material.color.r, objectRenderer.material.color.g, objectRenderer.material.color.b, currentAlpha);

            timer += Time.deltaTime;
            yield return null;
        }
    }
}
