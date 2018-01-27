using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour {

    public string m_RouteTag;
    public float m_TrainSpeed = 1.0f;
    public float m_TimeToWaitInStation = 0.0f;
    public int m_CurrentStationIndex = 0;
    public bool m_IncreaseToNextStation = true;

    private GameObject  m_RouteMaster;
    private RouteScript m_RouteComp;
    private Renderer    m_Renderer;
    private float m_DistanceTravelled;
    private float m_TimeInStation;
    private bool m_Collidied;
    private Collider m_CollidiedWith;

    // Use this for initialization
    void Start () {
        m_RouteMaster = GameObject.FindGameObjectWithTag(m_RouteTag);
        m_RouteComp = m_RouteMaster.GetComponent<RouteScript>();
        m_Renderer = gameObject.GetComponent<Renderer>();

        m_DistanceTravelled = 0.0f;
        m_TimeInStation = 0.0f;
        m_CurrentStationIndex = 0;
        m_IncreaseToNextStation = true;
        m_Collidied = false;

        UpdateTrain(GetCurrentStation(), GetGoToVector());
    }
    
    // Update is called once per frame
    void Update () {
        Vector4 goToVec        = GetGoToVector();

        float totalDistance = goToVec.magnitude;
        // go to next station
        if (m_DistanceTravelled < totalDistance)
        {
            if (!m_Collidied)
            {
                m_DistanceTravelled += Time.deltaTime * m_TrainSpeed;
                if (m_DistanceTravelled >= totalDistance)
                {
                    m_DistanceTravelled = totalDistance;
                }

                UpdateTrain(GetCurrentStation() + (goToVec.normalized * m_DistanceTravelled), goToVec);
            }
        }
        // wait at station
        else
        {
            m_TimeInStation += Time.deltaTime;
            if (   m_TimeInStation >= m_TimeToWaitInStation
                || NeedToWaitAtStation())
            {
                m_DistanceTravelled = 0.0f;
                m_TimeInStation = 0.0f;

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

    void OnTriggerEnter(Collider other)
    {
        //Renderer render = GetComponent<Renderer>();
        //render.material.color

        Train otherTrain = other.GetComponent<Train>();
        
        if (   otherTrain
            && otherTrain.m_RouteTag == m_RouteTag)
        {
            //int nextStationIndex = GetNextStationIndex();
            if (otherTrain.m_CurrentStationIndex == m_CurrentStationIndex
                && m_DistanceTravelled < GetGoToVector().magnitude
                && !m_Collidied)
            {
                m_Renderer.material.color = new Color(1.0f, 0.0f, 0.0f);
                m_Collidied = true;
                m_CollidiedWith = other;
            }
        }

        // reduce happiness
    }

    void OnTriggerExit(Collider other)
    {
        if (other == m_CollidiedWith)
        {
            m_Collidied = false;
            m_Renderer.material.color = new Color(1.0f, 1.0f, 1.0f);
        }
    }

    void OnMouseDown()
    {
        GameObject.Destroy(gameObject);

        // if not break down,
    }

    private int GetNextStationIndex()
    {
        return m_IncreaseToNextStation ? m_CurrentStationIndex + 1 : m_CurrentStationIndex - 1;
    }

    private Vector4 GetCurrentStation()
    {
        if (m_CurrentStationIndex < 0
            || m_CurrentStationIndex > m_RouteComp.m_WayPoint.Length)
        {
            int i = 0;
            int x = i + 10;
        }
        return m_RouteComp.m_WayPoint[m_CurrentStationIndex].transform.position;
    }

    private bool NeedToWaitAtStation()
    {
        return m_RouteComp.m_WayPoint[GetNextStationIndex()].tag == "NotAStation";
    }
    private Vector4 GetNextStation()
    {
        return m_RouteComp.m_WayPoint[GetNextStationIndex()].transform.position;
    }
    private Vector4 GetGoToVector()
    {
        
        Vector4 currentStation = GetCurrentStation();
        Vector4 nextStation    = GetNextStation();
        return nextStation - currentStation;
    }

    private void UpdateTrain(Vector4 position, Vector4 lookAt)
    {
        Quaternion rotation = Quaternion.LookRotation(lookAt);
        Vector3 upVec = new Vector3(0.0f, 0.0f, 1.0f);
        Vector3 rightVec = Vector3.Cross(upVec, lookAt.normalized) *0.02f;
        Vector3 v3Position = position;
        gameObject.transform.position = (gameObject.transform.position * 0.9f) + ((v3Position + rightVec) * 0.1f);
        gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, rotation, 0.1f);
    }
}
