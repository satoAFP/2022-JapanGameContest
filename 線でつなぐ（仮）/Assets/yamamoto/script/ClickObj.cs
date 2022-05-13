using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObj : MonoBehaviour
{
    public Material[] mat = new Material[2];//変更したいマテリアルをセット
    Material[] mats;

    [SerializeField]
    private bool Rotationflag,vertical;

    [SerializeField]
    public bool Nosetline = false;//シリンダーの上に置けないブロック


    // Start is called before the first frame update
    void Start()
    {
        mats = GetComponent<Renderer>().materials;
    }

    // Update is called once per frame
    void Update()
    {
        if(mat[0]!=mats[0])
        {
            //レイが当たってないときは初期色にし続ける
            mats[0] = mat[0];

            GetComponent<Renderer>().materials = mats;
        }

        if (Rotationflag == true)
        {
            if (vertical == true)
            {
                if (Mathf.Round(this.transform.localEulerAngles.y) == 0 || this.transform.localEulerAngles.y == 180.0f)
                    this.GetComponent<Conductor_Script>().SetEnergization(true);
                else
                    this.GetComponent<Conductor_Script>().SetEnergization(false);
            }
            else if (vertical == false)
            {
                if (Mathf.Round(this.transform.localEulerAngles.y) == 90.0f || this.transform.localEulerAngles.y == 270.0f)
                    this.GetComponent<Conductor_Script>().SetEnergization(true);
                else
                    this.GetComponent<Conductor_Script>().SetEnergization(false);
            }
        }

    }

    //関数化（Updateはうざいため）
    public void ChangeMaterial()
    {
        //マテリアル変更(1=強調0=普通)
        
        mats[0] = mat[1];

        GetComponent<Renderer>().materials = mats;
       
    }

   
}
