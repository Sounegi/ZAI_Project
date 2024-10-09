using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigAI : MonoBehaviour
{
    private int difficulty;
    [SerializeField] Player pig;
    [SerializeField] GameController game;
    [SerializeField] float test;
    [SerializeField] WindField wind;
    [SerializeField] private float maxSpeedX;
    //private float maxHp;
    [SerializeField] private float missedChance;
    [SerializeField] List<GameObject> pigPowerUp;
    [SerializeField] GameObject pigAim;

    public void SetDifficult(int dif)
    {
        difficulty = dif;
        //get difficulty seeting here
        pig.maxHp = GameData.enemyHP[difficulty];
        pig.ResetHealth();
        missedChance = GameData.enemyMC[difficulty]/100.0f;
    }
    public bool MissShoot()
    {
        if (Random.Range(0.0f, 1.0f) > missedChance)
            return false;
        else
            return true;
    }

    private float CalculateWind()
    {
        //return test;
        
        switch (difficulty)
        {
            case 0:
                //doesn't care wind
                return Random.Range(10.5f/maxSpeedX, 11.7f/maxSpeedX);
            case 1:
                Debug.Log(wind.windSpeed.x);
                if(wind.windSpeed.x < 1.5f && wind.windSpeed.x > -1.5f && pigPowerUp[1].activeSelf == true)
                {
                    game.ChangeBullet(2);
                    StartCoroutine(UsePowerUp(1));
                }
                return Random.Range((10.5f / maxSpeedX) - (wind.windSpeed.x * 0.04f), (11.7f / maxSpeedX) - (wind.windSpeed.x * 0.04f));

                
                /*
                else if(wind.windSpeed.x > 1.5f)
                {
                    return Random.Range(11.7f / maxSpeedX, 13.0f / maxSpeedX);
                }
                else
                {
                    return Random.Range(9.0f / maxSpeedX, 10.5f / maxSpeedX);
                }
                */
        
            case 2:

                if (wind.windSpeed.x < 1.5f && wind.windSpeed.x > -1.5f && pigPowerUp[1].activeSelf == true)
                {
                    game.ChangeBullet(2);
                    StartCoroutine(UsePowerUp(1));
                }
                else if((wind.windSpeed.x > 2.5f || wind.windSpeed.x < -2.5f) && pigPowerUp[0].activeSelf == true)
                {
                    game.ChangeBullet(4);
                    StartCoroutine(UsePowerUp(0));
                }
                else if(pig.curHp <= 15.0f && pigPowerUp[2].activeSelf == true)
                {
                    game.Heal(pig);
                    StartCoroutine(UsePowerUp(2));
                }
                return Random.Range((10.5f / maxSpeedX) - (wind.windSpeed.x * 0.04f), (11.7f / maxSpeedX) - (wind.windSpeed.x * 0.04f));

            default:
                return 1.0f;

        }
        
        
    }

    IEnumerator UsePowerUp(int index)
    {
        yield return new WaitForSeconds(2.0f);
        pigPowerUp[index].SetActive(false);
    }



    public void TakeShoot()
    {
        float chargeTime = CalculateWind();
        if (MissShoot())
        {
            Debug.Log("I'm gonna missed");
            //modify chargeTime to missed
            chargeTime += Random.Range(0.5f, 1.5f);
        }
        Debug.Log("chargetime: " + chargeTime);
        StartCoroutine(ChargeShoot(chargeTime));
        Debug.Log("AI take the shoot");
    }

    IEnumerator ChargeShoot(float time)
    {
        yield return new WaitForSeconds(2.0f);
        pig.Charge();
        pigAim.SetActive(true);
        yield return new WaitForSeconds(time);
        pig.Shoot(true);
        pigAim.SetActive(false);
    }

}
