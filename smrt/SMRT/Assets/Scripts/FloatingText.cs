using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This component destroys text after a f
public class FloatingText : MonoBehaviour {
	public Animator animator;
	private Text displayText;
	// Use this for initialization
	void OnEnable () 
	{
		if(animator)
		{
			AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
			Debug.Log("clipInfo " + clipInfo.Length);
			// Destroys the object after wainting for the animated clip length
			Destroy(gameObject, clipInfo[0].clip.length); 
			displayText = animator.GetComponent<Text>();
		}
		else{
			Debug.Log("FloatingText Component is missing a reference to an object with an animator script on it");
		}
	}
	
	public void SetText(string text)
	{
			displayText.text = text;
	}
	// Update is called once per frame
	void Update () {
		
	}
}
