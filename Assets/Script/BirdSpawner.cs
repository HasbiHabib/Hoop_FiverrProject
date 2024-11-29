using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// script to spawn object at random position then launch it forward (auto destruct)
public class BirdSpawner: MonoBehaviour
{
    [Header("BirdPrefab")]
    public GameObject Bird;                                 // what to spawn
    public float Speed;                                     // launch speed forward
    private float Timer;                                     // track the time to spawn another object

    [Header("Wave Setting Proggression")]
    public float MaxTimer;                                  // randomize the spawn rate between the Max time and the min time
    public float MinTimer;

    [Header("AREA POSITION")]
    public float Xsize;                                     // randomize the spawn point between the Xsize and Zsize
    public float Zsize;

    // Update is called once per frame
    void Update()
    {
        // if the timer is not zero it will count down until it hit zero
        if (Timer >= 0)
        {
            Timer -= Time.deltaTime;
        }
        // after the timer hit zero then it will spawn a bird then the time will randomize again between mintimer and maxtimer
        if (Timer < 0)
        {
            Timer =  Random.Range(MinTimer, MaxTimer);
            SpawnBird();
            return;
        }
    }


    // function to spawn a bird on randome position between Xsize and Zsize
    void SpawnBird()
    {
        // randomize spawn point
        var RandomNumberX = Random.Range(-Xsize, Xsize);
        var RandomNumberZ = Random.Range(-Zsize, Zsize);
        var SpawnPoint = new Vector3(RandomNumberX + transform.position.x, transform.position.y, RandomNumberZ + transform.position.z);
        
        // spawn the bird then launch it forward (the prefab require rigidbody to work)
        var thebird =  Instantiate(Bird, SpawnPoint, transform.rotation);
        thebird.GetComponent<Rigidbody>().linearVelocity = thebird.transform.TransformDirection(Vector3.forward * Speed);
        thebird.SetActive(true);
        // the bird will auto destroy in 10 secods
        Destroy(thebird, 25);           
    }



    // you can delete this, its just for debugging (it will show you how big the object spawn point (if you select the object you can see the red box))
    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent red cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position, new Vector3(Xsize * 2, 1, Zsize * 2));
    }
}
