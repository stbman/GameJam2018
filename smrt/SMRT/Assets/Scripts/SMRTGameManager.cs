using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SMRTGameManager : MonoBehaviour {
	[Header("Gameplay")]
	public string m_MainMenuLevel;
	private CameraControl m_CameraController;
	public float m_ScorePerSuccess = 1.0f;

	// UI stuff
	[Header("UI")]
	public Text m_HappinessIndexText;
	public Text m_MoneyText;

	private float m_HappinessIndex;
	private float m_Score;

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
	//--- End of Timer based Game Mode Stuff ---
	// Use this for initialization
	void Start () {
		GameObject cameraRig = GameObject.FindGameObjectWithTag ("CameraRig");
		m_CameraController = cameraRig.GetComponent<CameraControl> ();
	}
	
	public void IncrementScore(int score, Vector3 position)
	{
		DisplayScoreFloatingText (score, position);
		if(m_SuccessfulSound)
		{
			m_SuccessfulSound.Play ();
		}
		m_Score += score;
	}

	public void DisplayScoreFloatingText(float score, Vector3 position)
	{
		//Transform tr
		string timeExtensionMessage = "+" + score.ToString() + " secs";
		FloatingTextManager.CreateFloatingText(timeExtensionMessage, position);
	}


	// Update is called once per frame
	void Update () {
		
	}
}
