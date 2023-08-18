using UnityEngine;

public class Dungeonportal : MonoBehaviour
{
    private MonsterPortal monsterPortal; // MonsterPortal ��ũ��Ʈ ����
    private Animator portalAnimator;
    public string PlaySceneName;


    private void Start()
    {
        portalAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ClosePortal();
        }
    }

    private void ClosePortal()
    {
        monsterPortal = FindObjectOfType<MonsterPortal>();
        portalAnimator.SetBool("IsOpen", false);
        UnityEngine.SceneManagement.SceneManager.LoadScene(PlaySceneName);


    }
}
