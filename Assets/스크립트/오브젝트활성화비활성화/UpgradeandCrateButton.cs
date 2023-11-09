using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeandCrateButton : MonoBehaviour
{
    public GameObject UpgradeButton;
    public GameObject CrateButton;
    public GameObject Image;
    
    // Start is called before the first frame update
    public void OnClick()
    {
        UpgradeButton.SetActive(false);
        CrateButton.SetActive(false);
        Image.SetActive(true);
    }
}
