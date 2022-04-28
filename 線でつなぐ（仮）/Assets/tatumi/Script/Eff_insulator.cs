using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Eff_insulator : MonoBehaviour
{
	[SerializeField]
	private bool Stay=false;

    [SerializeField]
    private GameObject[] HitCon;

    [SerializeField]
    private string[] HitCon_name; 
    

    private int i,nowObj=1;

	void FixedUpdate()
    {

        i++;

        if(i==5)
        {
           HitCon[nowObj].GetComponent<Conductor_Script>().EffExit();
           HitCon[nowObj] = null;
            nowObj--;
        }
        
    }

	private void OnParticleCollision(GameObject other)
	{
        if (other.gameObject.tag == "Conductor")
        {
            string result = HitCon_name.SingleOrDefault(value => value == other.gameObject.name);

            Debug.Log(result);

                // ìñÇΩÇ¡ÇΩëäéËÇã≠êßìdåπOFF
            other.gameObject.GetComponent<Conductor_Script>().PowerOff();
                HitCon[nowObj] = other.gameObject;
              //  nowObj++;
            
               
            i = 0;
              
        }
	}

    
}
