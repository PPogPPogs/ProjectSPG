[System.Serializable]
public class PlayerData
{
    public string nickname;
    public int level;

    public PlayerData(string nickname, int level)
    {
        this.nickname = nickname;
        this.level = level;
    }

    // ���� �� �Լ�
    public void LevelUp()
    {
        level++;
    }
}
