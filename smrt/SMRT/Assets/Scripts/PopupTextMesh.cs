using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This component just pops up a text in 3D, and fades it in and out
public class PopupTextMesh : MonoBehaviour {
	public Color m_FadeColour;
	public float m_Scale = 1; // Scale to this size during fade time
	private bool m_Display;
	private float m_FadeTime;
	private TextMesh m_TextMeshCache;

	// Use this for initialization
	void Start () {
		m_TextMeshCache = gameObject.GetComponent<TextMesh>();
		gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		ScaleText ();
		FadeText ();
	}

	void ScaleText()
	{
		if (m_Display) {
			// Not implemented yet, Mathf.Lerp on local scale is not that straight forward
		}
	}

	void FadeText()
	{
		if (m_Display) 
		{
			m_TextMeshCache.color = Color.Lerp (m_TextMeshCache.color, m_FadeColour, m_FadeTime * Time.deltaTime);
		} 
		else
		{
			m_TextMeshCache.color = Color.Lerp (m_TextMeshCache.color, Color.clear, m_FadeTime * Time.deltaTime);
		}
	}
		
	public void PopupText(string text, Vector3 worldPosition, float duration)
	{
		m_TextMeshCache.text = text;
		gameObject.SetActive (true);
		gameObject.transform.position = worldPosition;
		m_Display = true;
		m_FadeTime = duration/2;

		Invoke ("HidePopupText", duration);
	}
		
	void HidePopupText()
	{
		m_Display = false;
		Invoke ("DeactivatePopupText", m_FadeTime);
	}

	void DeactivatePopupText()
	{
		gameObject.SetActive (false);
	}
}
