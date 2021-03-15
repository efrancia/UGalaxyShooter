using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCont : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform x in this.transform)
        {
            x.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat(x.name + "Volume", .5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
