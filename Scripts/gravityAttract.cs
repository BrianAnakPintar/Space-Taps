using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravityAttract : MonoBehaviour
{
    public Rigidbody rb;
    public Rigidbody rocketrb;
    const float G = 5.674f;
    public string planetName;
    public Follower rocketScript;

    void Pull ()
    {
        Vector3 direction = rb.position - rocketrb.position;
        float distance = direction.magnitude;
        float forceMagnitude = G*(rb.mass * rocketrb.mass) / Mathf.Pow(distance, 2);
        Vector3 force = direction.normalized * forceMagnitude;

        rocketrb.AddForce(force);
    }

    private void FixedUpdate()
    {
        if(rocketScript.inPlanet == true)
        {
            if(rocketScript.currentFuelBar <= 0)
            {
                if(rocketScript.planetName == planetName)
                {
                    Pull();
                }
            }
        }
    }
}
