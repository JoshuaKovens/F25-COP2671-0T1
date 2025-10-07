using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public GameObject titleScreen;
    public bool isGameActive;
    private int score;
    private float spawnRate = 1.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive) //Infinite loop
        {
            yield return new WaitForSeconds(spawnRate); //Waits for the defined spawn rate
            int index = Random.Range(0, targets.Count);//Selects a random target from the list
            Instantiate(targets[index]);//Creates the target

        }
    }

    public void UpdateScore(int scoreToAdd)//Updates the score
    {
        score += scoreToAdd; //Adds to the score
        scoreText.text = "Score: " + score; //Initializes the score display
    }

    public void GameOver() //Ends the game
    {
        restartButton.gameObject.SetActive(true); //Displays the restart button
        gameOverText.gameObject.SetActive(true); //Displays the game over text
        isGameActive = false; //Stops the game
    }

    public void RestartGame() //Restarts the game
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //Reloads the current scene
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true; //Starts the game
        score = 0;
        spawnRate /= difficulty; //Adjusts the spawn rate based on the selected difficulty 1/1 = 1, 1/2 = 0.5, 1/3 = 0.33 faster spawn rates

        StartCoroutine(SpawnTarget()); //Starts the SpawnTarget coroutine
        UpdateScore(0); //Initializes the score display

        titleScreen.gameObject.SetActive(false); //Removes the title screen
    }
}
