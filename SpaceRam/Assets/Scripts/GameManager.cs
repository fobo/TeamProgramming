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
    public TextMeshProUGUI scoreTextStatus;
    public TextMeshProUGUI gameOverText; // add Game Over text, disable object
    public TextMeshProUGUI highScoresWeb; 
    public Button restartButton;
    public Button submitButton;
    public InputField playerName;
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
        playerName.gameObject.SetActive(true);
        submitButton.gameObject.SetActive(true);
        highScoresWeb.gameObject.SetActive(true);
        isGameActive = false;
    }
    public void CallSaveData() {
        StartCoroutine(SavePlayerData());
    }

    IEnumerator SavePlayerData() {

        WWWForm form = new WWWForm();
        //form.AddField("id", null );
        form.AddField("playerName", playerName.text);
        form.AddField("score", score);

        WWW www = new WWW("http://206.189.198.175/SpaceRam/savedata.php", form);
        yield return www;
        if(www.text == "0") {
            Debug.Log("Game Saved.");
            scoreTextStatus.text = "Score Submitted: " + playerName.text + " " + score;
            scoreTextStatus.gameObject.SetActive(true);
        } else {
            Debug.Log("Save failed. Error #" + www.text);
        }
        // UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    public void RestartGame() {
       
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
