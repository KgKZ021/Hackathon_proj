using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{

    [SerializeField] private List<GameObject> cloud;
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private float heightOffset = 1f;
    [SerializeField] private GameInput gameInput;


    private float timer = 0;

    private bool waitingForJump = true;

    void Start()
    {
        gameInput.OnJump += GameInput_OnFirstJump;
        Bubble.Instance.OnMicJump += Bubble_OnMicJump;
    }
    void Update()
    {
        if (waitingForJump)
        {
            return;
        }
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            SpawnCloud();
            timer = 0;
        }

    }
    private void GameInput_OnFirstJump(object sender, System.EventArgs e)
    {
        FirstJumpSpawn();
    }

    private void Bubble_OnMicJump(object sender, System.EventArgs e)
    {
        FirstJumpSpawn();
    }

    private void SpawnCloud()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;

        int random = Random.Range(0, cloud.Count);

        GameObject spawnCloud = cloud[random];
        Transform spawnX_Prefab_Transform = spawnCloud.transform.Find("SpawnX");

        if (spawnX_Prefab_Transform != null)
        {
            // 3. Get its *local* position (its offset from the prefab's root)
            Vector3 spawnX_LocalOffset = spawnX_Prefab_Transform.localPosition;


        }

        // 4. Spawn the cloud normally
        Vector3 spawnPosition = new Vector3(spawnX_Prefab_Transform.position.x, Random.Range(lowestPoint, highestPoint), 0);
        Instantiate(spawnCloud, spawnPosition, transform.rotation);
    }

    private void FirstJumpSpawn()
    {
        // 1. We are no longer waiting
        waitingForJump = false;

        // 2. Spawn the first cloud immediately
        SpawnCloud();

        // 3. Unsubscribe. We don't need to listen for this event anymore.
        gameInput.OnJump -= GameInput_OnFirstJump;
    }
}
