using System;
using System.Collections.Generic;

using System.Collections;
using UnityEngine.SocialPlatforms;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCont : MonoBehaviour
{
    // Start is called before the first frame update
    
    float _speed = 4.0f;
    PlayerCont player;
    Animator anim;
    AudioSource DeathAudio;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerCont>();
        anim = this.GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("no anim"); 
        }
        DeathAudio = GameObject.Find("Death").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(new Vector3(0, -1, 0) * _speed * Time.deltaTime);
        
        Wrapper();
    }
    void Wrapper()
    {
        if (transform.position.y < -4.0f)
        {
            transform.position = new Vector3(UnityEngine.Random.Range(-5.0f,5.0f), 4.0f, transform.position.z);
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
            player.AddScore(100.0f);
            onContact();
        }
        if (other.CompareTag("shield"))
        {
            
            //must call player components not others'
            //player = null = other.transform.GetComponent<PlayerCont>() since other is the shield not player
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
        transform.GetComponent<BoxCollider2D>().enabled = false;
        _speed = 1.0f;
        Destroy(this.gameObject,1.0f);
        Destroy(newAud.gameObject, 3.2f);
    }
}
