using UnityEngine;
using UnityEngine.UI;

public class MyCharacterController : MonoBehaviour
{
    public PlayerData player;
    public Text nicknameText;
    public Text levelText;

    private void Start()
    {
        if (player == null)
            player = new PlayerData("DefaultName", 1);

        UpdateCharacterUI();
    }

    public void SetPlayerInfo(PlayerData newPlayer)
    {
        player = newPlayer;
        UpdateCharacterUI();
    }

    public void UpdateCharacterUI()
    {
        if (nicknameText != null)
            nicknameText.text = player.nickname;

        if (levelText != null)
            levelText.text = "LV:"+ player.level;
    }

    public void LevelUp()
    {
        player.LevelUp();
        UpdateCharacterUI();
    }

    // PlayScene������ UI Text ��Ҹ� Ȱ��ȭ�� �� ����� �Լ�
    public void ShowUIElements()
    {
        if (nicknameText != null)
            nicknameText.gameObject.SetActive(true);

        if (levelText != null)
            levelText.gameObject.SetActive(true);
    }

    // PlayScene������ UI Text ��Ҹ� ��Ȱ��ȭ�� �� ����� �Լ�
    public void HideUIElements()
    {
        if (nicknameText != null)
            nicknameText.gameObject.SetActive(false);

        if (levelText != null)
            levelText.gameObject.SetActive(false);
    }
}
