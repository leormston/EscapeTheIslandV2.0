using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class checkInSight : MonoBehaviour
{
    RaycastHit hit;

    public GameObject wood;
    public AudioSource woodAudio;
    public AudioSource stoneAudio;
    public AudioSource berryAudio;
    public AudioSource treeAudio;
    public AudioSource scrapAudio;
    private void Start()
    {
        var aSources = GetComponents<AudioSource>();
        woodAudio = aSources[1];
        stoneAudio = aSources[2]; 
        berryAudio = aSources[3];
        treeAudio = aSources[4];
        scrapAudio = aSources[5];
    }

    private void Update()
    {

        //get player controller for the uion flag
        GameObject PlayerController = GameObject.Find("Player");
        Movement playerScript = PlayerController.GetComponent<Movement>();

        //call resource
        GameObject gameManager = GameObject.Find("GameManager");
        ResourceCounter resourceScript = gameManager.GetComponent<ResourceCounter>();

        // call hunger
        //GameObject hungerDisplay = GameObject.Find("HungerDisplay");
        hungerReduce hungerScript = gameManager.GetComponent<hungerReduce>();

        //call equipItem
        EquipItem equipScript = gameManager.GetComponent<EquipItem>();

        //a foward vector 
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        if (playerScript.uiOn == false)
        {
            if (Physics.Raycast(transform.position, fwd, out hit, 4))
            {
                //Debug.Log("hit object" + hit.collider.name);

                if (Keyboard.current.eKey.wasPressedThisFrame)
                {
                    if (hit.collider.name.Contains("berries"))
                    {
                        Destroy(hit.transform.gameObject);
                        berryAudio.Play();
                        //hunger increase
                        hungerScript.hungerIncrease();
                    }
                    else if (hit.collider.name.Contains("wood"))
                    {
                        Destroy(hit.transform.gameObject);
                        woodAudio.Play();
                        resourceScript.wood += 1;
                    }
                    else if (hit.collider.name.Contains("stone"))
                    {
                        Destroy(hit.transform.gameObject);
                        stoneAudio.Play();
                        resourceScript.stone += 1;
                    }
                    else if (hit.collider.name.Contains("scrap"))
                    {
                        Destroy(hit.transform.gameObject);
                        scrapAudio.Play();
                        resourceScript.scrap += 1;
                    }
                    else if (hit.collider.name.Contains("Ship"))
                    {
                        MissionBoard missionScript = gameManager.GetComponent<MissionBoard>();
                        if (missionScript.escape)
                        {
                            GameOver gameOverScript = gameManager.GetComponent<GameOver>();
                            gameOverScript.displayGameOver(3);
                        }
                    }

                    else if (hit.collider.name.Contains("BanyanTree") && equipScript.axeOn)
                    {
                        Vector3 pos = new Vector3(hit.transform.gameObject.transform.position.x, hit.transform.gameObject.transform.position.y + 20.0f,hit.transform.gameObject.transform.position.z);
                        GameObject.Instantiate(wood, pos, hit.transform.gameObject.transform.rotation);
                        treeAudio.Play();
                        Destroy(hit.transform.gameObject);
                    }

                }

            }
        }

    }
}
