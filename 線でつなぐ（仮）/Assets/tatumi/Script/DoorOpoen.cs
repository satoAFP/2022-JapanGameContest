using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoorOpoen : MonoBehaviour
{
    public bool[] ClearTaskflag;
    private bool taskflag = false;
    private int num,now_material=0;

    
    public Animator anim;

    public Material[] mat = new Material[2];//変更したいマテリアルをセット
    Material[] mats;


    // Start is called before the first frame update
    void Start()
    {
        mats = GetComponent<Renderer>().materials;
    }

    // Update is called once per frame
    void Update()
    {
        //全要素一致か判断
        IEnumerable<bool> results = (IEnumerable<bool>)ClearTaskflag.Distinct();
       
        num = results.Count();

        //bool型で判定できるように変換
        foreach (bool Check in results)

        taskflag = Check;

        mats[0] = mat[0];

        GetComponent<Renderer>().materials = mats;

    }

    public void RayOpenDoor()
    {
        //ドアが開けるかどうか見てアニメーションを再生
        if (num == 1 && taskflag == true)
            anim.SetBool("Open", true);
        else
            anim.SetBool("Open", false);
    }

    public void RayTargetDoor()
    {
        //マテリアル変更(1=強調0=普通)

        mats[0] = mat[1];

        GetComponent<Renderer>().materials = mats;
      
    }
}
