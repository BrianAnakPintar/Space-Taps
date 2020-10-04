using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Transform target;
    public float smoothing = 10.0f;
    Quaternion lookAtRotation;
    public float offset = 5.0f;
    Vector3 tmp;

    void Update()
    {
        lookAtRotation = Quaternion.LookRotation(target.transform.position - transform.position);

        if (transform.rotation != lookAtRotation)
        {
            tmp = lookAtRotation.eulerAngles;
            tmp.y -= 90;                            // fix the rotation
            tmp.z = (tmp.x * -1) - offset;          // uses the x value to aim vertically and iff offset so it aims at the center of the target
            tmp.x = 0;                              // stops the turret from rotating in the wrong axis

            Quaternion newRot = Quaternion.Euler(tmp);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, newRot, Time.deltaTime * smoothing * 2);
        }
    }
}
