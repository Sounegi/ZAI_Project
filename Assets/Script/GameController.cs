using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] bool multiplay = false;
    [SerializeField] bool auntTurn;

    [SerializeField] float auntMaxHp;
    [SerializeField] float auntCurHp;
    [SerializeField] Slider auntHpBar;
    [SerializeField] GameObject auntHitbox;
    [SerializeField] GameObject auntShootButton;

    [SerializeField] float pigMaxHp;
    [SerializeField] float pigCurHp;
    [SerializeField] Slider pigHpBar;
    [SerializeField] GameObject pigHitbox;
    [SerializeField] GameObject pigShootButton;

    float thinkTime = 30.0f;
    float warnTime = 10.0f;

    private void Start()
    {
        SetUp(multiplay);
    }

    private void SetUp(bool multi)
    {
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
    }

    private void ChangeTurn(bool turn)
    {
        //Stop if already change turn
        StopCoroutine(TurnCountDown());
        if (turn)
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
    }

    public void CalculateDamage(bool hitAunt, float damage)
    {
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
        ChangeTurn(auntTurn);
    }

    private void GameEnd(bool auntWin)
    {

    }

    IEnumerator TurnCountDown()
    {
        yield return new WaitForSecondsRealtime(thinkTime-warnTime);
        //show warning
        Debug.Log("Warning 10s left!");
        yield return new WaitForSecondsRealtime(warnTime);

        ChangeTurn(auntTurn);
    }

    

}
