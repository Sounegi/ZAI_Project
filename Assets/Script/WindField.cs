using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindField : MonoBehaviour
{
    private Rigidbody2D affectBullet;
    private bool windapply = false;
    public Vector2 windSpeed;
    private float maxWinSpeed = 5.0f;

    public void GenerateWind()
    {
        windSpeed = new Vector2(Random.Range(-maxWinSpeed, maxWinSpeed), 0.0f);
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
            affectBullet.AddForce(windSpeed,ForceMode2D.Force);
        }
        
    }
}
