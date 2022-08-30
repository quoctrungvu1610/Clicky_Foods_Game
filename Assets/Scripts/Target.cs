using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    Rigidbody targetRb;
    private float minSpeed = 5;
    private float maxSpeed = 16;

    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -1;

    private GameManager gameManager;

    public ParticleSystem explosionParticle;

    public int pointValue;
    // Start is called before the first frame update
    void Start()
    {

       gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

       targetRb = gameObject.GetComponent<Rigidbody>();

       targetRb.AddForce(RandomForce(), ForceMode.Impulse);

       targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        gameObject.transform.position = RandomSpawnPos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }
    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }
    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), -ySpawnPos);
    }
    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, gameObject.transform.position, gameObject.transform.rotation);
            gameManager.UpdateScore(pointValue);
        } 
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.GameOver();
        }
    }
}
