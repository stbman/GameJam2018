using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
	public float m_DampTime = 0.2f;                 // Approximate time for the camera to refocus.
	public bool m_UseZoom = false;

	public bool m_Follow = false;

	[HideInInspector] public float m_ScreenEdgeBuffer = 4f;           // Space between the top/bottom most target and the screen edge.
	[HideInInspector] public float m_MinSize = 6.5f;                  // The smallest orthographic size the camera can be.
	[HideInInspector] public Transform[] m_Targets; // All the targets the camera needs to encompass.


	private Camera m_Camera;                        // Used for referencing the camera.
	private float m_ZoomSpeed;                      // Reference speed for the smooth damping of the orthographic size.
	private Vector3 m_MoveVelocity;                 // Reference velocity for the smooth damping of the position.
	private Vector3 m_DesiredPosition;              // The position the camera is moving towards. It will be average of all m_Targets positions
	private Vector3 m_MousePosition;
	public float m_MouseFollowSpeed = 0.1f;
	private Vector3 m_CachedStartingPosition;

	private void Awake ()
	{
		m_Camera = GetComponentInChildren<Camera> ();
		m_DesiredPosition = transform.position;
		m_CachedStartingPosition = transform.position;
	}


	private void FixedUpdate ()
	{
        Vector3 p = m_Camera.ViewportToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, m_Camera.nearClipPlane));

		DebugDisplay.Log ("Screen mouse pos: " + Input.mousePosition.ToString());
		DebugDisplay.Log ("World mouse pos: " + p.ToString());

		// Move the camera towards a desired position.
		Move ();
	}


	private void Move ()
	{
		if(!m_Follow)
			return;
		// Find the average position of the targets.
		//FindAveragePosition ();
		FindMousePosition();

		// Smoothly transition to that position.
 		transform.position = Vector3.SmoothDamp(transform.position, m_DesiredPosition, ref m_MoveVelocity, m_DampTime);
	}

	void FindMousePosition () {
        if (Input.GetMouseButton(1)) {
            m_MousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, m_Camera.nearClipPlane));
			m_DesiredPosition = Vector3.Lerp(m_DesiredPosition, new Vector3(m_MousePosition.x, m_MousePosition.y, m_CachedStartingPosition.z/2.0f), m_MouseFollowSpeed);

        }
		else
		{
			m_DesiredPosition = Vector3.Lerp(m_DesiredPosition, new Vector3(m_DesiredPosition.x, m_DesiredPosition.y, m_CachedStartingPosition.z), m_MouseFollowSpeed);
		}
    }

	void UpdateMouseScroll()
	{
		
	}


	private void FindAveragePosition ()
	{
		Vector3 averagePos = new Vector3 ();
		int numTargets = 0;

		// Go through all the targets and add their positions together.
		for (int i = 0; i < m_Targets.Length; i++)
		{
			if (m_Targets [i] == null)
				continue;
			
			// If the target isn't active, go on to the next one.
			if (!m_Targets[i].gameObject.activeSelf)
				continue;

			// Add to the average and increment the number of targets in the average.
			averagePos += m_Targets[i].position;
			numTargets++;
		}

		// If there are targets divide the sum of the positions by the number of them to find the average.
		if (numTargets > 0)
			averagePos /= numTargets;

		// Keep the same y value.
		//averagePos.y = transform.position.y;

		// The desired position is the average position;
		m_DesiredPosition = averagePos;
	}
}