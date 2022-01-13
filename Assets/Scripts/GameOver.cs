using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOver;
    public Text content;
    //public GameObject player;

    public GameObject GameUI;

    public Image bg;

    public Button restart;
    public Button quit;

    private void Start()
    {
        restart.onClick.AddListener(Restart);
        quit.onClick.AddListener(Quit);
    }

    public void displayGameOver(int condition) {
        GameObject player = GameObject.Find("Player");
        Movement playerScript = player.GetComponent<Movement>();
        gameOver.SetActive(true);
        playerScript.uiOn = true;
        GameUI.SetActive(false);
        //player.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        
        if(condition == 1 || condition ==2){
            content.text = condition == 1 ? "YOU DIED!" : "YOU STARVED TO DEATH!";
            bg.color = new Color32(255,0,0,100);
        }
        else{
            content.text = "YOU'VE ESCAPED THE ISLAND";
            bg.color = new Color32(255,255,255,100);
        }
    }

    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
