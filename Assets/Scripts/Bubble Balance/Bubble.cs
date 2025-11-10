
using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bubble : MonoBehaviour
{
    public static Bubble Instance  {get; private set;}
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float bounceStrength = 3;
    private Rigidbody2D rbBubble;

    [Header("VoiceControl")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioLoudnessDetection audioLoudnessDetection;

    [SerializeField] private float loudnessThershold = 1f;
    [SerializeField] private float maxVoiceBoost = 2f;

    [SerializeField] private LogicManager logicManager;
    private bool bubbleAlive = true;
    public event EventHandler OnMicJump;


    private float loudnessMultiplier = 100f;
    private float loudnessDivisor = 5000f;

    private void Awake()
    {

        Instance = this;
        rbBubble = GetComponent<Rigidbody2D>();
        rbBubble.gravityScale = 0;

        Time.timeScale = 1f;
    }
    private void Start()
    {
        gameInput.OnJump += GameInput_OnJump;
    }

    private void OnDisable()
    {
        gameInput.OnJump -= GameInput_OnJump;      
    }

    private void GameInput_OnJump(object sender, System.EventArgs e)
    {
        if (bubbleAlive)
        {
             Jump();
        }
        else
        {
            
            RestartGame();
        }

    }
    private void FixedUpdate()
    {
        if (bubbleAlive != true) return;
        float loudness = loudnessMultiplier * audioLoudnessDetection.GetLoudnessFromMicrophone();
        Debug.Log(loudness);
        if (loudness >= loudnessThershold)
        {

            float boost = Mathf.Clamp(loudness / loudnessDivisor * maxVoiceBoost, 0f, maxVoiceBoost);
            OnMicJump?.Invoke(this, EventArgs.Empty);
            Jump(boost);

        }

    }

    private void Jump(float intensity = 0)
    {
        if (bubbleAlive!=true) return;
        float jumpForce = bounceStrength + intensity;
        float maxStrength = 5;
        if (jumpForce < maxStrength)
        {
            rbBubble.linearVelocity = new Vector2(rbBubble.linearVelocity.x, bounceStrength + intensity);
            rbBubble.gravityScale = 0.35f;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if we hit an object tagged as "Obstacle"
        logicManager.GameOver();
        bubbleAlive = false;
    }

    private void RestartGame()
    {
        // Un-freeze the game
        Time.timeScale = 1f;

        // Reload the currently active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
