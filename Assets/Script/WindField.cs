using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindField : MonoBehaviour
{
    private Rigidbody2D affectBullet;
    private bool windapply = false;
    public Vector2 windSpeed;
    [SerializeField] Scrollbar bar;
    //private float maxWinSpeed = 3.5f;
    public void GenerateWind()
    {
        float wind = (float)(int) Random.Range(-GameData.maxWindSpeed, GameData.maxWindSpeed);
        windSpeed = new Vector2(wind, 0.0f);
        bar.value = (GameData.maxWindSpeed + wind -0.5f)/(2* (GameData.maxWindSpeed- 0.5f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Rigidbody2D>() != null)
        {
            affectBullet = collision.GetComponent<Rigidbody2D>();
            windapply = true;
        }
    }

    private void FixedUpdate()
    {
        if (windapply)
        {
            if(affectBullet!=null)
                affectBullet.AddForce(windSpeed,ForceMode2D.Force);
        }
        
    }
}
