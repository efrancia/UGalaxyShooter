using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuCont : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject settings;
    [SerializeField] GameObject main;
    public void LoadSPGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Settings()
    {
        settings.SetActive(true);
        main.SetActive(false);
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
