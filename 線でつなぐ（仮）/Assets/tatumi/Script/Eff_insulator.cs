using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eff_insulator : MonoBehaviour
{
	private void OnParticleCollision(GameObject other)
	{
		// “–‚½‚Á‚½‘Šè‚ğ‹­§“dŒ¹OFF
		other.gameObject.GetComponent<Conductor_Script>();
	}
}
