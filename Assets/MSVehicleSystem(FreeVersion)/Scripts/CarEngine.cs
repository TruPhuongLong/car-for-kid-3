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
	public event EventHandler carHorn;

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
		CheckWayToHorn();
	}

	void Stop() {
		headOfCar.parent.GetComponent<Rigidbody>().velocity = Vector3.zero;
	}

	void ApplySteer()
	{
		Vector3 relativeVector = headOfCar.InverseTransformPoint(nodes[currentNode].position);
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

	}

	void CheckWayToHorn()
	{
		if (currentNode == 4 || currentNode == 7 && carHorn != null)
		{
			carHorn.Invoke();
		}
	}


}
