using System;
using System.Collections.Generic;

using System.Collections;
using UnityEngine.SocialPlatforms;
using UnityEngine;

public class ExternalCont : MonoBehaviour
{
    // Start is called before the first frame update
    //IMPROPER CODE

    PlayerCont player;
    [SerializeField] GameObject Shotgun;
    Animator anim;
    GameObject newShotty;
    float _speed = 1.5f;
    bool _isMainAster =true;
    
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerCont>();
        anim = GetComponent<Animator>();
        if (!_isMainAster) {
            Destroy(transform.parent.parent.gameObject,6.0f);
            _speed = 4.5f;
        }
        else
        {
            _speed = 1.5f;
        }
    }
    public void IsMainAster(bool ima) {
        _isMainAster = ima;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 45) * _speed * Time.deltaTime);
        transform.parent.transform.Translate(new Vector3(0, -1, 0) * _speed  * Time.deltaTime);
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.InstaDeath();
        }
        if (other.CompareTag("bullet"))
        {
            if (_isMainAster)
            {
                _speed = 1.0f;
                transform.GetComponent<CapsuleCollider2D>().enabled = false;
                newShotty = Instantiate(Shotgun, transform.position, Quaternion.identity);
                ExternalCont[] ec = newShotty.transform.GetComponentsInChildren<ExternalCont>();
                foreach (ExternalCont all in ec)
                {
                    all.IsMainAster(false);
                }
                anim.SetTrigger("mainAsterDead");
                Destroy(other);
                Destroy(transform.parent.gameObject, 2.0f);
            }
            else {
                Destroy(other);
            }
        }

        if (other.CompareTag("shield"))
        {
            player.ShieldHit();
            Destroy(other.gameObject);
        }

    }
    

}
