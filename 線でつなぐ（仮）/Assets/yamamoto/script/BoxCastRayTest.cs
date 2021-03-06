using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCastRayTest : MonoBehaviour
{

    [SerializeField]
    private Transform targetTra;    public GameObject Target;//レイが衝突しているオブジェクトを入れる 

    private Vector3 TargetScale;//ターゲットの元の大きさ

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
       //distanceFromTargetObj = Vector3.Distance(transform.position, targetTra.position);

        RaycastHit hit;

        Ray ray = new Ray(transform.position, transform.forward);//レイの設定

        //bool a = false;
        //a = Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Target"));


        //Cubeのレイを飛ばしターゲットと接触しているか判定
        if(grab==false)
        {
            if (Physics.Raycast(ray, out hit, 5.0f, LayerMask.GetMask("Target")))
            {
                Debug.Log(hit.transform.name);

                hit.collider.gameObject.GetComponent<ClickObj>().ChangeMaterial();//レイが当たったところに色付け

                // Debug.Log(hit.transform.position.y);

                Cancel = hit.collider.gameObject;//レイが当たったらオブジェクトを取得する（同じオブジェクトを二回クリックで選択を解除させるため）

                //左クリックされたときにレイと接触しているオブジェクトの座標をTargetに入れる
                if (Input.GetMouseButtonDown(0) && grab == false)
                {
                    Target = hit.collider.gameObject;
                    //手に持つ用にオブジェクトのサイズを帰る
                    TargetScale = Target.transform.localScale;
                    Target.transform.localScale /= 5;
                    Target.GetComponent<BoxCollider>().isTrigger = true;
                    Target.GetComponent<Rigidbody>().isKinematic = true;

                    grab = true;//掴みフラグをtrue
                    Cancel = Target;//キャンセルするオブジェクトを設定
                }
                //再度同じオブジェクトを選択で持ち状態を解除
                //else if (Input.GetMouseButtonDown(0) && grab == true && Cancel == Target)//TargetとCancelの取得しているオブジェクトが同じとき
                //{
                //    //オブジェクトの初期化
                //    Target = null;
                //    Cancel = null;
                //    //掴みフラグをfalse
                //    grab = false;

                //}

                //右クリックでオブジェクトを回転
                else if (Input.GetMouseButtonDown(1))
                {
                    hit.collider.gameObject.transform.eulerAngles += new Vector3(0.0f, 90.0f, 0.0f);
                }

            }
        }
        

        //マップチップにレイが接触しているか判定(rayを線に変更）
        else if (Physics.Raycast(ray, out hit, 4.0f, LayerMask.GetMask("Mapcip")))
        {

            Vector3 worldPos = hit.collider.gameObject.transform.position;//マップチップの座標を取得する

            if (grab == true)
            {
                hit.collider.gameObject.GetComponent<MapcipSlect>().ChangeMaterial();//掴んでるときのみ選択先の場所に色を出す
            }

            //左クリックされたときにマップチップの座標をTargetに上書きする
            if (Input.GetMouseButtonDown(0) && grab == true)
            {
                //マップチップの上にオブジェクトが置いていない時のみオブジェクトを設置する
                if(hit.collider.gameObject.GetComponent<MapcipSlect>().Onblock==false)
                {
                    //マップチップの高さが一定以上の時オブジェクトを置いた時の高さを調整する

                    //worldPos.y += Target.transformr.localPosition.y;
                    //worldPos.y += Target.transform.localScale.y / 2;//Y軸を固定する
                    worldPos.y += 0.5f;//Y軸を固定する

                    //手に持ったオブジェクトを元の大きさに戻す
                    Target.gameObject.transform.parent = null;
                    Target.transform.localScale = TargetScale;
                    Target.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                    //手に持ったオブジェクトの当たり判定を復活させる
                    Target.GetComponent<BoxCollider>().isTrigger = false;
                    Target.GetComponent<Rigidbody>().isKinematic = false;

                    Target.transform.position = worldPos;
                    Target = null;//タ-ゲットの初期化
                    grab = false;//掴みフラグをfalse
                }

                
                
            }

            //Debug.Log(hit.transform.name);
           
        }

        //ドアにレイが接触しているか判定(rayを線に変更）
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Door")))
        {
            if (Input.GetMouseButtonDown(0) && grab == false)
                hit.collider.gameObject.GetComponent<DoorOpoen>().RayOpenDoor();//ドアを開ける

            hit.collider.gameObject.GetComponent<DoorOpoen>().RayTargetDoor();//色付け
        }

        //床（電気を流すオブジェクト）にレイが接触しているか
        else if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Plane")))
        {

            //---------------------掴み処理（変数などは変える必要があるかも）------------------------------

            //Cancel = hit.collider.gameObject;//レイが当たったらオブジェクトを取得する（同じオブジェクトを二回クリックで選択を解除させるため）

            ////左クリックされたときにレイと接触しているオブジェクトの座標をTargetに入れる
            //if (Input.GetMouseButtonDown(0) && grab == false)
            //{
            //    Target = hit.collider.gameObject;
            //    grab = true;//掴みフラグをtrue
            //    hit.collider.gameObject.GetComponent<ClickObj>().ChangeMaterial(1);//色付け
            //    Cancel = Target;//キャンセルするオブジェクトを設定
            //}
            ////再度同じオブジェクトを選択で持ち状態を解除
            //else if (Input.GetMouseButtonDown(0) && grab == true && Cancel == Target)//TargetとCancelの取得しているオブジェクトが同じとき
            //{
            //    //Debug.Log("w");
            //    //オブジェクトの初期化
            //    Target = null;
            //    Cancel = null;
            //    //掴みフラグをfalse
            //    grab = false;

            //    hit.collider.gameObject.GetComponent<ClickObj>().ChangeMaterial(0);//色付け
            //}

            //右クリックでオブジェクトを回転
            if (Input.GetMouseButtonDown(1))
            {
                hit.collider.gameObject.transform.eulerAngles += new Vector3(0.0f, 90.0f, 0.0f);
            }

        }
        
    }

    void OnDrawGizmos()
    {
        //　Cubeのレイを疑似的に視覚化
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + transform.forward * distanceFromTargetObj, Vector3.one);
    }

}