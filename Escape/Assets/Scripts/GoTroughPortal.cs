using UnityEngine;
using System.Collections;

public class GoTroughPortal : MonoBehaviour {

	public GameObject otherPortal;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
			other.transform.position = otherPortal.transform.position;
	}
}
