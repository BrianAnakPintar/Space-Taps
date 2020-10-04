using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlanets : MonoBehaviour
{
    public float rotateSpeed;
    void FixedUpdate()
    {
        transform.Rotate(0, rotateSpeed, 0);
    }
}
