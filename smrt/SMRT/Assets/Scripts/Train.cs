using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour {

    public string m_RouteTag;
    public float m_TrainSpeed = 5.0f;
    public float m_TimeToWaitInStation = 0.0f;
    public int m_CurrentStationIndex = 1;
    public bool m_IncreaseToNextStation = true;

    private GameObject m_RouteMaster;
    private RouteScript m_RouteComp;

    private float m_DistanceTravelled;
    private float m_TimeInStation;
    // Use this for initialization
    void Start () {
        m_RouteMaster = GameObject.FindGameObjectWithTag(m_RouteTag);
        m_RouteComp = m_RouteMaster.GetComponent<RouteScript>();

        m_DistanceTravelled = 0.0f;
        m_TimeInStation = 0.0f;
        m_CurrentStationIndex = 2;
        m_IncreaseToNextStation = false;

        UpdateTrain(GetCurrentStation(), Quaternion.LookRotation(GetGoToVector()));
    }
    
    // Update is called once per frame
    void Update () {
        Vector4 goToVec        = GetGoToVector();

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

            UpdateTrain(GetCurrentStation() + (goToVec.normalized * m_DistanceTravelled), Quaternion.LookRotation(goToVec));
        }
        // wait at station
        else
        {
            m_TimeInStation += Time.deltaTime;
            if (m_TimeInStation >= m_TimeToWaitInStation)
            {
                m_DistanceTravelled = 0.0f;
                if (m_IncreaseToNextStation)
                {
                    if (m_CurrentStationIndex + 2 >= m_RouteComp.m_WayPoint.Length)
                    {
                        m_CurrentStationIndex = m_RouteComp.m_WayPoint.Length - 1;
                        m_IncreaseToNextStation = false;
                    }
                    else
                    {
                        ++m_CurrentStationIndex;
                    }
                }
                else
                {
                    if (m_CurrentStationIndex <= 1)
                    {
                        m_CurrentStationIndex = 0;
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

    private Vector4 GetCurrentStation()
    {
        return m_RouteComp.m_WayPoint[m_CurrentStationIndex].transform.position;
    }

    private Vector4 GetNextStation()
    {
        int nextStationIndex = m_IncreaseToNextStation ? m_CurrentStationIndex + 1 : m_CurrentStationIndex - 1;
        return m_RouteComp.m_WayPoint[nextStationIndex].transform.position;
    }
    private Vector4 GetGoToVector()
    {
        
        Vector4 currentStation = GetCurrentStation();
        Vector4 nextStation    = GetNextStation();
        return nextStation - currentStation;
    }

    private void UpdateTrain(Vector4 position, Quaternion rotation)
    {
        gameObject.transform.position = position;
        gameObject.transform.rotation = rotation;
    }
}
