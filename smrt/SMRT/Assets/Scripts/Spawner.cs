using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
    public GameObject m_Prefab;
    public float m_MinSpawnCooldown;
    public float m_MaxSpawnCooldown;
    public string m_RouteTag;
    public int m_MaxTrain = 20;
    public float m_TrainSpeedd = 0.1f;

    private Timer m_Timer;
    private bool m_Collided;
    private int m_SpawnedTrain = 0;
    // Use this for initialization
    void Awake () {
    }

    void Start()
    {
        m_Timer = GetComponent<Timer>();
        m_Timer.StartTimer(Random.Range(m_MinSpawnCooldown, m_MaxSpawnCooldown));
        m_Collided = false;
        m_SpawnedTrain = 0;
    }

    public void ResetSpawner()
    {
        m_Timer = GetComponent<Timer>();
        m_Timer.StartTimer(Random.Range(m_MinSpawnCooldown, m_MaxSpawnCooldown));
    }
    
    // Update is called once per frame
    void Update () {

        
        if(     m_Timer.IsElapsed()
            && !m_Collided
            && m_SpawnedTrain <= m_MaxTrain)
        {
            GameObject newTrain = Instantiate(m_Prefab, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
            Train trainLogic = newTrain.GetComponent<Train>();
            if (trainLogic)
            {
                trainLogic.m_RouteTag = m_RouteTag;
                trainLogic.m_TrainSpeed = m_TrainSpeedd;
                ++m_SpawnedTrain;
            }

            m_Timer.StartTimer(Random.Range(m_MinSpawnCooldown, m_MaxSpawnCooldown));
        }

        m_Collided = false;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Train>())
        m_Collided = true;
    }
}