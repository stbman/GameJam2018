using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextManager : MonoBehaviour 
{
	private static FloatingText popUpTextPrefab;
	private static GameObject canvas;
	public static void Initialize()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
		if(!popUpTextPrefab)
		{
			popUpTextPrefab = Resources.Load<FloatingText>("Prefab/PopupTextParent");
			Debug.Log("Failed to load pop up text prefab, path should be 'Prefab/PopupTextParent'");
		}
        	
    }

    public static void CreateFloatingText(string text, Vector3 position)
    {
		if(!popUpTextPrefab)
		{
			Debug.Log("Failed to load pop up text prefab on Initialize(), path should be 'Prefab/PopupTextParent'");
			return;
		}

        FloatingText instance = Instantiate(popUpTextPrefab);
		Vector2 screenPosition = Camera.main.WorldToScreenPoint(new Vector2(position.x + Random.Range(-0.5f, 0.5f),
																position.y + Random.Range(0.0f, 0.5f)));
        instance.transform.SetParent(canvas.transform, false);
		instance.transform.position = screenPosition;
        instance.SetText(text);
    }
}

