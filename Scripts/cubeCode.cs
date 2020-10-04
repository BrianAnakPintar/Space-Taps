using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class cubeCode : MonoBehaviour
{
    //Path Creator
    public PathCreator pathCreator;
    public float speed;
    float distanceTravelled;
    public Follower rocketScript;
    // Update is called once per frame
    void Update()
    {
        speed = rocketScript.speed + 0.1f;
        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
    }

}
