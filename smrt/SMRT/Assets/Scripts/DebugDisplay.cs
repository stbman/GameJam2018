using UnityEngine;
using System.Collections;

public static class DebugDisplay
{
    public static void Log(string logText)
    {
	#if UNITY_EDITOR
	Debug.Log(logText);
	#elif UNITY_ANDROID || UNITY_IOS
	// Don't do any logging here
	#endif
    }

	public static void LogWarning(string logText)
    {
	#if UNITY_EDITOR
	Debug.LogWarning(logText);
	#elif UNITY_ANDROID || UNITY_IOS
	// Don't do any logging here
	#endif
    }


 	public static void LogError(string logText)
    {
	#if UNITY_EDITOR
	Debug.LogError(logText);
	#elif UNITY_ANDROID || UNITY_IOS
	// Don't do any logging here
	#endif
    }


    public static void ForGizmo (Vector3 pos, Vector3 direction, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
	{
		Gizmos.DrawRay (pos, direction);
		DrawArrowEnd(true, pos, direction, Gizmos.color, arrowHeadLength, arrowHeadAngle);
	}

	public static void ForGizmo (Vector3 pos, Vector3 direction, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
	{
		Gizmos.DrawRay (pos, direction);
		DrawArrowEnd(true, pos, direction, color, arrowHeadLength, arrowHeadAngle);
	}

	public static void ForDebug (Vector3 pos, Vector3 direction, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
	{
		Debug.DrawRay (pos, direction);
		DrawArrowEnd(false, pos, direction, Gizmos.color, arrowHeadLength, arrowHeadAngle);
	}

	public static void ForDebug (Vector3 pos, Vector3 direction, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
	{
		Debug.DrawRay (pos, direction, color);
		DrawArrowEnd(false, pos, direction, color, arrowHeadLength, arrowHeadAngle);
	}

	private static void DrawArrowEnd (bool gizmos, Vector3 pos, Vector3 direction, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
	{
		Vector3 right = Quaternion.LookRotation (direction) * Quaternion.Euler (arrowHeadAngle, 0, 0) * Vector3.back;
		Vector3 left = Quaternion.LookRotation (direction) * Quaternion.Euler (-arrowHeadAngle, 0, 0) * Vector3.back;
		Vector3 up = Quaternion.LookRotation (direction) * Quaternion.Euler (0, arrowHeadAngle, 0) * Vector3.back;
		Vector3 down = Quaternion.LookRotation (direction) * Quaternion.Euler (0, -arrowHeadAngle, 0) * Vector3.back;
		if (gizmos) {
			Gizmos.color = color;
			Gizmos.DrawRay (pos + direction, right * arrowHeadLength);
			Gizmos.DrawRay (pos + direction, left * arrowHeadLength);
			Gizmos.DrawRay (pos + direction, up * arrowHeadLength);
			Gizmos.DrawRay (pos + direction, down * arrowHeadLength);
		} else {
			Debug.DrawRay (pos + direction, right * arrowHeadLength, color);
			Debug.DrawRay (pos + direction, left * arrowHeadLength, color);
			Debug.DrawRay (pos + direction, up * arrowHeadLength, color);
			Debug.DrawRay (pos + direction, down * arrowHeadLength, color);
		}
	}

	public static Vector3 PlotTrajectoryAtTime (Vector3 start, Vector3 startVelocity, Vector3 acceleration, float time)
	{
		return start + startVelocity * time + acceleration * time * time * 0.5f;
	}

	public static void PlotTrajectory (Vector3 start, Vector3 startVelocity,  Vector3 acceleration, float timestep, float maxTime, Color color) 
	{
		Vector3 prev = start;
		for (int i=1;;i++) 
		{
			float t = timestep * i;
			if (t > maxTime) 
				break;

			Vector3 pos = PlotTrajectoryAtTime (start, startVelocity, acceleration, t);
			if (Physics.Linecast (prev,pos)) 
				break;
			Debug.DrawLine (prev,pos, color);
			prev = pos;
		}
	}
}
