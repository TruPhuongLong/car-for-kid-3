using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public delegate void EventHandler();

public class CarEngine : MonoBehaviour
{
	public Transform path;
	List<Transform> nodes;
	int currentNode = 0;
	public WheelCollider wheelFrontLeftW;
	public WheelCollider WheelFrontRightW;
	public WheelCollider WheelRearLeftW;
	public WheelCollider WheelRearRightW;

	public Transform frontLeftT, frontRightT;
	public Transform rearLeftT, rearRightT;

	public Transform headOfCar;

	public float maxSteerAngle = 40;
	public float motorForce = 50;

	public bool isStop;
	public event EventHandler carStop;

	// Start is called before the first frame update
	void Start()
	{
		
		Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
		nodes = new List<Transform>();

		// get children node
		for (int i = 0; i < pathTransforms.Length; i++)
		{
			if (pathTransforms[i] != path.transform)
			{
				nodes.Add(pathTransforms[i]);
			}
		}

	}

	private void FixedUpdate()
	{
		if (isStop)
		{
			Stop();
			return;
		}
		ApplySteer();
		Driver();
		UpdateWheelPoses();
		CheckWayPointDistance();
	}

	void Stop() {
		headOfCar.parent.GetComponent<Rigidbody>().velocity = Vector3.zero;
	}

	void ApplySteer()
	{

		//Vector3 delta = nodes[currentNode].position - headOfCar.position;

		//float newSteer = Mathf.Atan(delta.x / delta.z) * 180 / Mathf.PI;
		//Debug.Log("=== steer angle: " + newSteer);


		Vector3 relativeVector = headOfCar.InverseTransformPoint(nodes[currentNode].position);
		//float newSteer = -relativeVector.x / relativeVector.magnitude * 45;
		float newSteer = Mathf.Asin(relativeVector.x / relativeVector.magnitude) * 180 / Mathf.PI;
		wheelFrontLeftW.steerAngle = newSteer;
		WheelFrontRightW.steerAngle = newSteer;
	}

	void Driver()
	{
		wheelFrontLeftW.motorTorque = 40f * motorForce;
		WheelFrontRightW.motorTorque = 40f * motorForce;

	}

	private void UpdateWheelPoses()
	{
		UpdateWheelPose(wheelFrontLeftW, frontLeftT);
		UpdateWheelPose(WheelFrontRightW, frontRightT);
		UpdateWheelPose(WheelRearLeftW, rearLeftT);
		UpdateWheelPose(WheelRearRightW, rearRightT);
	}

	private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
	{
		Vector3 _pos = _transform.position;
		Quaternion _quat = _transform.rotation;

		_collider.GetWorldPose(out _pos, out _quat);

		_transform.position = _pos;
		_transform.rotation = _quat;
	}

	void CheckWayPointDistance()
	{
		Vector3 delta = nodes[currentNode].position - headOfCar.position;
		if (Mathf.Abs(delta.x) < 0.5f && Mathf.Abs(delta.z) < 0.5f)
		{
			Debug.Log("chang current node");
			currentNode++;
			headOfCar.parent.GetComponent<Rigidbody>().velocity *= 0.5f;

			//check stop:
			if (currentNode >= nodes.Count)
			{
				isStop = true;

				// Invoke the event.
				carStop.Invoke();
			}
		}

		//if (Vector3.Distance(nodes[currentNode].position, transform.position) < 0.05f)
		//{
		//	Debug.Log("chang current node");
		//	currentNode++;
		//}
	}

	
}
