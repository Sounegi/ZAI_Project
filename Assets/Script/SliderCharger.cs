using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;


public class SliderCharger : MonoBehaviour
{
    [SerializeField] private Slider powerGauge;
    public bool charge;
    [SerializeField] private bool dir;


    void OnEnable()
    {
        powerGauge.value = powerGauge.minValue;
        charge = false;
        dir = false;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (charge)
        {
            if (powerGauge.value >= powerGauge.maxValue) dir = true;
            else if (powerGauge.value <= powerGauge.minValue) dir = false;

            powerGauge.value += dir ? -GameData.chargeSpeed : GameData.chargeSpeed;

        }
    }

}
