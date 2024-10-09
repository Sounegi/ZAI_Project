using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    public float bulletDamage;
    private bool firstHit;
    private void Start()
    {
        firstHit = true;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.tag);

        
        //Calculate Damage
        if (collision.collider.tag == "NormalHB")
        {
            if (firstHit)
            {
                StartCoroutine(HitPlayer());
                firstHit = false;
            }
            collision.collider.GetComponentInParent<Player>().TakeDamage(bulletDamage);
        }
        else if (collision.collider.tag == "CritHB")
        {
            if (firstHit)
            {
                StartCoroutine(HitPlayer());
                firstHit = false;
            }
            collision.collider.GetComponentInParent<Player>().TakeDamage(bulletDamage*GameData.critDamageMultiplier);
        }
        else
        {
            if (firstHit)
            {
                StartCoroutine(HitObject());
                firstHit = false;
            }
            
        }
    }

    IEnumerator HitObject()
    {
        yield return new WaitForSeconds(1.0f);
        this.GetComponent<Collider2D>().isTrigger = true;
        this.GetComponent<SpriteRenderer>().color = new Color(255.0f, 255.0f, 255.0f, 125.0f);
        yield return new WaitForSeconds(3.0f);
        Destroy(this.gameObject);
    }

    IEnumerator HitPlayer()
    {
        this.GetComponent<Collider2D>().isTrigger = true;
        this.GetComponent<SpriteRenderer>().color = new Color(255.0f, 255.0f, 255.0f, 125.0f);
        yield return new WaitForSeconds(3.0f);
        Destroy(this.gameObject);
    }
}
