using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIniMap : MonoBehaviour
{
    public GameObject player;
    Vector3 trans, Angle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Angle = player.transform.localEulerAngles;
        trans = player.transform.position;

        Angle.x = 90.0f;
        trans.y += 12.0f;

        //this.transform.rotation = Quaternion.Euler(Angle);
        this.transform.position = trans;
    }
}
