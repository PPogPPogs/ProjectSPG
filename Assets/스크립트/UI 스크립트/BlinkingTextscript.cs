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
                // �ؽ�Ʈ�� �帮�� ����ϴ�.
                for (float alpha = 1f; alpha >= 0f; alpha -= 0.1f)
                {
                    textComponent.color = new Color(textComponent.color.r, textComponent.color.g, textComponent.color.b, alpha);
                    yield return new WaitForSeconds(0.1f);
                }
            }
            else
            {
                // �ؽ�Ʈ�� ������� �����ϴ�.
                for (float alpha = 0f; alpha <= 1f; alpha += 0.1f)
                {
                    textComponent.color = new Color(textComponent.color.r, textComponent.color.g, textComponent.color.b, alpha);
                    yield return new WaitForSeconds(0.1f);
                }
            }

            isFading = !isFading; // �帴�ϰ� ������ ������� �����⸦ �����ư��� �����մϴ�.
        }
    }
}