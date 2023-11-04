using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingObjectOne : MonoBehaviour
{
    public GameObject CannonPrefab;
    public GameObject GodonPrefab;
    public GameObject LionPrefab;
    public GameObject DooPrefab;
    public GameObject PandaPrefab;

    void Start()
    {
        PlayerPrefs.DeleteAll();
        float CannonX = PlayerPrefs.GetFloat($"{2.5}XCannon", 0f); // X ��ǥ�� �ҷ��ɴϴ�.
        float CannonY = PlayerPrefs.GetFloat($"{2.5}YCannon", 0f); // Y ��ǥ�� �ҷ��ɴϴ�.
        float CannonZ = PlayerPrefs.GetFloat($"{2.5}ZCannon", 0f); // Z ��ǥ�� �ҷ��ɴϴ�.

        // ��ǥ�� ����Ǿ� �ִ��� Ȯ��
        if (CannonX != 0f || CannonY != 0f || CannonZ != 0f)
        {
            // �������� �ν��Ͻ�ȭ�ϰ� ��ǥ ����
            GameObject CannonObject = Instantiate(CannonPrefab, new Vector3(CannonX, CannonY, CannonZ), Quaternion.identity);
        }
        //Connon ��

        float GodonX = PlayerPrefs.GetFloat($"{2.5}XGodonBuilding", 0f); // X ��ǥ�� �ҷ��ɴϴ�.
        float GodonY = PlayerPrefs.GetFloat($"{2.5}YGodonBuilding", 0f); // Y ��ǥ�� �ҷ��ɴϴ�.
        float GodonZ = PlayerPrefs.GetFloat($"{2.5}ZGodonBuilding", 0f); // Z ��ǥ�� �ҷ��ɴϴ�.

        // ��ǥ�� ����Ǿ� �ִ��� Ȯ��
        if (GodonX != 0f || GodonY != 0f || GodonZ != 0f)
        {
            // �������� �ν��Ͻ�ȭ�ϰ� ��ǥ ����
            GameObject GodonObject = Instantiate(GodonPrefab, new Vector3(GodonX, GodonY, GodonZ), Quaternion.identity);
        }
        //Godon ��

        float LionX = PlayerPrefs.GetFloat($"{2.5}XLionBuilding", 0f); // X ��ǥ�� �ҷ��ɴϴ�.
        float LionY = PlayerPrefs.GetFloat($"{2.5}YLionBuilding", 0f); // Y ��ǥ�� �ҷ��ɴϴ�.
        float LionZ = PlayerPrefs.GetFloat($"{2.5}ZLionBuilding", 0f); // Z ��ǥ�� �ҷ��ɴϴ�.

        // ��ǥ�� ����Ǿ� �ִ��� Ȯ��
        if (LionX != 0f || LionY != 0f || LionZ != 0f)
        {
            // �������� �ν��Ͻ�ȭ�ϰ� ��ǥ ����
            GameObject LionObject = Instantiate(LionPrefab, new Vector3(LionX, LionY, LionZ), Quaternion.identity);
        }
        // Lion ��

        float DooX = PlayerPrefs.GetFloat($"{2.5}XDooBuilding", 0f); // X ��ǥ�� �ҷ��ɴϴ�.
        float DooY = PlayerPrefs.GetFloat($"{2.5}YDooBuilding", 0f); // Y ��ǥ�� �ҷ��ɴϴ�.
        float DooZ = PlayerPrefs.GetFloat($"{2.5}ZDooBuilding", 0f); // Z ��ǥ�� �ҷ��ɴϴ�.

        // ��ǥ�� ����Ǿ� �ִ��� Ȯ��
        if (DooX != 0f || DooY != 0f || DooZ != 0f)
        {
            // �������� �ν��Ͻ�ȭ�ϰ� ��ǥ ����
            GameObject DooObject = Instantiate(DooPrefab, new Vector3(DooX, DooY, DooZ), Quaternion.identity);
        }
        // Doo ��

        float PandaX = PlayerPrefs.GetFloat($"{2.5}XPandaBuilding", 0f); // X ��ǥ�� �ҷ��ɴϴ�.
        float PandaY = PlayerPrefs.GetFloat($"{2.5}YPandaBuilding", 0f); // Y ��ǥ�� �ҷ��ɴϴ�.
        float PandaZ = PlayerPrefs.GetFloat($"{2.5}ZPandaBuilding", 0f); // Z ��ǥ�� �ҷ��ɴϴ�.

        // ��ǥ�� ����Ǿ� �ִ��� Ȯ��
        if (PandaX != 0f || PandaY != 0f || PandaZ != 0f)
        {
            // �������� �ν��Ͻ�ȭ�ϰ� ��ǥ ����
            GameObject PandaObject = Instantiate(PandaPrefab, new Vector3(PandaX, PandaY, PandaZ), Quaternion.identity);
        }
        // Panda ��

       

    }

    // Update is called once per frame
    void Update()
    {

    }
}
