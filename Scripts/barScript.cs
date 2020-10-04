using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class barScript : MonoBehaviour
{
    public Follower datas;
    public Image bar;
    public Text text;
    private bool x;
    void Update()
    {
        float progress = datas.currentFuelBar / datas.maxfuelBar;
        bar.fillAmount = progress;
        if(datas.inPlanet == true)
        {
            text.color = new Color(0, 1, 0);
        }
        else
        {
            text.color = new Color(255, 205, 0);
        }
        if(datas.fuels < 0)
        {
            text.text = "X";
        }
        else
        {
            text.text = datas.fuels.ToString();
        }
    }
}
