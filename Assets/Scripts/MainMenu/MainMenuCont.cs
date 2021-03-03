using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuCont : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadSPGame()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadCoOpGame()
    {
        SceneManager.LoadScene(2);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
