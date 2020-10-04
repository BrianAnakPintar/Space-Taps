using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour
{
    public Follower datas;
    public Text currentPlanet;
    public Text  jupiter;

    // Update is called once per frame
    void Update()
    {
        currentPlanet.text = datas.planetName;
    }
}
