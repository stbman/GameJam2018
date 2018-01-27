using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour {

    public string m_RouteTag;
    public float  m_TrainSpeed            = 0.1f;
    public float  m_TimeToWaitInStation   = 1.0f;
    public int    m_CurrentStationIndex   = 0;
    public bool   m_IncreaseToNextStation = true;

    private GameObject  m_RouteMaster;
    private RouteScript m_RouteComp;

    private float m_DistanceTravelled;
    private float m_TimeInStation;
    // Use this for initialization
    void Start () {
        m_RouteMaster = GameObject.FindGameObjectWithTag(m_RouteTag);
        m_RouteComp = m_RouteMaster.GetComponent<RouteScript>();

        m_DistanceTravelled = 0.0f;
        m_TimeInStation = 0.0f;
    }
    
    // Update is called once per frame
    void Update () {
        int nextStationIndex   = m_IncreaseToNextStation ? m_CurrentStationIndex + 1 : m_CurrentStationIndex - 1;
        Vector4 currentStation = m_RouteComp.m_WayPoint[m_CurrentStationIndex].transform.position;
        Vector4 nextStation    = m_RouteComp.m_WayPoint[nextStationIndex].transform.position;
        Vector4 goToVec        = nextStation - currentStation;

        float totalDistance = goToVec.magnitude;
        // go to next station
        if (m_DistanceTravelled < totalDistance)
        {
            // TODO: stop if a train is still at next station
            
            m_DistanceTravelled += Time.deltaTime * m_TrainSpeed;
            if (m_DistanceTravelled >= totalDistance)
            {
                m_DistanceTravelled = totalDistance;
            }

            gameObject.transform.position = currentStation + (goToVec.normalized * m_DistanceTravelled);
        }
        //gameObject.transform.position = currentStation;
        // wait at station
        else
        {
            m_TimeInStation += Time.deltaTime;
            if (m_TimeInStation >= m_TimeToWaitInStation)
            {
                m_DistanceTravelled = 0.0f;
                m_TimeInStation = 0.0f;
                if (m_IncreaseToNextStation)
                {
                    if (m_CurrentStationIndex + 1 >= m_RouteComp.m_WayPoint.Length)
                    {
                        m_IncreaseToNextStation = false;
                    }
                    else
                    {
                        ++m_CurrentStationIndex;
                    }
                }
                else
                {
                    if (m_CurrentStationIndex <= 0)
                    {
                        m_IncreaseToNextStation = true;
                    }
                    else
                    {
                        --m_CurrentStationIndex;
                    }
                }
                // move to next station
            }
        }
    }
}
