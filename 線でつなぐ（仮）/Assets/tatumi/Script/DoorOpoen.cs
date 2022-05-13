using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoorOpoen : MonoBehaviour
{
    public bool[] ClearTaskflag;
    private bool taskflag = false;
    private int num,now_material=0;

    [SerializeField, Header("ワープゲートとして使うか(true)、ステージのドアとして使うか(false)")] public bool warp_door;
    [SerializeField, Header("ドアのアニメーション")] public Animator door;
    [SerializeField, Header("ドアノブのアニメーション")] public Animator doorknob;

    public Material[] mat = new Material[1];//変更したいマテリアルをセット
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
       
        //num = 1 : 
        num = results.Count();

        //bool型で判定できるように変換
        foreach (bool Check in results)

        taskflag = Check;

        mats[0] = mat[1];

        GetComponent<Renderer>().materials = mats;

    }

    public void RayOpenDoor()
    {
        if (!warp_door)
        {
            //ドアが開けるかどうか見てアニメーションを再生
            if (num == 1 && taskflag == true)
            {
                door.SetBool("open", true);
                doorknob.SetBool("open", true);
            }
            else
            {
                door.SetBool("open", false);
                doorknob.SetBool("open", false);
            }
        }
    }

    public void RayOpenWarpDoor()
    {
        if (warp_door)
        {
            //ステージ入るための扉開く処理
            door.SetBool("open", true);
            doorknob.SetBool("open", true);
        }
    }

    public void RayTargetDoor()
    {
        //マテリアル変更(1=強調0=普通)

        mats[0] = mat[0];

        GetComponent<Renderer>().materials = mats;

        Debug.Log("k");

    }
}
