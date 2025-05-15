using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
   [SerializeField] TextMeshProUGUI livesText; 
   [SerializeField] TextMeshProUGUI scoreText; 

    [SerializeField] float playerLives=3f;
    [SerializeField] float score=0f;
    void Awake()     //starttan daha önce çalışır
    {
        int numberGameSessions=FindObjectsOfType<GameSession>().Length;
        if(numberGameSessions>1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }
    void Start()
    {
        livesText.text=playerLives.ToString();  //int olan playerlivesı stirnge çevirip score textine ata
        scoreText.text=score.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives>1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }
    public void AddToScore(int pointsToAdd)
    {
        score+=pointsToAdd;
        scoreText.text=score.ToString();
    }
    void TakeLife()
    {
        playerLives--;
        int currentSceneIndex=SceneManager.GetActiveScene().buildIndex; //aktif sahnenin build indeksi currentSceneIndex olsun
        SceneManager.LoadScene(currentSceneIndex);  //currentScene sahnesini yükle
        livesText.text=playerLives.ToString();

    }
    void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
