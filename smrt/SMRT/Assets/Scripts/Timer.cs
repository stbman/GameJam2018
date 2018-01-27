using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {
	private float m_StartTime = 0.0f;
	private float m_EndTime = 0.0f;
	private float m_CurrentTime = 0.0f;
	private float m_Duration = 0.0f;

	// booleans
	private bool  m_IsStarted = false;
	private bool  m_IsStopped = false;
	private bool  m_IsElapsed = false;
	public bool m_DebugOn = false;

	// progress ratio
	private float m_ProgressRatio = 0.0f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(m_IsStarted)
		{
			m_CurrentTime += Time.deltaTime;
			m_ProgressRatio = (m_CurrentTime - m_StartTime)/m_Duration;
			if(m_CurrentTime > m_EndTime)
			{
				m_IsElapsed = true;
				m_IsStarted = false;
			}

			if(m_DebugOn)
			{
				DebugDisplay();
			}
		}
	}
		
	public void StartTimer(float duration)
	{
		m_Duration = duration;
		m_IsStarted = true;
		m_IsStopped = false;
		m_IsElapsed = false;

		m_StartTime = Time.time; 
		m_CurrentTime = Time.time;
		m_EndTime = m_StartTime + duration;
	}

	public void AddTime(float duration)
	{
		m_EndTime += duration;
	}

	public void StopTimer()
	{
		m_IsStarted = false;
		m_IsStopped = true;
	}

	public bool IsElapsed()
	{
		return m_IsElapsed;
	}

	public bool IsStarted()
	{
		return m_IsStarted;
	}

	public bool IsStopped()
	{
		return m_IsStopped;
	}

	private void DebugDisplay()
	{
		print("Progress Ratio: " + m_ProgressRatio);
	}

	public float GetProgressRatio()
	{
		return m_ProgressRatio;
	}

	public float GetTimeDuration()
	{
		return m_Duration;
	}

	public float GetTimeLeft()
	{		
		return m_EndTime - m_CurrentTime;
	}
}
