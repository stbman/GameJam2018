using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour {

    public string m_RouteTag;
    public int m_CurrentStationIndex = 1;
    public int m_NextStationIndex = 2;

    private GameObject m_RouteMaster;
    private RouteScript m_RouteComp;
    private Rigidbody m_RigidBody;
    private float m_TimeElasped;
    // Use this for initialization
    void Start () {
        m_RigidBody = gameObject.GetComponent<Rigidbody>();
        m_RouteMaster = GameObject.FindGameObjectWithTag(m_RouteTag);
        m_RouteComp = m_RouteMaster.GetComponent<RouteScript>();

        m_TimeElasped = 0.0f;
    }
    
    // Update is called once per frame
    void Update () {
        Vector4 currentStation = m_RouteComp.m_WayPoint[m_CurrentStationIndex].transform.position;
        Vector4 nextStation = m_RouteComp.m_WayPoint[m_NextStationIndex].transform.position;
        m_TimeElasped += Time.deltaTime;

        Vector4 newPosition = currentStation + ((nextStation - currentStation) * m_TimeElasped);

        gameObject.transform.position = newPosition;
    }
}
