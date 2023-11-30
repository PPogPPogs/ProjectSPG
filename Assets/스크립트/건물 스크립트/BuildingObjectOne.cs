using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingObjectOne : MonoBehaviour
{
    public GameObject Cannon0Prefab;
    public GameObject Cannon1Prefab;
    public GameObject Godon0Prefab;
    public GameObject Godon1Prefab;
    public GameObject LionPrefab;
    public GameObject DooPrefab;
    public GameObject PandaPrefab;
    public GameObject JustinBuild0;
    public GameObject JustinBuild1;

    private bool justinOneGo = false;

    void Start()
    {
        
        justinOneGo = PlayerPrefs.GetInt("JustinOneGo", 0) == 1;
        
        float Cannon0X = PlayerPrefs.GetFloat($"{2.5}XCannon0", 0f); // X 좌표를 불러옵니다.
        float Cannon0Y = PlayerPrefs.GetFloat($"{2.5}YCannon0", 0f); // Y 좌표를 불러옵니다.
        float Cannon0Z = PlayerPrefs.GetFloat($"{2.5}ZCannon0", 0f); // Z 좌표를 불러옵니다.

        // 좌표가 저장되어 있는지 확인
        if (Cannon0X != 0f || Cannon0Y != 0f || Cannon0Z != 0f)
        {
            // 프리팹을 인스턴스화하고 좌표 설정
            GameObject Cannon0Object = Instantiate(Cannon0Prefab, new Vector3(Cannon0X, Cannon0Y, Cannon0Z), Quaternion.identity);

        }

        float Cannon1X = PlayerPrefs.GetFloat($"{2.5}XCannon1", 0f); // X 좌표를 불러옵니다.
        float Cannon1Y = PlayerPrefs.GetFloat($"{2.5}YCannon1", 0f); // Y 좌표를 불러옵니다.
        float Cannon1Z = PlayerPrefs.GetFloat($"{2.5}ZCannon1", 0f); // Z 좌표를 불러옵니다.

        // 좌표가 저장되어 있는지 확인
        if (Cannon1X != 0f || Cannon1Y != 0f || Cannon1Z != 0f)
        {
            // 프리팹을 인스턴스화하고 좌표 설정
            GameObject Cannon1Object = Instantiate(Cannon1Prefab, new Vector3(Cannon1X, Cannon1Y, Cannon1Z), Quaternion.identity);
        }
        //Connon 끝

        int justinBuild0Active = PlayerPrefs.GetInt("JustinBuild0Active", 1); // 기본값은 활성화 (1)
        JustinBuild0.SetActive(justinBuild0Active == 1);

        int justinBuild1Active = PlayerPrefs.GetInt("JustinBuild1Active", 0); // 기본값은 활성화 (0)
        JustinBuild1.SetActive(justinBuild1Active == 1);

        //Justin 끝

        float Godon0X = PlayerPrefs.GetFloat($"{2.5}XGodonBuild0", 0f); // X 좌표를 불러옵니다.
        float Godon0Y = PlayerPrefs.GetFloat($"{2.5}YGodonBuild0", 0f); // Y 좌표를 불러옵니다.
        float Godon0Z = PlayerPrefs.GetFloat($"{2.5}ZGodonBuild0", 0f); // Z 좌표를 불러옵니다.

        // 좌표가 저장되어 있는지 확인
        if (Godon0X != 0f || Godon0Y != 0f || Godon0Z != 0f)
        {
            // 프리팹을 인스턴스화하고 좌표 설정
            GameObject Godon0Object = Instantiate(Godon0Prefab, new Vector3(Godon0X, Godon0Y, Godon0Z), Quaternion.identity);
        }

        float Godon1X = PlayerPrefs.GetFloat($"{2.5}XGodonBuild1", 0f); // X 좌표를 불러옵니다.
        float Godon1Y = PlayerPrefs.GetFloat($"{2.5}YGodonBuild1", 0f); // Y 좌표를 불러옵니다.
        float Godon1Z = PlayerPrefs.GetFloat($"{2.5}ZGodonBuild1", 0f); // Z 좌표를 불러옵니다.

        // 좌표가 저장되어 있는지 확인
        if (Godon1X != 0f || Godon1Y != 0f || Godon1Z != 0f)
        {
            // 프리팹을 인스턴스화하고 좌표 설정
            GameObject Godon1Object = Instantiate(Godon1Prefab, new Vector3(Godon1X, Godon1Y, Godon1Z), Quaternion.identity);
        }
        //Godon 끝

        float LionX = PlayerPrefs.GetFloat($"{2.5}XLionBuilding", 0f); // X 좌표를 불러옵니다.
        float LionY = PlayerPrefs.GetFloat($"{2.5}YLionBuilding", 0f); // Y 좌표를 불러옵니다.
        float LionZ = PlayerPrefs.GetFloat($"{2.5}ZLionBuilding", 0f); // Z 좌표를 불러옵니다.

        // 좌표가 저장되어 있는지 확인
        if (LionX != 0f || LionY != 0f || LionZ != 0f)
        {
            // 프리팹을 인스턴스화하고 좌표 설정
            GameObject LionObject = Instantiate(LionPrefab, new Vector3(LionX, LionY, LionZ), Quaternion.identity);
        }
        // Lion 끝

        float DooX = PlayerPrefs.GetFloat($"{2.5}XDooBuilding", 0f); // X 좌표를 불러옵니다.
        float DooY = PlayerPrefs.GetFloat($"{2.5}YDooBuilding", 0f); // Y 좌표를 불러옵니다.
        float DooZ = PlayerPrefs.GetFloat($"{2.5}ZDooBuilding", 0f); // Z 좌표를 불러옵니다.

        // 좌표가 저장되어 있는지 확인
        if (DooX != 0f || DooY != 0f || DooZ != 0f)
        {
            // 프리팹을 인스턴스화하고 좌표 설정
            GameObject DooObject = Instantiate(DooPrefab, new Vector3(DooX, DooY, DooZ), Quaternion.identity);
        }
        // Doo 끝

        float PandaX = PlayerPrefs.GetFloat($"{2.5}XPandaBuilding", 0f); // X 좌표를 불러옵니다.
        float PandaY = PlayerPrefs.GetFloat($"{2.5}YPandaBuilding", 0f); // Y 좌표를 불러옵니다.
        float PandaZ = PlayerPrefs.GetFloat($"{2.5}ZPandaBuilding", 0f); // Z 좌표를 불러옵니다.

        // 좌표가 저장되어 있는지 확인
        if (PandaX != 0f || PandaY != 0f || PandaZ != 0f)
        {
            // 프리팹을 인스턴스화하고 좌표 설정
            GameObject PandaObject = Instantiate(PandaPrefab, new Vector3(PandaX, PandaY, PandaZ), Quaternion.identity);
        }
        // Panda 끝

       

    }

    // Update is called once per frame
    void Update()
    {

    }
}
