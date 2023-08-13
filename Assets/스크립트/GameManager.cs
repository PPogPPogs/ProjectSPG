using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static string selectedCharacter;
    public static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    instance = obj.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SelectCharacter(string characterName)
    {
        selectedCharacter = characterName;
    }

    public void StartGame()
    {
        if (!string.IsNullOrEmpty(selectedCharacter))
        {
            LoadScene("PlayScene");
        }
        else
        {
            Debug.Log("캐릭터를 선택해주세요!");
        }
    }
}


