using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationComponent : MonoBehaviour {
	public float m_RevolutionsPerSecond = 1.0f;
	public Vector3 m_RotationAxis;
	private Vector3 m_CachedRotation;
	Transform m_CachedObjectTransform;
	// Use this for initialization
	void Start () {
		m_CachedObjectTransform = gameObject.transform;
		m_RotationAxis.Normalize();
		float rotateAngle = m_RevolutionsPerSecond * 360.0f * Time.deltaTime; // Full revolution in degrees
		m_CachedRotation = m_RotationAxis * rotateAngle;
	}
	
	// Update is called once per frame
	void Update () {
		m_CachedObjectTransform.Rotate(m_CachedRotation);
	}
}
