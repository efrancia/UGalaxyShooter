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
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerCont>();
        anim = this.GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("no anim"); 
        }
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
            onContact();
            player.isAlive(); 
        }
        if (other.CompareTag("bullet"))
        {
            onContact();
            //anim.Play("E_explodes_anim");
            Destroy(other.gameObject);
            Destroy(this.gameObject,1.0f);
            player.AddScore(100.0f);
        }
        if (other.CompareTag("shield"))
        {
            onContact();
            //must call player components not others'
            //player = null = other.transform.GetComponent<PlayerCont>() since other is the shield not player
            player.ShieldHit();
            Destroy(this.gameObject,1.0f);
        }
        if (other.CompareTag("ExternalForce")) {
            onContact();
            Destroy(this.gameObject, 1.0f);
        }

    }
    void onContact() {
        anim.SetTrigger("onDeath");
        transform.GetComponent<BoxCollider2D>().enabled = false;
        _speed = 1.0f;
    }
}
