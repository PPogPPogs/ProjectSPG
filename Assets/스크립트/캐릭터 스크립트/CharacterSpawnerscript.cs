using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public Transform spawnPosition;

    private void Start()
    {
        string selectedCharacter = GameManager.selectedCharacter;
        SpawnCharacter(selectedCharacter);
    }

    private void SpawnCharacter(string characterName)
    {
        GameObject characterPrefab = null;
        foreach (var prefab in characterPrefabs)
        {
            if (prefab.name == characterName)
            {
                characterPrefab = prefab;
                break;
            }
        }

        if (characterPrefab != null)
        {
            Instantiate(characterPrefab, spawnPosition.position, spawnPosition.rotation);
        }
        else
        {
            Debug.LogError("선택된 캐릭터 프리팹을 찾을 수 없습니다.");
        }
    }
}
