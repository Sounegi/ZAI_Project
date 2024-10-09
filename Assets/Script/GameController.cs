using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] bool multiplay = false;
    //[SerializeField] int difficulty = 0;
    [SerializeField] PigAI ai;
    [SerializeField] public bool auntTurn;
    [SerializeField] private bool gameEnd;

    [SerializeField] Player aunt;
    //[SerializeField] float auntMaxHp;
    //[SerializeField] float auntCurHp;
    [SerializeField] GameObject auntHpBar;
    [SerializeField] GameObject auntPowerUp;
    [SerializeField] GameObject auntHitbox;
    [SerializeField] GameObject auntShootButton;
    [SerializeField] GameObject auntPointer;
    //[SerializeField] GameObject auntAim;

    [SerializeField] Player pig;
    //[SerializeField] float pigMaxHp;
    //[SerializeField] float pigCurHp;
    [SerializeField] GameObject pigHpBar;
    [SerializeField] GameObject pigPowerUp;
    [SerializeField] GameObject pigHitbox;
    [SerializeField] GameObject pigShootButton;
    [SerializeField] List<GameObject> pigPowerButton;
    [SerializeField] GameObject pigPointer;
    //[SerializeField] GameObject pigAim;

    /*
    [SerializeField] GameObject auntDefaultBullet;
    [SerializeField] GameObject auntPowerBullet;
    [SerializeField] GameObject pigDefaultBullet;
    [SerializeField] GameObject pigPowerBullet;    
    
    [SerializeField] int normAmount;
    [SerializeField] float normDamage;
    [SerializeField] int bombAmount;
    [SerializeField] float bombDamage;
    [SerializeField] int doubAmount;
    [SerializeField] float doubDamage;
    [SerializeField] int poopAmount;
    [SerializeField] float poopDamage;
    */
    [SerializeField] float healAmount;
    public List<BulletSetting> bullets;

    float thinkTime;
    float warnTime;
    
    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject h2pPanel;
    [SerializeField] GameObject modePanel;
    [SerializeField] GameObject difficultyPanel;
    [SerializeField] GameObject gameEndPanel;
    [SerializeField] Text winText;
    [SerializeField] GameObject warningTimer;
    [SerializeField] GameObject windBar;

    [SerializeField] WindField wf;



    private void Start()
    {
        gameEnd = false;
        startPanel.SetActive(true);
        h2pPanel.SetActive(false);
        modePanel.SetActive(false);
        warningTimer.SetActive(false);
        windBar.SetActive(false);
        gameEndPanel.SetActive(false);
        auntHpBar.SetActive(false);
        foreach(Transform child in auntPowerUp.transform)
        {
            child.gameObject.SetActive(true);
        }
        auntPowerUp.SetActive(false);
        auntShootButton.SetActive(false);
        pigHpBar.SetActive(false);
        foreach (Transform child in pigPowerUp.transform)
        {
            child.gameObject.SetActive(true);
        }
        foreach (GameObject button in pigPowerButton)
        {
            button.SetActive(true);
        }
        pigPowerUp.SetActive(false);
        pigShootButton.SetActive(false);
        aunt.ResetHealth();
        pig.ResetHealth();
        //SetUp(multiplay);
    }

    public void StartPlay()
    {
        modePanel.SetActive(true);
        startPanel.SetActive(false);
        h2pPanel.SetActive(false);

        thinkTime = GameData.thinkTime;
        warnTime = GameData.warnTime;
        LoadPowerUpData();
    }

    public void How2Play()
    {
        h2pPanel.SetActive(true);
        startPanel.SetActive(false);
    }

    public void SetUp(bool multi)
    {
        modePanel.SetActive(false);
        
        if (multi)
        {
            auntHpBar.SetActive(true);
            pigHpBar.SetActive(true);
            windBar.SetActive(true);
            auntTurn = true;

            ChangeBullet(0);

            auntHitbox.SetActive(false);
            auntShootButton.SetActive(true);
            auntPowerUp.SetActive(true);
            auntPointer.SetActive(true);

            pigHitbox.SetActive(true);
            pigShootButton.SetActive(false);
            pigPowerUp.SetActive(false);
            pigPointer.SetActive(false);

            aunt.maxHp = GameData.playerHP;
            pig.maxHp = GameData.playerHP;

            StartCoroutine(TurnCountDown());
            multiplay = true;
        }
        else
        {
            aunt.maxHp = GameData.playerHP;
            difficultyPanel.SetActive(true);
            multiplay = false;
            /*
            auntTurn = true;
            ChangeBullet(0);
            auntHitbox.SetActive(false);
            auntShootButton.SetActive(true);
            auntPowerUp.SetActive(true);

            pigHitbox.SetActive(true);
            pigShootButton.SetActive(false);
            pigPowerUp.SetActive(false);

            StartCoroutine(TurnCountDown());
            
            */
        }
        wf.GenerateWind();
    }

    public void SetAIDifficulty(int dif)
    {
        difficultyPanel.SetActive(false);
        ai.SetDifficult(dif);
        foreach(GameObject button in pigPowerButton)
        {
            button.SetActive(false);
        }

        auntHpBar.SetActive(true);
        pigHpBar.SetActive(true);
        windBar.SetActive(true);
        auntTurn = true;

        ChangeBullet(0);

        auntHitbox.SetActive(false);
        auntShootButton.SetActive(true);
        auntPowerUp.SetActive(true);
        auntPointer.SetActive(true);

        pigHitbox.SetActive(true);
        pigShootButton.SetActive(false);
        pigPowerUp.SetActive(false);
        pigPointer.SetActive(false);

        StartCoroutine(TurnCountDown());
    }

    private void ChangeTurn()
    {
        //Stop if already change turn
        warningTimer.SetActive(false);
        StopCoroutine(TurnCountDown());
        wf.GenerateWind();
        if (auntTurn)
        {
            ChangeBullet(0);
            auntHitbox.SetActive(false);
            auntShootButton.SetActive(true);
            auntPowerUp.SetActive(true);
            auntPointer.SetActive(true);

            pigHitbox.SetActive(true);
            pigShootButton.SetActive(false);
            pigPowerUp.SetActive(false);
            pigPointer.SetActive(false);

            StartCoroutine(TurnCountDown());
        }
        else
        {
            if (multiplay)
            {
                ChangeBullet(1);
                auntHitbox.SetActive(true);
                auntShootButton.SetActive(false);
                auntPowerUp.SetActive(false);
                auntPointer.SetActive(false);

                pigHitbox.SetActive(false);
                pigShootButton.SetActive(true);
                pigPowerUp.SetActive(true);
                pigPointer.SetActive(true);

                StartCoroutine(TurnCountDown());
            }
            else
            {
                ChangeBullet(1);
                auntHitbox.SetActive(true);
                auntShootButton.SetActive(false);
                auntPowerUp.SetActive(false);
                auntPointer.SetActive(false);

                pigHitbox.SetActive(false);
                pigPowerUp.SetActive(true);
                //Instantiate AI behavior here
                ai.TakeShoot();

                StartCoroutine(TurnCountDown());
            }
            
        }
        //wf.GenerateWind();
    }

    public void WaitResult()
    {
        auntShootButton.SetActive(false);
        pigShootButton.SetActive(false);
        auntPowerUp.SetActive(false);
        pigPowerUp.SetActive(false);

        StopAllCoroutines();
        StartCoroutine(ShootWait());
    }

    IEnumerator ShootWait()
    {
        yield return new WaitForSecondsRealtime(3.0f);
        auntTurn = !auntTurn;
        if(!gameEnd)
            ChangeTurn();
    }

    public void CheckHp()
    {
        if (aunt.curHp <= 0.0f)
        {
            GameEnd(false);
            return;
        }
        else if(pig.curHp <= 0.0f)
        {
            GameEnd(true);
            return;
        }
        /*
        if (hitAunt)
        {
            auntCurHp -= damage;
            auntHpBar.value = auntCurHp/auntMaxHp;
            if(auntCurHp <= 0.0f)
            {
                GameEnd(false);
                return;
            }
        }
        else
        {
            pigCurHp -= damage;
            pigHpBar.value = pigCurHp / pigMaxHp;
            if (pigCurHp <= 0.0f)
            {
                GameEnd(true);
                return;
            }
        }
        */
        Debug.Log("Hit!");
    }


    private void GameEnd(bool auntWin)
    {
        StopAllCoroutines();
        gameEnd = true;
        auntShootButton.SetActive(false);
        pigShootButton.SetActive(false);
        if (!multiplay)
        {
            if (auntWin)
            {
                winText.text = "You Win!";
            }
            else
            {
                winText.text = "You Lose..";
            }
        }
        else
        {
            if (auntWin)
            {
                winText.text = "Aunt Win!";
            }
            else
            {
                winText.text = "RichPig Win!";
            }
        }
        gameEndPanel.SetActive(true);
    }

    public void Replay()
    {
        Start();
    }

    IEnumerator TurnCountDown()
    {
        yield return new WaitForSecondsRealtime(thinkTime-warnTime);
        //show warning
        Debug.Log("Warning 10s left!");
        warningTimer.SetActive(true);
        warningTimer.GetComponent<WarningTimer>().StartTimer();
        yield return new WaitForSecondsRealtime(warnTime);
        auntTurn = !auntTurn;
        if(!gameEnd)
            ChangeTurn();
    }

    //PowerUp
    public void Heal(Player owner)
    {
        owner.TakeDamage(-10);
        if (auntTurn)
        {
            auntShootButton.SetActive(false);
            auntPowerUp.SetActive(false);
            pigHitbox.SetActive(false);
        }
        else
        {
            pigShootButton.SetActive(false);
            pigPowerUp.SetActive(false);
            auntHitbox.SetActive(false);
        }
        StartCoroutine(ShootWait());
    }

    public void ChangeBullet(int index)
    {
        Debug.Log(bullets[index].prefabs != null);
        if (auntTurn)
        {
            aunt.SetBullet(bullets[index]);
            /*
            if (bullets[index].prefabs != null)
            {
                aunt.SetBullet(bullets[index].prefabs);
                Debug.Log("Aunt change prefab");
            }
            
            aunt.shootAmount = bullets[index].shootAmount;
            aunt.bulletDamageData = bullets[index].bulletDamage;
            */
        }
        else
        {
            pig.SetBullet(bullets[index]);
            /*
            if (bullets[index].prefabs != null)
                pig.SetBullet(bullets[index].prefabs);
            pig.shootAmount = bullets[index].shootAmount;
            pig.bulletDamageData = bullets[index].bulletDamage;
            */
        }
        Debug.Log("Bullet Set!");
    }

    private void LoadPowerUpData()
    {
        bullets[0].shootAmount = GameData.powerUpAttAmount[0];
        bullets[0].bulletDamage = GameData.powerUpDamage[0];
        bullets[1].shootAmount = GameData.powerUpAttAmount[0];
        bullets[1].bulletDamage = GameData.powerUpDamage[0];
        for(int i = 2; i < 4; i++)
        {
            bullets[i].shootAmount = GameData.powerUpAttAmount[i - 1];
            bullets[i].bulletDamage = GameData.powerUpDamage[i - 1];
        }

        healAmount = GameData.powerUpDamage[4];
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("LoadingScene");
    }

}

[Serializable]
public class BulletSetting
{
    public GameObject prefabs;
    public int shootAmount;
    public float bulletDamage;
}
