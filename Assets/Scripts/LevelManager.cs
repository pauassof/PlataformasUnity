using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI coinsText;
    [SerializeField]
    private TextMeshProUGUI livesText;
    [SerializeField]
    private GameObject pausePanel;
    public Transform spawnPoint;
    public GameObject gameOverPanel;


    // Start is called before the first frame update
    void Start()
    {
        UpdateCoins();
        UpdateLives();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu();
        }
    }
    public void PauseMenu()
    {
        if (pausePanel.activeInHierarchy == true)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        
    }
    public void ReturnMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        GameManager.instance.lives = 5;
        GameManager.instance.totalCoins = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void UpdateCoins()
    {
        coinsText.text = GameManager.instance.totalCoins.ToString();
    }

    public void UpdateLives()
    {
        livesText.text = GameManager.instance.lives.ToString();
    }
}
