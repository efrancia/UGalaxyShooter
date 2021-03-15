using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiCont : MonoBehaviour
{
    // Start is called before the first frame update
    
    
    [SerializeField] PlayerCont player;
    Text _scoreText;
    [SerializeField]Sprite[] _lives;
    [SerializeField] Image hp;
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject Pause;
    [SerializeField] GameObject settings;
    [SerializeField] GameObject HScore;
    
    bool _playerDead;
    void Start()
    {
        
        settings.GetComponent<SettingsCont>().CreateDefaultVolume();

        //GameObject.Find() only works if GO is in the world *hint,hint* prefabs
        if (PlayerPrefs.HasKey("_highScore"))
        {
            HScore.GetComponent<Text>().text = "High Score: " + PlayerPrefs.GetFloat("_highScore");
        }
        else
        {
            PlayerPrefs.SetFloat("_highScore", 0);
        }
        _scoreText = GameObject.Find("Score").GetComponent<Text>();
        _scoreText.text = "Score: ";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space)&&!_playerDead)
        {
            PauseMenu();
        }
        if (Input.GetKey(KeyCode.Escape)&&!_playerDead&&Pause.activeSelf)
            {
            ResumeGame();
            }
        if (_playerDead)
        {
            GameManager();
        }
    }
    public void DisplayLife(int lives) {
        if (lives <= 0)
        {
            hp.sprite = _lives[0];
            //Coroutines initiated in the same class to loop, it also cannot be initiated in the Update method
            // if Cr's are initiated at Start, it would run first THEN the update loop, so Cr's hardly coexist with Update Method
            StartCoroutine(GameOver());
        }
        else
        {
            hp.sprite = _lives[lives];
        }
    }
    public void DisplayScore(float pScore) {
        
        _scoreText.text = "Score: " + pScore;
        if (pScore>= PlayerPrefs.GetFloat("_highScore"))
        {
            PlayerPrefs.SetFloat("_highScore", pScore);
            HScore.GetComponent<Text>().text = "High Score: "+ PlayerPrefs.GetFloat("_highScore");
        }
    }
    
    IEnumerator GameOver() {
        
        _playerDead = true;
        while (_playerDead) {
             gameOver.SetActive(true);
             yield return new WaitForSeconds(1.0f);
             gameOver.SetActive(false);
             yield return new WaitForSeconds(1.0f);
        }

     }
    void GameManager() { 

     if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene(1);

        }
     if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }
    }
    void PauseMenu() {
        Pause.SetActive(true);
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        Pause.SetActive(false);
        Time.timeScale = 1;
    }
    public void Settings()
    {
        settings.SetActive(true);
        Pause.SetActive(false);
    }
    public void QuitGame() {
        Application.Quit();
    }
    

}
