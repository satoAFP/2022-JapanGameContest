using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Allmap_moveCamera : MonoBehaviour
{

    //まさかの言われたとおりにするだけの存在

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 Getpos()
    {
        //横
        //trans.x += a.x + this.transform.position.x;

        //縦
       // trans.z = a.y + this.transform.position.z;

        return this.transform.position;
    }

    public void Setpos_x(float a)
    {

        Vector3 trans = this.transform.position;
        trans.x = a;
        this.transform.position = trans;
    }

    public void Setpos_z(float a)
    {
        Vector3 trans = this.transform.position;
        trans.z = a;
        this.transform.position = trans;
    }
}
