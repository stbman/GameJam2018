using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuScript : MonoBehaviour {
	public string m_NormalLevelSceneName;
	public string m_TimeLevelSceneName;
	public string m_BottlesLevelSceneName;
	

	[Header("Starting Menu")]
	public Canvas m_StartCanvas;
	public Canvas m_QuitMenu;
	public Button m_StartButton;
	public Button m_ExitButton;
	public Button m_LeaderBoardButton;
	public Button m_BottlesButton;

	[Header("Game Mode")]
	public Canvas m_GameModeCanvas;
	public Button m_NormalModeButton;
	public Button m_TimeAttackModeButton;
	public Button m_BackButton;
	public AudioSource m_MenuMusic;
	public AudioSource m_ButtonPressesSound;
	public AudioClip[] m_ButtonSounds;

	// Use this for initialization
	void Start () {
		m_MenuMusic.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ExitPress()
	{
		m_QuitMenu.enabled = true;
		m_StartButton.enabled = false;
		m_ExitButton.enabled = false;
		m_BottlesButton.enabled = false;
		m_LeaderBoardButton.enabled = false;
		PlayButtonPressSound();
	}

	public void ExitMenuNoPress()
	{
		m_QuitMenu.enabled = false;
		m_StartButton.enabled = true;
		m_ExitButton.enabled = true;
		m_BottlesButton.enabled = true;
		m_LeaderBoardButton.enabled = true;
		PlayButtonPressSound();
	}

	public void GoToGameModeMenu()
	{
		m_StartCanvas.enabled = false;
		m_GameModeCanvas.enabled = true;
		PlayButtonPressSound();
	}

	public void GoToStartMenu()
	{
		m_StartCanvas.enabled = true;
		m_GameModeCanvas.enabled = false;
		PlayButtonPressSound();
	}

	public void StartNormalLevel()
	{
		m_GameModeCanvas.enabled = false;
		SceneManager.LoadScene(m_NormalLevelSceneName);
		m_MenuMusic.Stop();
		PlayButtonPressSound();
	}

	public void StartTimeModeLevel()
	{
		m_GameModeCanvas.enabled = false;
		SceneManager.LoadScene(m_TimeLevelSceneName);
		m_MenuMusic.Stop();
		PlayButtonPressSound();
	}

	public void StartBottlesGymLevel()
	{
		m_GameModeCanvas.enabled = false;
		SceneManager.LoadScene(m_BottlesLevelSceneName);
		m_MenuMusic.Stop();
		PlayButtonPressSound();
	}

	public void ExitGame()
	{
		PlayButtonPressSound();
		Application.Quit();
	}

	private void PlayButtonPressSound()
	{
		if(m_ButtonSounds.Length == 0)
			return;

		m_ButtonPressesSound.clip = m_ButtonSounds[Random.Range(0, m_ButtonSounds.Length)];
		m_ButtonPressesSound.Play();
	}
}
