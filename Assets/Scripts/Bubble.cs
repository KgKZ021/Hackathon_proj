
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float bounceStrength = 1;
    private Rigidbody2D rbBubble;

    private void Awake()
    {
        rbBubble = GetComponent<Rigidbody2D>();
        rbBubble.gravityScale = 0;
    }
    private void Start()
    {
        gameInput.OnJump += GameInput_OnJump;
    }

    private void GameInput_OnJump(object sender, System.EventArgs e)
    {
        rbBubble.linearVelocity = new Vector2( rbBubble.linearVelocity.x,bounceStrength);
        rbBubble.gravityScale =(float) 0.35;

    }
}
