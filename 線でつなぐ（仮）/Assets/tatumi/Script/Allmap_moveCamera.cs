using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Allmap_moveCamera : MonoBehaviour
{

    //まさかの言われたとおりにするだけの存在詳細はAllmap_moveを確認

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //現在地返し
    public Vector3 Getpos()
    {
        //横(向こう側で処理)
        //trans.x += a.x + this.transform.position.x;

        //縦
       // trans.z = a.y + this.transform.position.z;

        return this.transform.position;
    }

    //横（X）の場所設定
    public void Setpos_x(float a)
    {

        Vector3 trans = this.transform.position;
        trans.x = a;
        this.transform.position = trans;
    }

    //横\縦（Z）の場所設定
    public void Setpos_z(float a)
    {
        Vector3 trans = this.transform.position;
        trans.z = a;
        this.transform.position = trans;
    }
}
