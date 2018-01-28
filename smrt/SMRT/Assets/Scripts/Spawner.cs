using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
    public GameObject m_Prefab;
    public float m_MinSpawnCooldown;
    public float m_MaxSpawnCooldown;
    public string m_RouteTag;
    public int m_MaxTrain = 20;
    public float m_TrainSpeedd = 0.1f;
    public float m_EachDayDecreaseInTime = 1.0f;

    private Timer m_Timer;
    private bool m_Collided;
    public int m_SpawnedTrain = 0;
    private SMRTGameManager m_GameManager;
    // Use this for initialization
    void Awake () {
    }

    void Start()
    {
        m_Timer = GetComponent<Timer>();
        m_Timer.StartTimer(Random.Range(m_MinSpawnCooldown, m_MaxSpawnCooldown));
        m_Collided = false;
        m_SpawnedTrain = 0;
        m_GameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SMRTGameManager>();
    }

    public void ResetSpawner()
    {
        m_Timer = GetComponent<Timer>();
        m_Timer.StartTimer(Random.Range(m_MinSpawnCooldown, m_MaxSpawnCooldown));
    }
    
    // Update is called once per frame
    void Update () {
        if(m_GameManager && (   !m_GameManager.m_LevelStarted
                             || m_GameManager.m_IsGameOver
                             || m_GameManager.m_DaysTimer.IsElapsed()))
        {
            return;
        }
        
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
                trainLogic.m_LineSpawner = this;
                ++m_SpawnedTrain;
            }

            float coolDownReduction = m_EachDayDecreaseInTime * (float)(m_GameManager.m_Days - 1);
            float minSpawn = m_MinSpawnCooldown - coolDownReduction;
            float maxSpawn = m_MaxSpawnCooldown - coolDownReduction;
            if (minSpawn < 1.0f)
            {
                minSpawn = 1.0f;
            }
            if (maxSpawn < 1.0f)
            {
                maxSpawn = 1.0f;
            }
            m_Timer.StartTimer(Random.Range(minSpawn, maxSpawn));
        }

        m_Collided = false;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Train>())
        m_Collided = true;
    }
}