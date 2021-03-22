using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
// Added to Game Manager Object 
public class GameManager : MonoBehaviour
{

    private int score;
    public TextMeshProUGUI scoreText;
    public bool isGameActive;
    public TextMeshProUGUI gameOverText; // add Game Over text, disable object
    public Button restartButton;
    // list accept type
    //public List<GameObject> targets; 

    // 1 sec spawn time
    //private float spawnRate = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        isGameActive = true;
        score = 0;
        //StartCoroutine(SpawnTarget());
        UpdateScore(0);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //IEnumerator SpawnTarget() {

    //    while(isGameActive)
    //    {
    //    yield return new WaitForSeconds(spawnRate);
    //    int index = Random.Range(0, targets.Count);
    //    Instantiate(targets[index]);
        
    //    }

    //}
    public void UpdateScore(int scoreToAdd) {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver() {
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame() {
       
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
