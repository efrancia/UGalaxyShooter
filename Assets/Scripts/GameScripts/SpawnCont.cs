using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCont : MonoBehaviour
{
    [SerializeField] GameObject Enemy;
    [SerializeField] GameObject enemyContainer;

    [SerializeField] GameObject TSPU;
    [SerializeField] GameObject SBPU;
    [SerializeField] GameObject SPU;
    [SerializeField] GameObject[] newPuP;

    [SerializeField] GameObject Asteroid;

    bool keepSpawning = true;
    int PuPToSpawn;
    float _timer = 1.5f;

    
    // Start is called before the first frame update
    void Start()
    {
        // needed to start coroutine
        Routines();
    }

    void Routines() { 
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnPowerUp());
        StartCoroutine(AsteroidSpawn());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator AsteroidSpawn() {

        while (keepSpawning)
        {
           
            yield return new WaitForSeconds(Random.Range(10.0f,20.0f)*_timer);
            //good habit to assign new instance to a variable to edit.
            GameObject newAsteroid = Instantiate(Asteroid, new Vector3(Random.Range(-5.0f, 5.0f), 5.0f, 5.0f), Quaternion.identity);
            //assigns the newly created objects to a proper container due to reuse
            newAsteroid.transform.parent = enemyContainer.transform.parent;
            
        }
    }
   
    public IEnumerator SpawnEnemy()
    {
        while (keepSpawning)
        {
            yield return new WaitForSeconds(_timer);
            //good habit to assign new instance to a variable to edit.
            GameObject newEnemy = Instantiate(Enemy, new Vector3(Random.Range(-5.0f, 5.0f), 4.0f, 5.0f), Quaternion.identity);
            //assigns the newly created objects to a proper container due to reuse
            newEnemy.transform.parent = enemyContainer.transform;   
        }

    }
    public IEnumerator SpawnPowerUp()
    {
        while (keepSpawning)
        {
            yield return new WaitForSeconds(Random.Range(7.0f,10.0f));
            PuPToSpawn = Random.Range(0,newPuP.Length);
            Instantiate(newPuP[PuPToSpawn], new Vector3(Random.Range(-5.0f, 5.0f), 4.0f, 5.0f), Quaternion.identity);
            
        }
    }
    public void playerDeath() {
        keepSpawning = false;
    }
    public void setDiff(float newTimer) {
        _timer = newTimer;
    }
}
