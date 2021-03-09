using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CredCont : MonoBehaviour
{
    // Start is called before the first frame update
    RectTransform rt;
    int x = 0;
    int y = 0;
    
    void Start()
    {
        rt = GetComponent<RectTransform>();
        StartCoroutine(WaitBeforeRandom());

    }

    // Update is called once per frame
    void Update()
    {
        rt.Translate(new Vector3(x, y, 0) * 25.0f * Time.deltaTime);

        rt.Rotate(new Vector3(x, y, 0) * 25.0f * Time.deltaTime);

        Wrapper();
    }

    void Wrapper()
    {
        if (rt.anchoredPosition.x < -400.0f)
        {
            rt.anchoredPosition3D = new Vector3(400.0f, rt.anchoredPosition.y, 0);
            NewDirection();
        }
        if (rt.anchoredPosition.x > 400.0f)
        {
            rt.anchoredPosition3D = new Vector3(-400.0f, rt.anchoredPosition.y, 0);
            NewDirection();
        }
        if (rt.anchoredPosition.y < -220.0f)
        {
            rt.anchoredPosition3D = new Vector3(rt.anchoredPosition.x, 220.0f, 0);
            NewDirection();
        }
        if (rt.anchoredPosition.y > 220.0f)
        {
            rt.anchoredPosition3D = new Vector3(rt.anchoredPosition.x, -220.0f, 0);
            NewDirection();
        }
    }
    void NewDirection()
    {

        do
        {
            x = Random.Range(-1, 1);
            y = Random.Range(-1, 1);
        } while (x == 0 && y == 0);
        
    }
    IEnumerator WaitBeforeRandom()
    {
        yield return new WaitForSeconds(6.9f);
        NewDirection();
    }

}