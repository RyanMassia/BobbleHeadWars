using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject player; //represents the hero
    public GameObject[] spawnPoints; //represents all the spawn points
    public GameObject alien; //represents the alien menace
    public int maxAliensOnScreen; //how many enemies on screen
    public int totalAliens; //counts all enemies on screen
    public float minSpawnTime; // control the rate of the ememies spawning
    public float maxSpawnTime;
    public int aliensPerSpawn; // how many aliens will spawn per time
    public GameObject upgradePrefab;// player must collide with to get the update
    public Gun gun; // reference to Gun script
    public float upgradeMaxTimeSpawn = 7.5f;

    private bool spawnedUpgrade = false;
    private float actualUpgradeTime = 0;
    private float currentUpgradeTime = 0;
    private int aliensOnScreen = 0; //shows total aliens will tell manager if more are needed or not
    private float generatedSpawnTime = 0; // trak time between spawns randomize to keep on toes
    private float currentSpawnTime = 0; // track miliseconds since last spawn

    // Use this for initialization
    void Start ()
    {
        actualUpgradeTime = Random.Range(upgradeMaxTimeSpawn - 3.0f, upgradeMaxTimeSpawn);  // upgrade time is a random number generated
        actualUpgradeTime = Mathf.Abs(actualUpgradeTime);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (player == null)
        {
            return;
        }
        currentUpgradeTime += Time.deltaTime; // s adds the amount of time from the past frame
        currentSpawnTime += Time.deltaTime; //will add all the time between each frame

        if (currentUpgradeTime > actualUpgradeTime) 
        {
            // 1s checks if the upgrade has already spawned
            if (!spawnedUpgrade)
            {

                int randomNumber = Random.Range(0, spawnPoints.Length - 1);
                GameObject spawnLocation = spawnPoints[randomNumber];
                // 3spawning the upgrade and associating the gun with it
                GameObject upgrade = Instantiate(upgradePrefab) as GameObject;
                Upgrade upgradeScript = upgrade.GetComponent<Upgrade>();
                upgradeScript.gun = gun;
                upgrade.transform.position =
                spawnLocation.transform.position;
                // 4 upgrade has been spawned
                spawnedUpgrade = true;
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.powerUpAppear);
            }
        }

        if (currentSpawnTime > generatedSpawnTime)
        {
            currentSpawnTime = 0;  //resets time after every spawn
            generatedSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);//generates a random time bewtween times
            if (aliensPerSpawn > 0 && aliensOnScreen < totalAliens)  //tell games to spawn enemies or not but wont go past total aliens set
            {
                List<int> previousSpawnLocations = new List<int>(); //makes sure to not spawn more then one enemy from each spot each wave
                if (aliensPerSpawn > spawnPoints.Length)
                {
                    aliensPerSpawn = spawnPoints.Length - 1; //limits number of enemies by spawnpoint
                }
                aliensPerSpawn = (aliensPerSpawn > totalAliens) ? aliensPerSpawn - totalAliens : aliensPerSpawn; //if spawns go above total aliens will reduce number of spawn 
                for (int i = 0; i < aliensPerSpawn; i++)
                {
                    if (aliensOnScreen < maxAliensOnScreen)
                    {
                        aliensOnScreen += 1; //cheaks if aliens on screen is less then max then adds 1
                        int spawnPoint = -1;//1 generates spawn point set to -1 cause it references array
                        while (spawnPoint == -1)//2 loops till spawn is found or spawnpoint is no longer -1
                        {
                            int randomNumber = Random.Range(0, spawnPoints.Length - 1); //3 random number for spawn
                            if (!previousSpawnLocations.Contains(randomNumber)) //4 looks to see if number is active spawn if not will spawn there 
                            {
                                previousSpawnLocations.Add(randomNumber);
                                spawnPoint = randomNumber;
                            }
                        }
                        GameObject spawnLocation = spawnPoints[spawnPoint];
                        GameObject newAlien = Instantiate(alien) as GameObject; // creates the alien
                        newAlien.transform.position = spawnLocation.transform.position; // spawns alien at spawn point
                        Alien alienScript = newAlien.GetComponent<Alien>();// reference to alien script 
                        alienScript.target = player.transform;
                        Vector3 targetRotation = new Vector3(player.transform.position.x, newAlien.transform.position.y, player.transform.position.z);
                        newAlien.transform.LookAt(targetRotation);// rotates alien on y axis toward player
                        alienScript.OnDestroy.AddListener(AlienDestroyed);
                    }
                }
            }
        }
    }
    public void AlienDestroyed()
    {
        //Debug.Log("dead alien");
        aliensOnScreen -= 1;
        totalAliens -= 1;
    }
}
