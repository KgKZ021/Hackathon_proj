using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float destroyMarginX = -45;

    private void FixedUpdate()
    {
        transform.position += (Vector3.left * moveSpeed) * Time.deltaTime;

        if(transform.position.x < destroyMarginX)
        {
            Destroy(gameObject);
        }
    }
}
