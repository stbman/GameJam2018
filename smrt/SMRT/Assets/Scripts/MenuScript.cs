using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuScript : MonoBehaviour {
	public string m_NormalLevelSceneName;
	

	[Header("Starting Menu")]
	public Canvas m_StartCanvas;
	public Button m_StartButton;
	public Button m_ExitButton;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GoToGameModeMenu()
	{
		m_StartCanvas.enabled = false;
	}

	public void GoToStartMenu()
	{
		m_StartCanvas.enabled = true;
	}

	public void StartNormalLevel()
	{
		if(m_StartCanvas)
		{
			m_StartCanvas.enabled = false;
		}
		SceneManager.LoadScene(m_NormalLevelSceneName);
	}

	public void ExitGame()
	{
		Application.Quit();
	}

	// private void PlayButtonPressSound()
	// {
	// 	if(m_ButtonSounds.Length == 0)
	// 		return;

	// 	m_ButtonPressesSound.clip = m_ButtonSounds[Random.Range(0, m_ButtonSounds.Length)];
	// 	m_ButtonPressesSound.Play();
	// }
}
