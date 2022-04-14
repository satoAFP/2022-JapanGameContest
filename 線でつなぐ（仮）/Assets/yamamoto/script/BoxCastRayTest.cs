using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCastRayTest : MonoBehaviour
{

    [SerializeField]
    private Transform targetTra;    public GameObject Target;//レイが衝突しているオブジェクトを入れる 

    public GameObject MapCip;//レイが衝突しているマップチップを入れる

    //　ターゲットとの距離
    private float distanceFromTargetObj;


    public GameObject Cancel;//選択キャンセル用の変数

    public bool grab;//掴みフラグ

    void Start()
    {
        grab = false;//初期化
    }

    // Update is called once per frame
    void Update()
    {
        //　ターゲットとの距離
        distanceFromTargetObj = Vector3.Distance(transform.position, targetTra.position);

        RaycastHit hit;
        //Cubeのレイを飛ばしターゲットと接触しているか判定
        //Physics.BoxCast (Vector3 中心位置, Vector3 ボックスサイズの半分, Vector3 レイを飛ばす方向, out ヒットした情報, Quaternion ボックスの回転, float レイの長さ, int レイヤーマスク);
        if (Physics.BoxCast(transform.position, Vector3.one * 0.5f, transform.forward, out hit, Quaternion.identity, 100f, LayerMask.GetMask("Target")))
        {
            Debug.Log(hit.transform.name);

            Cancel = hit.collider.gameObject;//レイが当たったらオブジェクトを取得する（同じオブジェクトを二回クリックで選択を解除させるため）

            //左クリックされたときにレイと接触しているオブジェクトの座標をTargetに入れる
            if (Input.GetMouseButtonDown(0) && grab == false)
            {
                Target = hit.collider.gameObject;
                grab = true;//掴みフラグをtrue
                hit.collider.gameObject.GetComponent<ClickObj>().ChangeMaterial(1);//色付け
                Cancel = Target;//キャンセルするオブジェクトを設定
            }
            //再度同じオブジェクトを選択で持ち状態を解除
            else if (Input.GetMouseButtonDown(0) && grab == true && Cancel == Target)//TargetとCancelの取得しているオブジェクトが同じとき
            {
                //Debug.Log("w");
                //オブジェクトの初期化
                Target = null;
                Cancel = null;
                //掴みフラグをfalse
                grab = false;

                hit.collider.gameObject.GetComponent<ClickObj>().ChangeMaterial(0);//色付け
            }

            //右クリックでオブジェクトを回転
            else if(Input.GetMouseButtonDown(1))
            {
                //Debug.Log("クリック");

                hit.collider.gameObject.transform.eulerAngles += new Vector3(0.0f, 90.0f, 0.0f);
            }

        }

        //マップチップにレイが接触しているか判定(rayを線に変更）
        else if(Physics.BoxCast(transform.position, Vector3.one * 0.000005f, transform.forward, out hit, Quaternion.identity, 150f, LayerMask.GetMask("Mapcip")))
        {

            MapCip = hit.collider.gameObject;//レイが当たっているマップチップを取得

            Vector3 worldPos = hit.collider.gameObject.transform.position;//マップチップの座標を取得する

            if (grab == true)
            {
                hit.collider.gameObject.GetComponent<MapcipSlect>().ChangeMaterial();//掴んでるときのみ選択先の場所に色を出す
            }

            //左クリックされたときにマップチップの座標をTargetに上書きする
            if (Input.GetMouseButtonDown(0) && grab == true)
            {
                //マップチップの高さが一定以上の時オブジェクトを置いた時の高さを調整する
                if(worldPos.y>0.3f)
                {
                    worldPos.y = 0.9f;
                }
                else
                {
                   worldPos.y = 0.5f;//Y軸を固定する
                }
              
                Target.transform.position = worldPos;
                Target.GetComponent<ClickObj>().ChangeMaterial(0);//選択objの色を戻す
                Target = null;//タ-ゲットの初期化
                grab = false;//掴みフラグをfalse
                
            }

            //Debug.Log(hit.transform.name);
           
        }

        //ドアにレイが接触しているか判定(rayを線に変更）
        else if (Physics.BoxCast(transform.position, Vector3.one * 0.000005f, transform.forward, out hit, Quaternion.identity, 150f, LayerMask.GetMask("Door")))
        {

        }


    }

    void OnDrawGizmos()
    {
        //　Cubeのレイを疑似的に視覚化
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + transform.forward * distanceFromTargetObj, Vector3.one);
    }

}