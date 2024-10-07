using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;
using System.Globalization;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public float maxHp;
    public float curHp;
    [SerializeField] Slider hpBar;

    [SerializeField] GameObject bulletPF;
    [SerializeField] Transform shootingPoint;
    [SerializeField] GameObject powerGauge;

    public static float chargeSpeed = 0.02f;
    private float maxSpeedX;
    private float maxSpeedY;
    
    [SerializeField] UnityEvent getHit;
    [SerializeField] UnityEvent shoot;


    private void Awake()
    {
        StartCoroutine(ObtainSheetData());
    }
    public void Charge()
    {
        powerGauge.SetActive(true);
        powerGauge.GetComponent<SliderCharger>().charge = true;
    }

    public void Shoot(bool dir)
    {
        powerGauge.GetComponent<SliderCharger>().charge = false;
        float power = powerGauge.GetComponent<Slider>().value;
        Debug.Log("Shoot with " +  power +" force");
        GameObject bullet = Instantiate(bulletPF, shootingPoint);
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2((dir? 1: -1)*maxSpeedX*power, maxSpeedY*power);
        powerGauge.SetActive(false);
        shoot?.Invoke();
    }

    public void TakeDamage(float damage)
    {
        curHp -= damage;
        hpBar.value = curHp / maxHp;
        getHit?.Invoke();
    }

    IEnumerator ObtainSheetData()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://sheets.googleapis.com/v4/spreadsheets/1EvtYh1pdawobEI3_VKR6tfr970uYd9OimzjuUvMzQR8/values/ShootSetting?key=AIzaSyCZaMfQf-868AAqeQkpTT1DR1sWb3StsE4");
        yield return www.SendWebRequest();
        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("ERROR: " + www.error);
        }
        else
        {
            string json = www.downloadHandler.text;
            Debug.Log(json);
            var o = JSON.Parse(json);

            //Debug.Log(o["values"][1][1].ToString().GetType());
            
            if(float.TryParse(o["values"][1][1], out chargeSpeed))
            {
                Debug.Log("chargeSpeed Set: " + chargeSpeed);
            }
            else
            {
                Debug.Log("chargeSpeed Set failed");
            }
            if (float.TryParse(o["values"][2][1], out maxSpeedX))
            {
                Debug.Log("maxSpeedX Set: " + maxSpeedX);
            }
            else
            {
                Debug.Log("maxSpeedX Set failed");
            }
            if (float.TryParse(o["values"][3][1], out maxSpeedY))
            {
                Debug.Log("maxSpeedY Set: " + maxSpeedY);
            }
            else
            {
                Debug.Log("maxSpeedY Set failed");
            }
            //chargeSpeed = float.Parse(o["values"][1][1]);
            /*
            foreach (var item in o["values"])
            {
                Debug.Log(item.GetType());
            }
            */
        }
    }

    
}

