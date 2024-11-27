using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// script to make object move to selected target and patrol between two point (also sometimes will idle)
public class Patrol : MonoBehaviour
{
    public Transform Object;                            // this is for declare witch object that will move around (Human)
    public Transform startPosition;                     // there is two point to declare (this is the first point)
    public Transform endPosition;                       // (this is the second point)
    public float speed = 1.5f;                          // object move speed

    int direction = 1;                                  // trackdown the direction of object currently on

    private bool OnWalk;                                // trackdown is the player on WalkMode or idleMode


    // when start the scene object will always start on walkmode
    void Start()
    {
        StartCoroutine(WalkMode());
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 target = currentMovementTarget();                                   // trackdown the direction of object currently on
        
        // this line is to make the object facing the target
        var RotationTarget = Quaternion.LookRotation(target - Object.transform.position, Vector3.up);
        Object.rotation = RotationTarget;

        // if onwalk mode then the player will walk toward the target
        if(OnWalk)
        {
            Object.position = Vector3.MoveTowards(Object.position, target, speed * Time.deltaTime);
        }

        // determine the distance between the target and the object
        float distance = (target - (Vector3)Object.position).magnitude;
        // if the player distance is under MinDistance(0.5) then the object will change target 
        if (distance <= 0.5f)
        {
            direction *= -1;
        }
    }


    // trackdown the direction of object currently on (if direction -1 the the target will change to endpostion, if it 1 then it will change to start position)
    Vector3 currentMovementTarget()
    {
        if(direction == 1)
        {
            return startPosition.position;
        }
        else
        {
            return endPosition.position;
        }
    }


    // switching to walkmode (with random time between 8 to 10 seconds) then it will auto switch to idle mode
    IEnumerator WalkMode()
    {
        OnWalk = true;
        var RandomWalkTime = Random.Range(8, 10);
        yield return new WaitForSeconds(RandomWalkTime);
        StartCoroutine(IdleMode());
    }

    // switching to idlemode (with random time between 0.5f to 1 seconds) then it will auto switch to walk mode
    IEnumerator IdleMode()
    {
        OnWalk = false;
        var RandomStayTime = Random.Range(0.5f, 1);
        yield return new WaitForSeconds(RandomStayTime);
        StartCoroutine(WalkMode());
    }



     // you can delete this, its just for debugging (it will show you white line that the player will go from the object to startpoin to endpoint)
    private void OnDrawGizmos()
    {
         // draw a line between one position to another
        if(Object != null && startPosition != null && endPosition != null)
        {
            Gizmos.DrawLine(Object.transform.position, startPosition.position);
            Gizmos.DrawLine(Object.transform.position, endPosition.position);
        }
    }
}
