using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class InputManager : MonoBehaviour
{
    [SerializeField] Movement movement;
    [SerializeField] MouseLook mouseLook;
    PlayerControls controls;
    PlayerControls.GroundMovementActions groundMovement;
    [SerializeField] float health;
    Vector2 horizontalInput; 
    Vector2 mouseInput;
    public GameObject projectile;
    public Text healthText;
    public float maxHealth = 150;
    public HealthBar healthBar;
    public AudioSource hurtAudio; 
    void start(){
        health = maxHealth;
        healthBar.SetMaxHealth(health);
    }
    
    private void Awake()
    {
        controls = new PlayerControls();
        groundMovement = controls.GroundMovement;
        groundMovement.HorizontalMovement.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>();
        groundMovement.Jump.performed += _ => movement.OnJumpPressed();

        groundMovement.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
        groundMovement.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>();
        hurtAudio = gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        movement.RecieveInput(horizontalInput);
        mouseLook.RecieveInput(mouseInput);
        GameObject gameManager = GameObject.Find("GameManager");
        EquipItem equipScript = gameManager.GetComponent<EquipItem>();
        //get uiOn
        GameObject player = GameObject.Find("Player");
        Movement playerScript = player.GetComponent<Movement>();
        //get equipItem to see if slinger is on
        
        if (equipScript.slingerOn   == true) {
            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                Vector3 pos = new Vector3(transform.position.x + 0.5f, transform.position.y + 0.5f, transform.position.z);
                Rigidbody rb = Instantiate(projectile, pos, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
                rb.AddForce(transform.up * 2.2f, ForceMode.Impulse);
            }
         }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        hurtAudio.Play();
        healthText.text = "Health: "+ health.ToString();
        
        healthBar.SetHealth(health);

        //if (health <= 0) Invoke(nameof(DestroyPlayer), 0.5f);
        if (health <= 0) {
            GameObject gameOver = GameObject.Find("GameManager");
            GameOver gameOverScript = gameOver.GetComponent<GameOver>();
            gameOverScript.displayGameOver(1);
        }
    }

    private void DestroyPlayer()
    {
        Destroy(gameObject);
    }
    public void OnTriggerEnter(Collider col)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if(col.gameObject.name == "EnemyCube(Clone)")
        {
            TakeDamage(5);
            Destroy(col.gameObject);
        }
        
        
     
    }
    private void OnEnable ()
    {
        controls.Enable();
    }

    private void OnDestroy()
    {
        controls.Disable();

    }

}
