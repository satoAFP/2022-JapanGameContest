using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eff_insulator : MonoBehaviour
{
	private void OnParticleCollision(GameObject other)
	{
		// ������������������d��OFF
		other.gameObject.GetComponent<Conductor_Script>();
	}
}
