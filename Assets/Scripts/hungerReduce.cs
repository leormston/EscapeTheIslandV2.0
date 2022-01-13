using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hungerReduce : MonoBehaviour
{   
    //hunger
    public int hunger = 100;
    public int counter;

    public int maxHunger = 100;
    public HungerBar hungerBar;

    //the hunger text
    public Text hungerText;

    //hunger event
    public DialogueObject event3;
    //public DialogueObject event9;

    void Start(){
        hunger = maxHunger;
        hungerBar.SetMaxHunger(hunger);
    }

    public void hungerIncrease() {
        //test if hunger is not 100 yet
        if (hunger <= 100) {
            //if the hunger is over 100 set it to 100
            if (hunger + 5 >= 100)
            {
                hunger = 100;
            }
            //else +5 hunger
            else {
                hunger += 5;
                Debug.Log("hunger increased");
            }
            Debug.Log("hunger increased is " + hunger);
            hungerText.text = "Hunger: " + hunger.ToString();
            hungerBar.SetHunger(hunger);
            counter = 0;
        }
        
    }

    public void hungerDecrease() {
        Debug.Log("the hunger before is " + hunger);
        hunger--;
        hungerText.text = "Hunger: " + hunger.ToString();
        Debug.Log("the hunger is " + hunger);
        hungerBar.SetHunger(hunger);
    }

    // Update is called once per frame
    void Update()
    {
        //counter reduce hunger every 3 second
        GameObject player = GameObject.Find("Player");
        Movement playerScript = player.GetComponent<Movement>();
        //not reducing hunger when ui is on
        if (playerScript.uiOn == false) {
            counter++;
            if (counter == 3000)
            {
                hungerDecrease();
                counter = 0;
            }
        }
        if (hunger == 0) {
            GameObject gameOver = GameObject.Find("GameManager");
            GameOver gameOverScript = gameOver.GetComponent<GameOver>();
            gameOverScript.displayGameOver(2);
        }

        if (hunger == 50) {
            GameObject gameManager = GameObject.Find("GameManager");
            DialogueSystem dialogueScript = gameManager.GetComponent<DialogueSystem>();
            dialogueScript.startDialogue(event3);
        }


    }
}
