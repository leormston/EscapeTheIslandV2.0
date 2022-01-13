using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapOn : MonoBehaviour
{
    public GameObject map;

    private bool mapOn = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) {
            minimapOn();
        }
    }
    void minimapOn()
    {
        /*GameObject PlayerController = GameObject.Find("Player");
        Movement playerScript = PlayerController.GetComponent<Movement>();
        if (playerScript.uiOn == true)
        {
            map.SetActive(false);
            playerScript.uiOn = false;
        }
        else {
            map.SetActive(true);
            playerScript.uiOn = true;
        }*/
        if (mapOn)
        {
            mapOn = false;
            map.SetActive(false);
        }
        else {
            mapOn = true;
            map.SetActive(true);
        }

    }    

}
