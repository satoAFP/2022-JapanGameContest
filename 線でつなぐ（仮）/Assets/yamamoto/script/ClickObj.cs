using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObj : MonoBehaviour
{
    public Material[] mat = new Material[2];//変更したいマテリアルをセット
    Material[] mats;

    [SerializeField]
    private bool Rotationflag;

    // Start is called before the first frame update
    void Start()
    {
        mats = GetComponent<Renderer>().materials;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //関数化（Updateはうざいため）
    public void ChangeMaterial(int a)
    {
        //マテリアル変更(1=強調0=普通)
        
        mats[0] = mat[a];

        GetComponent<Renderer>().materials = mats;
        //ここちゃんとかけ
        Debug.Log(Mathf.Round(this.transform.localEulerAngles.y));

        if (Rotationflag == true)
        {
            if (Mathf.Round(this.transform.localEulerAngles.y) == 0 || this.transform.localEulerAngles.y == 180.0f)
                this.GetComponent<Conductor_Script>().enabled = true;
            else
                this.GetComponent<Conductor_Script>().enabled = false;
        }
    }

   
}
