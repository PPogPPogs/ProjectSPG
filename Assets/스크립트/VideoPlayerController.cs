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
        // 동영상 재생이 끝날 때 호출되는 콜백 함수
        videoImage.gameObject.SetActive(false); // RawImage를 비활성화하여 숨김
    }
}
