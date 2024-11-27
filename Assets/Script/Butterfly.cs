using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// script to make object move to random position within certain range
public class Butterfly : MonoBehaviour
{
    public float speed;             // object speed toward the target

    // Xsize and Zsize is size for butterfly to roaming
    [Header("AREA POSITION")]
    public float Xsize;             // roaming size X value
    public float Zsize;             // roaming size z value

    public Vector3 TargetPosition;  // target point position
    private Vector3 FirstPosition;  // determined the first point position of the object (so the object wont go far from first point)

    void Start()
    {
        FirstPosition = transform.position;             // determined the FirstPosition (on start of the scene)
        RandomizeTargetPosition();                      // execute Random position function (to start making random target point)
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(transform.position, TargetPosition);          // determined the distance between the target and the object


        // if the player distance still above MinDistance(0.5) then the object will movetoward target 
        if (dist >= 0.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, TargetPosition, speed * Time.deltaTime);
            LookAtTarget();
        }
        // if the player distance under MinDistance then the object will start randomize target position 
        else                                            
        {
            RandomizeTargetPosition();
        }



    }


    // this function is for the object to always face the target (it will rotate toward target)
    public void LookAtTarget()
    {
        Vector3 relativePos = TargetPosition - transform.position;
        var RotationTarget = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = RotationTarget;
    }

    // this function is for randomize target position (range between -Xsize to Xsize and -Zsize to Zsize)
    void RandomizeTargetPosition()
    {
        var RandomNumberX = Random.Range(-Xsize, Xsize);
        var RandomNumberZ = Random.Range(-Zsize, Zsize);
        TargetPosition = new Vector3(RandomNumberX + FirstPosition.x, FirstPosition.y, RandomNumberZ + FirstPosition.z);
    }





    // you can delete this, its just for debugging (it will show you how big the object could roam (if you select the object you can see the red box))
    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent red cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position, new Vector3(Xsize * 2, 1, Zsize * 2));
    }
}
