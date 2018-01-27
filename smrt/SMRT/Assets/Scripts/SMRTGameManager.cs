using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SMRTGameManager : MonoBehaviour {
	[Header("Gameplay")]
	public string m_MainMenuLevel;
	public float m_ScorePerSuccess = 1.0f;
	public float m_DaysDuration = 30.0f;
	private int m_Days = 0;
	private int m_HighScoreDays = 0;

	// UI stuff
	[Header("UI")]
	public Text m_HappinessIndexText;
	public Text m_MoneyText;
	public Text m_GameOverText;
	public Text m_DaysText;
	private float m_HappinessIndex;
	private float m_Money;

	// GameMode
	[Header("Timer")]
	public Text m_StartingTimerText;
	public Text m_DaysTimerText;
	public float m_StartingTimeDuration = 3.0f;
	public Timer m_StartingTimer; // Count down timer to the start of game
	public Timer m_DaysTimer; // Count down timer to end of day
	// Sound
	[Header("Sound")]
	public AudioSource m_InGameBackgroundMusic;
	public AudioSource m_SuccessfulSound;
	public AudioSource m_CountdownTimerSource;
	public AudioSource m_FailSound;

	// GameMode
	[Header("Game Mode Properties")]
	public Text m_WinText;
	public bool m_IsGameOver;
	public bool m_IsRestarting = false;
	// Gameflow 
	private bool m_LevelStarted = false;
	// Use this for initialization
	void Start () {

		Init();
	}

	void Init()
	{
		m_HappinessIndex = 0.0f;
		m_Money = 0.0f;
		m_Days = 0;
		m_StartingTimer.StartTimer(m_StartingTimeDuration);
	}

	void RestartLevel()
	{
		m_IsRestarting = true;
		m_LevelStarted = false;
		Init();
	}

	void StartGame()
	{
		m_LevelStarted = true;
		m_IsRestarting = false;
		m_IsGameOver = false;
		SetUITextSafely(m_StartingTimerText, "");
		SetUITextSafely(m_GameOverText, "");
		m_DaysTimer.StartTimer(m_DaysDuration);
	}

	void GameOver()
	{
		m_IsGameOver = true;
		SetUITextSafely(m_GameOverText, "Game Over! Tap to Restart!");
	}
	
	public void IncrementHappiness(float happiness)
	{
		m_HappinessIndex += happiness;
	}

	public void IncrementMoney(int money, Vector3 position)
	{
		DisplayMoneyFloatingText (money, position);
		// if(m_SuccessfulSound)
		// {
		// 	m_SuccessfulSound.Play ();
		// }
		m_Money += money;
	}

	public void DisplayMoneyFloatingText(float money, Vector3 position)
	{
		//Transform tr
		string moneyMessage = "+" + money.ToString() + " $";
		FloatingTextManager.CreateFloatingText(moneyMessage, position);
	}


	// Update is called once per frame
	void Update () {
		GameFlowUpdate();
	}

	void GameFlowUpdate()
	{
		if (!m_LevelStarted && m_StartingTimer.IsStarted ()) {
			// Restrict input, no use case for now
		}

		if(!m_LevelStarted && m_StartingTimer.IsElapsed())
		{
			StartGame();
		}

		if(m_HappinessIndex < 0.0f)
		{
			GameOver();
		}

		if(m_IsGameOver)
		{
			if(!m_IsRestarting && (Input.GetMouseButtonDown(0) || Input.touchCount > 0))
			{
				RestartLevel();
			}
		}

		UpdateUI();
		UpdateTimers();
	}

	private void SetUITextSafely(Text textObject, string uiText)
	{
		if(textObject)
		{
			textObject.text = uiText;
		}
	}

	void UpdateTimers()
	{
		if(!m_StartingTimer.IsElapsed())
		{
			SetUITextSafely(m_StartingTimerText, Mathf.Round(m_StartingTimer.GetTimeLeft()).ToString());
		}

		if(m_DaysTimer.IsElapsed())
		{
			m_Days++;
			m_DaysTimer.StartTimer(m_DaysDuration);
		}

		if(!m_DaysTimer.IsElapsed())
		{
			SetUITextSafely(m_DaysTimerText, "Time Left: " + Mathf.Round(m_DaysTimer.GetTimeLeft()).ToString());
		}
	}

	void UpdateUI()
	{
		SetUITextSafely(m_HappinessIndexText, m_HappinessIndex.ToString());
		SetUITextSafely(m_MoneyText, "$ " + m_Money.ToString());
		SetUITextSafely(m_DaysText, "Day: " + m_Days.ToString());
	}
}
