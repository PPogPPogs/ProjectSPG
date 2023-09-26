using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPortalAfter : MonoBehaviour
{

    private Animator portalAnimator;
    private bool isMonster = true;
    


    void Start()
    {
        portalAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CalendarManager calendarManager = FindObjectOfType<CalendarManager>();
        int currentHour = calendarManager.GetHour();

        if (calendarManager != null)
        {
            if (currentHour >= 7 && currentHour <= 18)
            {
                portalAnimator.SetBool("IsOpen", false);
            }
            else
            {
                if (isMonster)
                {
                    portalAnimator.SetBool("IsOpen", true);
                }
                else
                {
                    portalAnimator.SetBool("IsOpen", false);
                }

            }
        }
    }
}
