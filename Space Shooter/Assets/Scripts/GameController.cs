using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public int lives;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	
	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;
	public GUIText highScoreText;
	public GUIText lifeText;
	
	private int score;
	private int hiScore;
	private int waveCount = 1;
	private bool gameOver;
	private bool restart;
	
	void Start ()
	{
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		hiScore = PlayerPrefs.GetInt("highscore");
		lives = PlayerPrefs.GetInt("Life");
		highScoreText.text = "High Score: " + hiScore;
		lifeText.text = "Lives: " + lives;
		score = 0;
		UpdateScore();
		StartCoroutine( SpawnWaves());
	}
	
	void Update ()
	{
		if(restart)
		{
			if (Input.GetKeyDown(KeyCode.R))
			{
			    if(score > hiScore)
			    {
			    	PlayerPrefs.SetInt("highscore",score);
				}
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	}
	
	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		
		while(true)
		{
	   
			for (int i = 0 ;i< hazardCount ;i++ )
			{
				if (i==2)
				{
					gameOverText.text = "";
				}
				GameObject hazard = hazards[Random.Range(0,hazards.Length)];
				Vector3 spawnPosition = new Vector3( Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
			hazardCount = hazardCount +5;
			waveCount += 1;
			
			
			if (gameOver)
			{
				
				restartText.text = "Press 'R' for Restart";
				restart = true;
				break;
			}
			gameOverText.text = "Wave " + waveCount ;
		}
	}
	
	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore();
	
	}
	
	void UpdateScore ()
	{
	
		scoreText.text = "Score: " + score;
	}
	
	public void GameOver()
	{
	   lives= lives -1;
	   
		
	   if( lives > 0)
	   {
	      gameOver = false;
			PlayerPrefs.SetInt("Life",lives);
			Application.LoadLevel(Application.loadedLevel);
	   }
	   else{
			lifeText.text = "Lives: " + lives;
			PlayerPrefs.SetInt("Life",3);
			gameOverText.text = "Game Over!";
			gameOver = true;
			}
	}
	
}
