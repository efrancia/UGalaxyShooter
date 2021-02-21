using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpCont : MonoBehaviour
{
    // Start is called before the first frame update
    float _speed = 5.3f;
    //PuPs ID: 
    //0 = TSPU
    //1 = speed
    //2 = shields
    [SerializeField] int PuPId;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, -1, 0) * _speed * Time.deltaTime);
        if (transform.position.y < -4.0f)
        {
            Destroy(this.gameObject);   
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            PlayerCont player = other.transform.GetComponent<PlayerCont>();
            if (player != null)
            {
                switch (PuPId) {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        player.ShieldActive();
                        break;
                    default:
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }

}
