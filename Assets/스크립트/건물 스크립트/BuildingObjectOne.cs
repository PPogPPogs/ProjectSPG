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
    public GameObject JustinBuildingOnePrefab;
    public GameObject JustinBuildingPrefab;
    private bool justinOneGo = false;

    void Start()
    {
        
        justinOneGo = PlayerPrefs.GetInt("JustinOneGo", 0) == 1;
        
        float CannonX = PlayerPrefs.GetFloat($"{2.5}XCannon", 0f); // X 좌표를 불러옵니다.
        float CannonY = PlayerPrefs.GetFloat($"{2.5}YCannon", 0f); // Y 좌표를 불러옵니다.
        float CannonZ = PlayerPrefs.GetFloat($"{2.5}ZCannon", 0f); // Z 좌표를 불러옵니다.

        // 좌표가 저장되어 있는지 확인
        if (CannonX != 0f || CannonY != 0f || CannonZ != 0f)
        {
            // 프리팹을 인스턴스화하고 좌표 설정
            GameObject CannonObject = Instantiate(CannonPrefab, new Vector3(CannonX, CannonY, CannonZ), Quaternion.identity);
        }
        //Connon 끝

        float JustinX = PlayerPrefs.GetFloat("XJustinBuilding", 0f); // X 좌표를 불러옵니다.
        float JustinY = PlayerPrefs.GetFloat("YJustinBuilding", 0f); // Y 좌표를 불러옵니다.
        float JustinZ = PlayerPrefs.GetFloat("ZJustinBuilding", 0f); // Z 좌표를 불러옵니다.

        // 좌표가 저장되어 있는지 확인
        if (JustinX != 0f || JustinY != 0f || JustinZ != 0f)
        {
            GameObject JustinOneObject = Instantiate(JustinBuildingOnePrefab, new Vector3(JustinX, JustinY, JustinZ), Quaternion.identity);
            Debug.Log("불러옴");
        }
        else
        {
            // 저장된 좌표가 없을 때 특정 좌표로 프리팹을 생성
            Vector3 defaultPosition = new Vector3(-16f, -0.6f, 0f);
            GameObject JustinOneObject = Instantiate(JustinBuildingPrefab, defaultPosition, Quaternion.identity);
            Debug.Log("저장된 좌표가 없어 특정 좌표로 생성됨");
        }
        //Justin 끝
        float GodonX = PlayerPrefs.GetFloat($"{2.5}XGodonBuilding", 0f); // X 좌표를 불러옵니다.
        float GodonY = PlayerPrefs.GetFloat($"{2.5}YGodonBuilding", 0f); // Y 좌표를 불러옵니다.
        float GodonZ = PlayerPrefs.GetFloat($"{2.5}ZGodonBuilding", 0f); // Z 좌표를 불러옵니다.

        // 좌표가 저장되어 있는지 확인
        if (GodonX != 0f || GodonY != 0f || GodonZ != 0f)
        {
            // 프리팹을 인스턴스화하고 좌표 설정
            GameObject GodonObject = Instantiate(GodonPrefab, new Vector3(GodonX, GodonY, GodonZ), Quaternion.identity);
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
