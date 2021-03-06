using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DialogueSystem : MonoBehaviour
{
    public DialogueObject testDialogue;
    public GameObject dialogueWindow;
    public Text content;
    public Text name;
    public GameObject fairy;
    //a flag for pause not work while dialogue is on
    public bool diaOn = false;

    private Typewriter typewriter;

    // Start is called before the first frame update
    void Start()
    {
        typewriter = GetComponent<Typewriter>();
        startDialogue(testDialogue);
    }


    // Update is called once per frame

    public void startDialogue(DialogueObject dialogueObject) {
        GameObject PlayerController = GameObject.Find("Player");
        Movement playerScript = PlayerController.GetComponent<Movement>();
        // playerScript.uiOn = true;
        StartCoroutine(NextDialogue(dialogueObject));
        //show window
        dialogueWindow.SetActive(true);
    }

    private IEnumerator NextDialogue(DialogueObject dialogueobject) {
        diaOn = true;

        for (int i = 0; i < dialogueobject.Dialogue.Length; i++) {
            name.text = dialogueobject.Potrait[i];
            //show fairy art if it is said by fairy
            if (dialogueobject.Potrait[i] == "Fairy")
            {
                fairy.SetActive(true);
            }
            else {
                fairy.SetActive(false);
            }
            if (Input.GetKey(KeyCode.P))
            {
                Debug.Log("it is in this if");
                turnOffDialogue();
                yield break;
            }
            else {
                Debug.Log("if is still going");
            }

            //type writer effect for the current line
            yield return typewriter.Run(dialogueobject.Dialogue[i], content);
            //yield return new WaitUntil(() => Keyboard.current.enterKey.wasPressedThisFrame);
            yield return new WaitForSeconds(1);
            
        }
        turnOffDialogue();

        /*foreach (string dialogue in dialogueobject.Dialogue) {

            yield return typewriter.Run(dialogue, content);
            yield return new WaitUntil(() => Keyboard.current.enterKey.wasPressedThisFrame);
        }*/
    }

    private void turnOffDialogue() {
        dialogueWindow.SetActive(false);
        GameObject PlayerController = GameObject.Find("Player");
        Movement playerScript = PlayerController.GetComponent<Movement>();
        playerScript.uiOn = false;
        diaOn = false;
    }
}
