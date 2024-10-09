using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningTimer : MonoBehaviour
{
    [SerializeField] Text warningText;

    public void StartTimer()
    {
        StartCoroutine(WarnTimer());
    }
    IEnumerator WarnTimer()
    {
        //warningText.text = "10";
        for(int i = 10; i >= 0; i--)
        {
            warningText.text = ""+i;
            yield return new WaitForSecondsRealtime(1.0f);
        }
        
    }
}
