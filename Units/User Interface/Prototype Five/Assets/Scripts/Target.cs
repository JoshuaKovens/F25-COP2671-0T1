using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager;
    private float minSpeed = 12;
    private float maxSpeed = 18;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -2;
   
    public ParticleSystem explosionParticle; //Particle effect to play on target destruction
    public int pointValue; //Points awarded for hitting this target

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetRb = GetComponent<Rigidbody>(); 
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); //Finds the GameManager script

        targetRb.AddForce(RandomForce(), ForceMode.Impulse); 
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        transform.position = RandomSpawnPos();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown() //Destroys the target when clicked
    {
        if (gameManager.isGameActive) //Prevents interaction when the game is over
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation); //Plays the particle effect
            gameManager.UpdateScore(pointValue); //Adds to the score when a target is destroyed
        }
    }
    
    private void OnTriggerEnter(Collider other) //Destroys the target when it falls below a certain point
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.GameOver(); //Ends the game if a not bad target is missed
        }

    }


    Vector3 RandomForce() //Adds a random upward force to the target
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque() //Adds a random torque to the target
    {
        return Random.Range(-maxTorque, maxTorque);
    }
    
    Vector3 RandomSpawnPos() //Spawns the target at a random x position within a defined range
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
}
