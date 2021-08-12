using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    InputActionAsset playerControls;
    InputAction movement;
    InputAction charge;
    InputAction escape;

    public Rigidbody rb;

    [System.NonSerialized]
    public Vector3 direction;
    Vector3 lastDirection;
    public float speed;

    [System.NonSerialized]
    public float durationHeld;
    bool isHeld = false;
    public float multiplier;

    private int score = 0;
    private int health = 5;

    public Text scoreText;
    public Text healthText;

    public Image winLoseBG;
    public Text winLoseText;

    private void Start()
    {
        var gameplayActionMap = playerControls.FindActionMap("Player");

        movement = gameplayActionMap.FindAction("Move");
        charge = gameplayActionMap.FindAction("Charge");
        escape = gameplayActionMap.FindAction("Exit");

        movement.performed += OnMovementChanged;
        movement.canceled += OnMovementChanged;
        movement.Enable();

        charge.performed += chargeStart => { isHeld = true; };
        charge.canceled += chargeRelease;
        charge.Enable();

        escape.performed += toMain => { SceneManager.LoadScene(0); };
        escape.Enable();
    }

    private void Update()
    {
        if (health <= 0)
        {
            winLoseText.text = "Game Over!";
            winLoseText.color = Color.white;
            winLoseBG.color = Color.red;
            winLoseBG.gameObject.SetActive(true);
            StartCoroutine(LoadScene(3));
        }
    }

    void FixedUpdate()
    {
        if (!isHeld)
            rb.AddForce(direction * speed);
        else
            durationHeld += 10;
    }

    private void OnMovementChanged(InputAction.CallbackContext context)
    {
        Vector2 inputDirection = context.ReadValue<Vector2>();
        inputDirection = Vector2.ClampMagnitude(inputDirection, 1f);

        direction = new Vector3(inputDirection.x, 0f, inputDirection.y);
        if (direction.magnitude > 0)
            lastDirection = direction;
    }

    private void chargeRelease(InputAction.CallbackContext context)
    {
        

        rb.AddForce(lastDirection * durationHeld * multiplier);
        durationHeld = 0;
        isHeld = false;
    }

    void SetScoreText()
    {
        scoreText.text = $"Score: {score}";
    }

    void SetHealthText()
    {
        healthText.text = $"Health: {health}";
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            score += 1;
            SetScoreText();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Trap"))
        {
            health -= 1;
            SetHealthText();
        }
        else if (other.CompareTag("Goal"))
        {
            winLoseText.text = "You Win!";
            winLoseBG.color = Color.green;
            winLoseBG.gameObject.SetActive(true);
            StartCoroutine(LoadScene(3));
        }
    }

    IEnumerator LoadScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
