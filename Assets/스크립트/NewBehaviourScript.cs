using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public void OnDeleteAllDataButton()
    {
        // PlayerPrefs에 저장된 모든 데이터 삭제
        PlayerPrefs.DeleteAll();
        Debug.Log("삭제완료");
    }
}
