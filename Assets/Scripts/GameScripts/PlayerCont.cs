using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

using UnityEngine.EventSystems;

public class PlayerCont : MonoBehaviour
{
    // Start is called before the first frame update
    //controls
    [SerializeField] private float _speed = 6.0f;
    int _speedMult = 1;
    [SerializeField] float _hInput;
    [SerializeField] float _vInput;
    [SerializeField] float _zInput;
    [SerializeField] int _lives = 3;
    Animator anim;

    //Spawn
    [SerializeField] SpawnCont spawnCont;
    SpawnCont spawnMgr;

    //Fire
    [SerializeField] GameObject bullet;
    [SerializeField] AudioSource LazerAudio;
    [SerializeField] AudioSource PupAudio;

    [SerializeField] float _fireRate = 0.2f;
    [SerializeField] float _canfire = -1.0f;

    //TS
    [SerializeField] GameObject TripleShot;
    bool _tripleShotActive = false;

    //Shield
    [SerializeField] GameObject Shield;
    GameObject sh;
    int _shieldHP;
    bool _shieldActive = false;

    //speed
   
    bool _speedBoostActive = false;

    //score
    float _playerScore = 0.0f;
    [SerializeField] UiCont ui;

    //background
    [SerializeField] GameObject Bg;

   
    void Start()
    {
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        //find specific game object and assign to proper component
        //GameObject.Find(GameObject the Script is attached on).GetComponent<Script name>();
        spawnMgr = GameObject.Find("SpawnMngr").GetComponent<SpawnCont>();
        if (spawnMgr == null)
        {
            Debug.LogError("no Spawn MGR");
        }
        anim = gameObject.GetComponent<Animator>();
       
        
    }
    //[SerializeField] = allows teting controls without compromising strucure

    // Update is called once per frame
    void Update()
    {
        // Vector3.right = 1m/frame, 60m/s
        // proper structure = (new Vector3(1,1,1) * speed * real time);  
        _hInput = Input.GetAxis("Horizontal");
        _vInput = Input.GetAxis("Vertical");
        
        if (_speedBoostActive)
        {
            _speedMult = 2;
        }
        else
        {
            _speedMult = 1;
        }
        
        transform.Translate(new Vector3(_hInput, _vInput, 0) * _speed * _speedMult * Time.deltaTime);

        BGOffset();

        anim.SetFloat("playerMovement", _hInput);
        if (_lives > 0 )
        {
            Fire();
        }
        
        Wrapper();
        // check if player collider is on, life and shield life Debug.Log(this.transform.GetComponent<BoxCollider2D>().enabled+" "+_lives+_shieldHP);
        AddDifficulty();
        

    }


    void BGOffset() { 
      if ((_hInput < 0 && Bg.transform.position.x <= -0.5f) ||
            (_hInput > 0 && Bg.transform.position.x >= 0.5f)|| 
            (_vInput < 0 && Bg.transform.position.y <= -0.2f) ||
            (_vInput > 0 && Bg.transform.position.y >= 0.2f))
            {
                Bg.transform.Translate(Vector3.zero);
            }
            else {
               Bg.transform.Translate(new Vector3(_hInput, _vInput, 0) * _speed/35 * _speedMult * Time.deltaTime);
            }
    }
  

    void Wrapper()
    {
        if (transform.position.x < -5.2f)
        {
            transform.position = new Vector3(5.2f, transform.position.y, transform.position.z);
        }
        if (transform.position.x > 5.2f)
        {
            transform.position = new Vector3(-5.2f, transform.position.y, transform.position.z);
        }
        if (transform.position.y < -3.5f)
        {
            transform.position = new Vector3(transform.position.x, 3.5f, transform.position.z);
        }
        if (transform.position.y > 3.5f)
        {
            transform.position = new Vector3(transform.position.x, -3.5f, transform.position.z);
        }
    }
    public void isAlive()
    {
        /* if (_shieldActive)
         {
             if (_shieldHP <= 0)
             {
                 _shieldActive = false;
                 Destroy(sh.gameObject);
                 return;
             }
             else
             {
                 _shieldHP--;
                 return;
             }
         }
         */
        _lives--;

        if (_lives <= 0)
        {

            transform.GetComponent<BoxCollider2D>().enabled = false;
            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject child = transform.GetChild(i).gameObject;
                if (child != null)
                {
                    child.SetActive(false);
                }
            }

            _speed = 0;

            anim.SetBool("onDeath", true);
            Destroy(this.gameObject, 3.0f);
            //spawnCont.playerDeath();
            spawnMgr.playerDeath();

        }
        else
        {
            string hurt = "Hurt" + _lives.ToString();
            Animator phurt = GameObject.Find(hurt).GetComponent<Animator>();
            phurt.enabled = true;
        }
        ui.DisplayLife(_lives);

    }
    public void InstaDeath()
    {
        _lives = 0;
        isAlive();
    }
    public void TripleShotActive()
    {
        _tripleShotActive = true;
        AudioPlayer(PupAudio, 3.2f);
        StartCoroutine(PuPDuration());

    }
    public void ShieldActive()
    {

        _shieldActive = true;
        _shieldHP = 1;
        sh = Instantiate(Shield, transform.position, Quaternion.identity);
        this.transform.GetComponent<BoxCollider2D>().enabled = false;
        sh.transform.parent = this.transform;
        AudioPlayer(PupAudio, 3.2f);
        StartCoroutine(PuPDuration());

    }
    public void SpeedBoostActive()
    {
        _speedBoostActive = true;
        AudioPlayer(PupAudio, 3.2f);
        StartCoroutine(PuPDuration());
    }


    public void TurnOffPuP()
    {
        _tripleShotActive = false;
        _speedBoostActive = false;
        _shieldActive = false;
        if (sh != null) { Destroy(sh.gameObject); }
        this.transform.GetComponent<BoxCollider2D>().enabled = true;
    }
    IEnumerator PuPDuration()
    {
        yield return new WaitForSeconds(7.0f);
        TurnOffPuP();
    }
    void Fire()
    {
        
        if (Input.GetKey(KeyCode.Mouse0) && Time.time > _canfire)
        {
            _canfire = Time.time + _fireRate;
            if (_tripleShotActive)
            {
                Instantiate(TripleShot, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(bullet, transform.position, Quaternion.identity);
            }

            AudioPlayer(LazerAudio, .75f);

        }


    }

    void AudioPlayer(AudioSource aud, float audtime)
    {
        //AudioSource newAud = Instantiate(aud);
        /*newAud.PlayOneShot(newAud.clip);
         Destroy(newAud.gameObject,audtime);*/
        AudioSource.PlayClipAtPoint(aud.clip, transform.position);

    }
    public void ShieldHit()
    {


        if (_shieldActive)
        {
            if (_shieldHP <= 0)
            {
                _shieldActive = false;
                transform.GetComponent<BoxCollider2D>().enabled = true;
                Destroy(sh.gameObject);
            }
            else
            {
                _shieldHP--;
            }
        }
    }
    public void AddScore(float point)
    {
        _playerScore += point;
        ui.DisplayScore(_playerScore);

    }


    void AddDifficulty()
    {
        if (_playerScore >= 3000)
        {
            spawnMgr.setDiff(.25f);
        }
        else if (_playerScore >= 2000)
        {
            spawnMgr.setDiff(.5f);
        }
        else if (_playerScore >= 1000)
        {
            spawnMgr.setDiff(.75f);

        }
        else if (_playerScore >= 500)
        {
            spawnMgr.setDiff(1.0f);
        }
    }

    
}
    
    
//Debug.Break() pauses the application;
