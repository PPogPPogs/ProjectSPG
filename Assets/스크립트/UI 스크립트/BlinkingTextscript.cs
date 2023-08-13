using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BlinkingText : MonoBehaviour
{
    private Text textComponent;
    private bool isFading = false;

    private void Start()
    {
        textComponent = GetComponent<Text>();
        StartCoroutine(BlinkText());
    }

    private IEnumerator BlinkText()
    {
        while (true)
        {
            if (isFading)
            {
                // 텍스트를 흐리게 만듭니다.
                for (float alpha = 1f; alpha >= 0f; alpha -= 0.1f)
                {
                    textComponent.color = new Color(textComponent.color.r, textComponent.color.g, textComponent.color.b, alpha);
                    yield return new WaitForSeconds(0.1f);
                }
            }
            else
            {
                // 텍스트를 원래대로 돌립니다.
                for (float alpha = 0f; alpha <= 1f; alpha += 0.1f)
                {
                    textComponent.color = new Color(textComponent.color.r, textComponent.color.g, textComponent.color.b, alpha);
                    yield return new WaitForSeconds(0.1f);
                }
            }

            isFading = !isFading; // 흐릿하게 만들기와 원래대로 돌리기를 번갈아가며 실행합니다.
        }
    }
}