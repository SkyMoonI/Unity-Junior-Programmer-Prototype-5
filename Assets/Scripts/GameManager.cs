using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
	[SerializeField] List<GameObject> targets;
	float spawnRate = 1.0f;

	[SerializeField] TextMeshProUGUI gameOverText;
	[SerializeField] Button restartButton;
	[SerializeField] TextMeshProUGUI scoreText;
	[SerializeField] GameObject titleScreen;
	int score = 0;
	[SerializeField] TextMeshProUGUI livesText;
	public int lives = 3;

	public bool isGameActive = false;

	[Header("Pause Screen")]
	[SerializeField] GameObject pauseScreen;
	public bool isGamePaused = false;

	// Start is called before the first frame update
	void Start()
	{
		livesText.text = "Lives: " + lives.ToString();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			PauseGame();
		}
	}

	IEnumerator SpawnTarget()
	{
		while (isGameActive && !isGamePaused)
		{
			yield return new WaitForSeconds(spawnRate);
			int index = Random.Range(0, targets.Count);
			Instantiate(targets[index]);
		}
	}

	public void UpdateScore(int scoreToAdd)
	{
		score += scoreToAdd;
		scoreText.text = "Score: " + score.ToString();

	}
	public void UpdateLives(int livesToSubtract)
	{
		lives -= livesToSubtract;
		livesText.text = "Lives: " + lives.ToString();

	}
	public void GameOver()
	{
		gameOverText.gameObject.SetActive(true);
		restartButton.gameObject.SetActive(true);
		isGameActive = false;
	}

	public void RestartGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void StartGame(int difficulty)
	{
		spawnRate /= difficulty;
		isGameActive = true;
		score = 0;
		UpdateScore(0);
		StartCoroutine(SpawnTarget());
		titleScreen.gameObject.SetActive(false);
	}
	void PauseGame()
	{
		if (!isGamePaused)
		{
			isGamePaused = true;
			Time.timeScale = 0;
			pauseScreen.gameObject.SetActive(true);
		}
		else
		{
			isGamePaused = false;
			Time.timeScale = 1;
			pauseScreen.gameObject.SetActive(false);
		}


	}
}
