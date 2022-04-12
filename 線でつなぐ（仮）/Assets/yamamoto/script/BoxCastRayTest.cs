using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCastRayTest : MonoBehaviour
{

    [SerializeField]
    private Transform targetTra;
    //　ターゲットとの距離
    private float distanceFromTargetObj;

    public GameObject Target;//レイが衝突しているオブジェクトを入れる 

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //　ターゲットとの距離
        distanceFromTargetObj = Vector3.Distance(transform.position, targetTra.position);

        RaycastHit hit;
        //　Cubeのレイを飛ばしターゲットと接触しているか判定
        if (Physics.BoxCast(transform.position, Vector3.one * 0.5f, transform.forward, out hit, Quaternion.identity, 100f, LayerMask.GetMask("Target")))
        {
            Debug.Log(hit.transform.name);

            //接触したオブジェクトのスクリプトを取得し、フラグを変更
            hit.collider.GetComponent<ClickObj>().move=true;

            //左クリックされたときにレイと接触しているオブジェクトの座標をTargetに入れる
            if (Input.GetMouseButtonDown(0))
            {
                Target = hit.collider.gameObject;
            }

        }

        //マップチップにレイが接触しているか判定(rayを線に変更）
        else if(Physics.BoxCast(transform.position, Vector3.one * 0.000005f, transform.forward, out hit, Quaternion.identity, 100f, LayerMask.GetMask("Mapcip")))
        {

            Vector3 worldPos = hit.collider.gameObject.transform.position;//マップチップの座標を取得する

           
            //左クリックされたときにマップチップの座標をTargetに上書きする
            if (Input.GetMouseButtonDown(0))
            {
                worldPos.y = 0.5f;//Y軸を固定する
                Target.transform.position = worldPos;
                Target = new GameObject();
            }

            Debug.Log(hit.transform.name);
           
        }
    }

    void OnDrawGizmos()
    {
        //　Cubeのレイを疑似的に視覚化
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + transform.forward * distanceFromTargetObj, Vector3.one);
    }

}