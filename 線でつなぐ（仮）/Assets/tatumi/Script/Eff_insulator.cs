using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eff_insulator : MonoBehaviour
{
	private void OnParticleCollision(GameObject other)
	{
		// 当たった相手を強制電源OFF
		other.gameObject.GetComponent<Conductor_Script>();
	}
}
