using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player; //represents the hero
    public GameObject[] spawnPoints; //represents all the spawn points
    public GameObject alien; //represents the alien menace
    public int maxAliensOnScreen; //how many enemies on screen
    public int totalAliens; //counts all enemies on screen
    public float minSpawnTime; // control the rate of the ememies spawning
    public float maxSpawnTIme;  
    public int aliensPerSpawn; // how many aliens will spawn per time
    private int aliensOnScreen = 0; //shows total aliens will tell manager if more are needed or not
    private float generatedSpawnTime = 0.0f; // trak time between spawns randomize to keep on toes
    private float currentSpawnTime = 0.0f; // track miliseconds since last spawn

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentSpawnTime += Time.deltaTime; //will add all the time between each frame
        if (currentSpawnTime > generatedSpawnTime)
        {
            currentSpawnTime = 0; //resets time after every spawn
        }

        generatedSpawnTime = Random.Range(minSpawnTime, maxSpawnTIme); //generates a random time bewtween times
        if (aliensPerSpawn > 0 && aliensOnScreen < totalAliens) //tell games to spawn enemies or not but wont go past total aliens set
        {
            List<int> previousSpawnLocations = new List<int>(); //makes sure to not spawn more then one enemy from each spot each wave 
            if (aliensPerSpawn > spawnPoints.Length)
            {
                aliensPerSpawn = spawnPoints.Length - 1; //limits number of enemies by spawnpoint
            }

            aliensPerSpawn = (aliensPerSpawn > totalAliens) ? //if spawns go above total aliens will reduce number of spawn 
               aliensPerSpawn - totalAliens : aliensPerSpawn;
            for (int i = 0; i < aliensPerSpawn; i++)
            {
                if (aliensOnScreen < maxAliensOnScreen)
                {
                    aliensOnScreen += 1; //cheaks if aliens on screen is less then max then adds 1
                }
                //1 generates spawn point set to -1 cause it references array
                int spawnPoint = -1;
                //2 loops till spawn is found or spawnpoint is no longer -1
                while (spawnPoint == -1) 
                {
                    //3 random number for spawn 
                    int randomNumber = Random.Range(0, spawnPoints.Length - 1);
                    //4 looks to see if number is active spawn if not will spawn there  
                    if (!previousSpawnLocations.Contains(randomNumber))
                    {
                        previousSpawnLocations.Add(randomNumber);
                        spawnPoint = randomNumber;
                    }
                }
                GameObject spawnLocation = spawnPoints[spawnPoint];
                GameObject newAlien = Instantiate(alien) as GameObject; // creates the alien
                newAlien.transform.position = spawnLocation.transform.position; // spawns alien at spawn point
                Alien alienScript = newAlien.GetComponent<Alien>();
                alienScript.target = player.transform; // reference to alien script 
                Vector3 targetRotation = new Vector3(player.transform.position.x,
                      newAlien.transform.position.y, player.transform.position.z);
                newAlien.transform.LookAt(targetRotation); // rotates alien on y axis toward player 
            }
        }
    }
}
