using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    [SerializeField] bool multiplay = false;
    [SerializeField] public bool auntTurn;

    [SerializeField] Player aunt;
    //[SerializeField] float auntMaxHp;
    //[SerializeField] float auntCurHp;
    [SerializeField] GameObject auntHpBar;
    [SerializeField] GameObject auntHitbox;
    [SerializeField] GameObject auntShootButton;

    [SerializeField] Player pig;
    //[SerializeField] float pigMaxHp;
    //[SerializeField] float pigCurHp;
    [SerializeField] GameObject pigHpBar;
    [SerializeField] GameObject pigHitbox;
    [SerializeField] GameObject pigShootButton;

    float thinkTime = 30.0f;
    float warnTime = 10.0f;

    
    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject h2pPanel;
    [SerializeField] GameObject modePanel;
    [SerializeField] GameObject gameEndPanel;
    [SerializeField] Text winText;

    [SerializeField] WindField wf;

    private void Start()
    {
        startPanel.SetActive(true);
        h2pPanel.SetActive(false);
        modePanel.SetActive(false);
        gameEndPanel.SetActive(false);
        auntHpBar.SetActive(false);
        auntShootButton.SetActive(false);
        pigHpBar.SetActive(false);
        pigShootButton.SetActive(false);

        //SetUp(multiplay);
    }

    public void StartPlay()
    {
        modePanel.SetActive(true);
        startPanel.SetActive(false);
        h2pPanel.SetActive(false);
    }

    public void How2Play()
    {
        h2pPanel.SetActive(true);
        startPanel.SetActive(false);
    }

    public void SetUp(bool multi)
    {
        modePanel.SetActive(false);
        auntHpBar.SetActive(true);
        pigHpBar.SetActive(true);
        if (multi)
        {

        }
        else
        {
            auntHitbox.SetActive(false);
            auntShootButton.SetActive(true);

            pigHitbox.SetActive(true);
            pigShootButton.SetActive(false);

            auntTurn = false;
            StartCoroutine(TurnCountDown());
        }
        wf.GenerateWind();
    }

    private void ChangeTurn()
    {
        //Stop if already change turn
        StopCoroutine(TurnCountDown());
        if (auntTurn)
        {
            auntHitbox.SetActive(false);
            auntShootButton.SetActive(true);

            pigHitbox.SetActive(true);
            pigShootButton.SetActive(false);

            auntTurn = false;
            StartCoroutine(TurnCountDown());
        }
        else
        {
            auntHitbox.SetActive(true);
            auntShootButton.SetActive(false);

            pigHitbox.SetActive(false);
            pigShootButton.SetActive(true);

            auntTurn = true;
            StartCoroutine(TurnCountDown());
        }
        wf.GenerateWind();
    }

    public void WaitResult()
    {
        auntShootButton.SetActive(false);
        pigShootButton.SetActive(false);
        StopAllCoroutines();
        StartCoroutine(ShootWait());
    }

    IEnumerator ShootWait()
    {
        yield return new WaitForSecondsRealtime(3.0f);
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
        auntShootButton.SetActive(false);
        pigShootButton.SetActive(false);
        if (multiplay)
        {

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
        yield return new WaitForSecondsRealtime(warnTime);

        ChangeTurn();
    }

    

}
