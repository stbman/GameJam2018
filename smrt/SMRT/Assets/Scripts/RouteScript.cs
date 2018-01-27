using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteScript : MonoBehaviour {

    public GameObject[] m_WayPoint;

    private bool[] m_InStation;

    // Use this for initialization
    void Start () {

        m_InStation = new bool[m_WayPoint.Length];
        for (int i = 0; i < m_InStation.Length; ++i)
        {
            m_InStation[i] = false;
        }
    }
    
    // Update is called once per frame
    void Update () {
        
    }
}
