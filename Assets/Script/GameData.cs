using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class GameData : MonoBehaviour
{
    [SerializeField] bool realTimeUpdate;

    public static float chargeSpeed;
    public static float maxSpeedX;
    public static float maxSpeedY;
    public static float critDamageMultiplier;
    public static float playerHP;
    public static float thinkTime;
    public static float warnTime;
    public static float maxWindSpeed;

    public static List<int> powerUpAttAmount = new List<int>();
    public static List<float> powerUpDamage = new List<float>();

    public static List<float> enemyHP = new List<float>();
    public static List<float> enemyMC = new List<float>();


    void Start()
    {
        StartCoroutine(ObtainGameData());
        StartCoroutine(LoadPowerUpData());
        StartCoroutine(LoadEnemyAIData());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (realTimeUpdate)
        {
            StartCoroutine(ObtainGameData());
            StartCoroutine(LoadPowerUpData());
            StartCoroutine(LoadEnemyAIData());
        }
    }

    IEnumerator ObtainGameData()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://sheets.googleapis.com/v4/spreadsheets/1EvtYh1pdawobEI3_VKR6tfr970uYd9OimzjuUvMzQR8/values/GameSetting?key=AIzaSyCZaMfQf-868AAqeQkpTT1DR1sWb3StsE4");
        yield return www.SendWebRequest();
        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("ERROR: " + www.error);
        }
        else
        {
            bool allset = true;
            string json = www.downloadHandler.text;
            //Debug.Log(json);
            var o = JSON.Parse(json);

            if (float.TryParse(o["values"][1][1], out chargeSpeed))
            {
                
            }
            else
            {
                Debug.Log("chargeSpeed Set failed");
                allset = false;
            }
            if (float.TryParse(o["values"][2][1], out maxSpeedX))
            {
                
            }
            else
            {
                Debug.Log("maxSpeedX Set failed");
                allset = false;
            }
            if (float.TryParse(o["values"][3][1], out maxSpeedY))
            {
                
            }
            else
            {
                Debug.Log("maxSpeedY Set failed");
                allset = false;
            }
            if (float.TryParse(o["values"][4][1], out critDamageMultiplier))
            {
                
            }
            else
            {
                Debug.Log("critDamageMultiplier Set failed");
                allset = false;
            }
            if (float.TryParse(o["values"][5][1], out playerHP))
            {
                
            }
            else
            {
                Debug.Log("playerHP Set failed");
                allset = false;
            }
            if (float.TryParse(o["values"][6][1], out thinkTime))
            {
                
            }
            else
            {
                Debug.Log("thinkTime Set failed");
                allset = false;
            }
            if (float.TryParse(o["values"][7][1], out warnTime))
            {
                
            }
            else
            {
                Debug.Log("warnTime Set failed");
                allset = false;
            }
            if (float.TryParse(o["values"][8][1], out maxWindSpeed))
            {

            }
            else
            {
                Debug.Log("maxWindSpeed Set failed");
                allset = false;
            }

            if (allset)
            {
                Debug.Log("Game Data Load Complete");
            }

        }
    }

    IEnumerator LoadPowerUpData()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://sheets.googleapis.com/v4/spreadsheets/1EvtYh1pdawobEI3_VKR6tfr970uYd9OimzjuUvMzQR8/values/PowerUpInfo?key=AIzaSyCZaMfQf-868AAqeQkpTT1DR1sWb3StsE4");
        yield return www.SendWebRequest();
        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("ERROR: " + www.error);
        }
        else
        {
            bool allset = true;
            string json = www.downloadHandler.text;
            //Debug.Log(json);
            var o = JSON.Parse(json);

            int amount;
            float damage;

            for (int i = 1; i < o["values"].Count; i++)
            {
                if (int.TryParse(o["values"][i][1], out amount))
                {
                    powerUpAttAmount.Add(amount);
                }
                else
                {
                    Debug.Log("PowerUp " + i + " attack amount set failed");
                    powerUpAttAmount.Add(1);
                    allset = false;
                }
                //powerUpAttAmount.Add(amount);
                if (float.TryParse(o["values"][i][2], out damage))
                {
                    powerUpDamage.Add(damage);
                }
                else
                {
                    Debug.Log("PowerUp " + i + " damage set failed");
                    powerUpDamage.Add(5);
                    allset = false;
                }
            }


            if (allset)
            {
                Debug.Log("PowerUp Data Load Complete");
            }
        }
    }

    IEnumerator LoadEnemyAIData()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://sheets.googleapis.com/v4/spreadsheets/1EvtYh1pdawobEI3_VKR6tfr970uYd9OimzjuUvMzQR8/values/EnemyAISetting?key=AIzaSyCZaMfQf-868AAqeQkpTT1DR1sWb3StsE4");
        yield return www.SendWebRequest();
        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("ERROR: " + www.error);
        }
        else
        {
            bool allset = true;
            string json = www.downloadHandler.text;
            //Debug.Log(json);
            var o = JSON.Parse(json);

            float hp = 50f;
            float missedchance = 100f;

            for (int i = 1; i < o["values"].Count; i++)
            {
                if (float.TryParse(o["values"][i][1], out hp))
                {
                    enemyHP.Add(hp);
                }
                else
                {
                    Debug.Log("Enemy " + i + " hp set failed");
                    enemyHP.Add(50f);
                    allset = false;
                }
                if (float.TryParse(o["values"][i][2], out missedchance))
                {
                    enemyMC.Add(missedchance);
                }
                else
                {
                    Debug.Log("Enemy " + i + " missedchance set failed");
                    enemyMC.Add(50f);
                    allset = false;
                }
            }


            if (allset)
            {
                Debug.Log("PowerUp Data Load Complete");
            }
        }
    }

}
