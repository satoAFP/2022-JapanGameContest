using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCastRayTest : MonoBehaviour
{

    [SerializeField]
    private Transform targetTra;
    //　ターゲットとの距離
    private float distanceFromTargetObj;

    public bool r_flag;//オブジェクトにレイが衝突しているかのフラグ

    void Start()
    {
        r_flag = false;
    }

    // Update is called once per frame
    void Update()
    {
        //
        //　ターゲットとの距離
        distanceFromTargetObj = Vector3.Distance(transform.position, targetTra.position);

        RaycastHit hit;
        //　Cubeのレイを飛ばしターゲットと接触しているか判定
        if (Physics.BoxCast(transform.position, Vector3.one * 0.5f, transform.forward, out hit, Quaternion.identity, 100f, LayerMask.GetMask("Target")))
        {
            Debug.Log(hit.transform.name);
            //接触したオブジェクトのスクリプトを取得し、フラグを変更
            hit.collider.GetComponent<ClickObj>().move=true;
            //接触していたらフラグtrue
            r_flag = true;
        }
        else
        {
            //接触してないときfalse
            r_flag = false;
        }

        if(r_flag==true)
        {
            Debug.Log("移動");
        }
    }

    void OnDrawGizmos()
    {
        //　Cubeのレイを疑似的に視覚化
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + transform.forward * distanceFromTargetObj, Vector3.one);
    }
}