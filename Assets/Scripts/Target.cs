using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] int value;
    [SerializeField] ParticleSystem explosionParticle;

    private SpawnManager spawnManager;
    private Rigidbody targetRB;
    private float torqueForce = 10;
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float xRange = 5;
    private float yPosition = -1;

    void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        targetRB = GetComponent<Rigidbody>();
        targetRB.AddForce(RandomForce(), ForceMode.Impulse);
        targetRB.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPosition();
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque()
    {
        return Random.Range(-torqueForce, torqueForce);
    }

    Vector3 RandomSpawnPosition()
    {
        return new Vector3(Random.Range(-xRange, xRange), yPosition);
    }

    private void OnMouseDown()
    {
        Destroy(gameObject);
        spawnManager.UpdateScore(value);
        Instantiate(explosionParticle,transform.position,explosionParticle.transform.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad"))
        {
            spawnManager.GameOver();
        }
    }
}
