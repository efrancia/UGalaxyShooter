using System;
using System.Collections.Generic;

using System.Collections;
using UnityEngine.SocialPlatforms;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class EnemyCont : MonoBehaviour
{
    // Start is called before the first frame update
    
    float _speed = 4.0f;
    PlayerCont player;
    Animator anim;
    AudioSource DeathAudio;
    Rigidbody2D rb;
   

    //boosted enemy
    bool isBoosted=false;

    public void IsBooster(bool isit) {
        isBoosted = isit;
    }
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerCont>();
        anim = this.GetComponent<Animator>();
        if (anim == null)
        {
            UnityEngine.Debug.LogError("no anim"); 
        }
        DeathAudio = GameObject.Find("Death").GetComponent<AudioSource>();
        rb = transform.GetComponent<Rigidbody2D>();
        if (isBoosted)
        {
            ChargeBoost();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (isBoosted)
        {
            _speed = 2.0f;
            if (transform.position.y <= 1.8f)
            {
                _speed = 8.0f;
            }
        }
        
        transform.Translate(new Vector3(0, -1, 0) * _speed * Time.deltaTime);
        
        Wrapper();
    }
    void Wrapper()
    {
        if (transform.position.y < -4.0f)
        {
            if (isBoosted)
            {
                Destroy(this.gameObject);
            }
            else
            {
                transform.position = new Vector3(UnityEngine.Random.Range(-5.0f, 5.0f), 4.0f, transform.position.z);
            }
        }        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.isAlive(); 
            onContact();
        }
        if (other.CompareTag("bullet"))
        {
            
            //anim.Play("E_explodes_anim");
            Destroy(other.gameObject);
            if (isBoosted)
            {
                player.AddScore(200.0f);
            }
            else
            {
                player.AddScore(100.0f);
            }
            onContact();
        }
        if (other.CompareTag("shield"))
        {

            //must call player components not others'
            //player = null = other.transform.GetComponent<PlayerCont>() since other is the shield not player
            if (isBoosted)
            {
                player.AddScore(100.0f);
            }
            else
            {
                player.AddScore(50.0f);
            }
            player.ShieldHit();
            onContact();
        }
        if (other.CompareTag("ExternalForce")) {
            onContact();
        }

    }

    void onContact() {
        
        AudioSource newAud = Instantiate(DeathAudio);
        newAud.volume = .5f;
        newAud.pitch = .5f;
        newAud.PlayOneShot(newAud.clip);
        anim.SetTrigger("onDeath");
        //transform.GetComponent<BoxCollider2D>().enabled = false;
        _speed /= 4;
        isBoosted = false;
        Destroy(GetComponent<BoxCollider2D>());
        Destroy(this.gameObject,1.0f);
        Destroy(newAud.gameObject, 3.2f);
    }
    void ChargeBoost() {
        transform.GetComponent<SpriteRenderer>().color = Color.blue + Color.black;
    }

}
