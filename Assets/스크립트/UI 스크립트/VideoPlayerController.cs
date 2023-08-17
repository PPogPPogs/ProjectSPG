using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{
    public RawImage videoImage;
    public VideoPlayer videoPlayer;

    private void Start()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
        videoPlayer.Play();
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        // ������ ����� ���� �� ȣ��Ǵ� �ݹ� �Լ�
        videoImage.gameObject.SetActive(false); // RawImage�� ��Ȱ��ȭ�Ͽ� ����
    }
}
