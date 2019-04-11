using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
	public Color lineColor;
	public List<Transform> nodes;

	private void OnDrawGizmos()
	{
		Gizmos.color = lineColor;
		Transform[] pathTransforms = GetComponentsInChildren<Transform>();
		nodes = new List<Transform>();

		// get children node
		for (int i = 0; i < pathTransforms.Length; i++)
		{
			if (pathTransforms[i] != transform)
			{
				nodes.Add(pathTransforms[i]);
			}
		}

		//
		for (int i = 0, len = nodes.Count; i < len; i++)
		{
			Vector3 currentNode = nodes[i].position;
			Vector3 preNode = Vector3.zero;

			if (i > 0)
			{
				preNode = nodes[i - 1].position;
				Gizmos.DrawLine(preNode, currentNode);

			}
			else {
				preNode = nodes[len - 1].position;
			}

			Gizmos.DrawWireSphere(currentNode, 0.9f);
		}
	}

	
}
