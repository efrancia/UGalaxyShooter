using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiCont : MonoBehaviour
{
    // Start is called before the first frame update
    
    
    PlayerCont player;
    Text _scoreText;
    [SerializeField]Sprite[] _lives;
    [SerializeField] Image hp;
    [SerializeField] GameObject gameOver;
   
    bool _playerDead;
    void Start()
    {
        //GameObject.Find() only works if GO is in the world *hint,hint* prefabs
        _scoreText = GameObject.Find("Score").GetComponent<Text>();
        player = GameObject.Find("Player").GetComponent<PlayerCont>();
        _scoreText.text = "Score : ";
    }

    // Update is called once per frame
    void Update()
    {
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
        _scoreText.text = "Score : " + pScore;
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

}
