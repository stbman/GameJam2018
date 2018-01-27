using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	public GameObject m_Prefab;
	public float m_MinSpawnCooldown;
	public float m_MaxSpawnCooldown;
    public string m_RouteTag;

	private Timer m_Timer;

	// Use this for initialization
	void Awake () {
	}

	void Start()
	{
		m_Timer = GetComponent<Timer>();
		m_Timer.StartTimer(Random.Range(m_MinSpawnCooldown, m_MaxSpawnCooldown));
	}

	public void ResetSpawner()
	{
		m_Timer = GetComponent<Timer>();
		m_Timer.StartTimer(Random.Range(m_MinSpawnCooldown, m_MaxSpawnCooldown));
	}
	
	// Update is called once per frame
	void Update () {

		
		if(m_Timer.IsElapsed())
		{
			GameObject newTrain = Instantiate(m_Prefab, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
            Train trainLogic = newTrain.GetComponent<Train>();
            if (trainLogic)
            {
                trainLogic.m_RouteTag = m_RouteTag;
            }
			m_Timer.StartTimer(Random.Range(m_MinSpawnCooldown, m_MaxSpawnCooldown));
		}
	}
}