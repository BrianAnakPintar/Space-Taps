using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using EZCameraShake;
using UnityEngine.VFX;
using UnityEngine.UI;
public class Follower : MonoBehaviour
{
    [Header("Path Creator")]
    //Path Creator
    public PathCreator pathCreator;
    public float speed;
    float distanceTravelled;

    [Header("CamShake")]
    //CameraShake
    public float magnitude;
    public float roughness;
    public float fadeInTime;
    public float fadeOutTime;

    [Header("Fuel System")]
    //Fuel Bars
    public int fuels;
    public float maxfuelBar = 100f;
    public float currentFuelBar;
    public float speedGain;
    public float barDrain;

    [Header("Debris System")]
    public GameObject firstStage;
    public GameObject secondStage;
    public GameObject thirdStage;
    public GameObject destroyFirst;
    public GameObject destroySecond;
    public BoxCollider boxCollider;

    [Header("Launch System")]
    private bool hasLaunched;
    public float startSpeed;

    [Header("Landing Settings")]
    //Landing System
    public bool inPlanet;
    public string planetName;
    private bool hasLanded = false;
    public GameObject land;
    public GameObject crash;
    public GameObject endUI;
    public GameObject otherUI;
    public Text JupiterUI;

    [Header("Particles")]
    //Particles
    public ParticleSystem nuclear;
    public ParticleSystem crashParticle;
    public VisualEffect warpSpeedFX;
    public GameObject cube;
    public ParticleSystem fireTrail;
    public GameObject shield;

    [Header("Tapping")]
    private bool tap;
    public Button button;
    public GameObject boostButton;
    private void Start()
    {
        currentFuelBar = maxfuelBar;
        crashParticle.Stop();
        StartCoroutine(startEngine());
    }
    private void Update()
    {
        distanceTravelled += speed * Time.deltaTime;
        if (hasLaunched == true)
        {
            currentFuelBar -= barDrain * Time.deltaTime;
        }                     
        if (currentFuelBar < 0)
        {
            speed = 0f;
            Destroy(boostButton);
            endUI.SetActive(true);
            otherUI.SetActive(false);
            if (inPlanet == true)
            {
                if(planetName == "jupiter")
                {
                    //Crashed UI
                    crash.SetActive(true);
                    JupiterUI.enabled = true;
                    //Play Destroy Particles
                    Instantiate(crashParticle, transform.position, Quaternion.identity);
                    Instantiate(thirdStage, transform.position, Quaternion.identity);
                    Destroy(cube);
                    Destroy(gameObject);
                }
                else
                {
                    //Landing Successful UI
                    fuels = -1;
                    fireTrail.Stop(true);
                    land.SetActive(true);
                    hasLanded = true;
                }
            }
            else if (hasLanded == false)
            {
                //Crashed UI
                crash.SetActive(true);
                //Play Destroy Particles
                Instantiate(crashParticle, transform.position, Quaternion.identity);
                Instantiate(thirdStage, transform.position, Quaternion.identity);
                Destroy(cube);
                Destroy(gameObject);
                
            }
        }
        else
        {
            //Follow Path
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
        }
        if (fuels >= 0)
        {
            if (tap == true)
            {
                StartCoroutine(boost());
                tap = false;
            }
        }
        if (fuels == -1)
        {
            if(tap == true)
            {
                currentFuelBar = 1;
                tap = false;
            }
        }
    }
    IEnumerator boost()
    {
        speedGain = (maxfuelBar - currentFuelBar);
        speed += speedGain/8;
        CameraShaker.Instance.ShakeOnce(magnitude, roughness, fadeInTime, fadeOutTime);
        currentFuelBar = maxfuelBar;
        fuels -= 1;
        warpSpeedFX.Play();
        nuclear.Play();
        shield.SetActive(true);

        yield return new WaitForSeconds(2);
        speed = 5;
        speedGain = 0f;
        nuclear.Stop();
        warpSpeedFX.Stop();
        shield.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Planet"))
        {
            inPlanet = true;
            planetName = other.name;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Planet"))
        {
            inPlanet = false;
            planetName = null;
        }
    }
    IEnumerator startEngine()
    {
        hasLaunched = false;
        speed = startSpeed;
        CameraShaker.Instance.ShakeOnce(magnitude, roughness, fadeInTime, fadeOutTime);
        warpSpeedFX.Play();
        fireTrail.Play();   

        yield return new WaitForSeconds(2);


        Instantiate(firstStage, destroyFirst.transform.position, Quaternion.identity);
        Destroy(destroyFirst);
        warpSpeedFX.transform.localScale = new Vector3(2, 2, 3);
        fireTrail.Stop();
        warpSpeedFX.Stop();
        speed = startSpeed - 5f;

        yield return new WaitForSeconds(2);
        hasLaunched = true;
        Instantiate(secondStage, destroySecond.transform.position, Quaternion.identity);
        Destroy(destroySecond);
        warpSpeedFX.transform.localScale = new Vector3(2, 2, 2);
        speed = 1f;
    }
    public void buttonClick()
    {
        tap = true;
    }
}
