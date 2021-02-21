using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartCont : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public void LoadGame() {
        SceneManager.LoadScene(1);
    }
    private void Update()
    {
        
    }
}
