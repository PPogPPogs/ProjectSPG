using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingActive : MonoBehaviour
{
   
    public GameObject JustinBuild0;
    public GameObject JustinBuild1;
  
    // Start is called before the first frame update
    void Start()
    {
        

        int justinBuild0Active = PlayerPrefs.GetInt("JustinBuild0Active", 1); // �⺻���� Ȱ��ȭ (1)
        JustinBuild0.SetActive(justinBuild0Active == 1);

        int justinBuild1Active = PlayerPrefs.GetInt("JustinBuild1Active", 0); // �⺻���� Ȱ��ȭ (0)
        JustinBuild1.SetActive(justinBuild1Active == 1);
        // JustinBuild Ȱ��ȭ

       



    }

    // Update is called once per frame
    void Update()
    {

    }
}
