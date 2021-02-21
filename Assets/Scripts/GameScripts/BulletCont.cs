using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public class BulletCont : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField]
    float _speed = 3.0f;
    GameObject ts;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0,1,0) * _speed *Time.deltaTime);
        Destroy();
    }
    private void Destroy()
    {
        if (transform.position.y > 4.0f)
        {

            if (this.transform.parent != null)
            {
                Destroy(this.transform.parent.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
