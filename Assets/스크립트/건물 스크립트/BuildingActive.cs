using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingActive : MonoBehaviour
{
    public GameObject Cannon10;
    public GameObject Cannon11;
    public GameObject JustinBuild0;
    public GameObject JustinBuild1;
    public GameObject GodonBuild10;
    public GameObject GodonBuild11;
    // Start is called before the first frame update
    void Start()
    {
        int cannon10Active = PlayerPrefs.GetInt("Cannon10Active", 0); // 기본값은 활성화 (0)
        Cannon10.SetActive(cannon10Active == 1);

        int cannon11Active = PlayerPrefs.GetInt("Cannon11Active", 0); // 기본값은 활성화 (0)
        Cannon11.SetActive(cannon11Active == 1);
        // Cannon 활성화

        int justinBuild0Active = PlayerPrefs.GetInt("JustinBuild0Active", 1); // 기본값은 활성화 (1)
        JustinBuild0.SetActive(justinBuild0Active == 1);

        int justinBuild1Active = PlayerPrefs.GetInt("JustinBuild1Active", 0); // 기본값은 활성화 (0)
        JustinBuild1.SetActive(justinBuild1Active == 1);
        // JustinBuild 활성화

        int godonBuild10Active = PlayerPrefs.GetInt("GodonBuild10Active", 0); // 기본값은 활성화 (0)
        GodonBuild10.SetActive(godonBuild10Active == 1);

        int godonBuild11Active = PlayerPrefs.GetInt("GodonBuild11Active", 0); // 기본값은 활성화 (0)
        GodonBuild11.SetActive(godonBuild11Active == 1);
        



    }

    // Update is called once per frame
    void Update()
    {

    }
}
