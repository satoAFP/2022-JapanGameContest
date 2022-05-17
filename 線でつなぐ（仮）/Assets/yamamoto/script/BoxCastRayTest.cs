using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCastRayTest : MonoBehaviour
{

    [SerializeField]
    private Transform targetTra;    public GameObject Target;//レイが衝突しているオブジェクトを入れる   

    [SerializeField] private int getsize; //オブジェクトを拾った時に手に収まるサイズにする

    [SerializeField] private Vector3 TargetPos; //オブジェクトを拾った時に手に収まる位置

    [SerializeField] private Vector3 TargetRor; //オブジェクトを拾った時に手に収まる回転

    private Vector3 TargetScale;//ターゲットの元の大きさ

    private Vector3 TargetRotate;//ターゲットの元の角度

    //　ターゲットとの距離
    private float distanceFromTargetObj;


    public GameObject Cancel;//選択キャンセル用の変数

    public bool grab;//掴みフラグ

    private bool setlineblock = false;//ClickObjのNosetlineの受け取りフラグ


    [System.NonSerialized]
    public bool Existence_Check = false;//判定ブロック存在フラグ

    [System.NonSerialized]
    public bool  NosetLight = false;//ライトオブジェクトの上にオブジェクトを置かせない

    // 判定ブロックプレハブ格納用
    public GameObject Judgeblock;

    private bool first_setblock = true;//判定ブロックをマップチップにレイが当たったら一個だけ設置

    private GameObject Memmapcip=null;//現在選択しているマップチップ記憶

    private GameObject cloneblock = null;//生成された判定ブロックを入れる

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

        //壁にレイが接触しているか(接触していたら他のオブジェクトとのレイの処理を行わない）
        if (Physics.Raycast(ray, out hit, 4.0f, LayerMask.GetMask("Wall")) || Physics.Raycast(ray, out hit, 4.0f, LayerMask.GetMask("Door")))
        {
            Debug.Log("Wall");
        }
        //Cubeのレイを飛ばしターゲットと接触しているか判定
        else if (grab==false)
        {
            if (Physics.Raycast(ray, out hit, 5.0f, LayerMask.GetMask("Target")))
            {
                Debug.Log(hit.transform.name);

                hit.collider.gameObject.GetComponent<ClickObj>().ChangeMaterial();//レイが当たったところに色付け

                // Debug.Log(hit.transform.position.y);

                Cancel = hit.collider.gameObject;//レイが当たったらオブジェクトを取得する（同じオブジェクトを二回クリックで選択を解除させるため）

                //シリンダーの上に置けるブロックの場合、シリンダー設置ON
                if(hit.collider.GetComponent<ClickObj>().Setlineblock == true)
                {
                    setlineblock = true;
                    Debug.Log("as");
                }

                //左クリックされたときにレイと接触しているオブジェクトの座標をTargetに入れる
                if (Input.GetMouseButtonDown(0) && grab == false)
                {
                    Target = hit.collider.gameObject;

                    //手に持つ用にオブジェクトのサイズと回転を記憶
                    TargetScale = Target.transform.localScale;
                    TargetRotate = Target.transform.eulerAngles;

                    //カメラの子オブジェクトにする
                    Target.transform.parent = gameObject.transform;

                    //手に持つ用にオブジェクトのサイズと回転の変更
                    Target.transform.localScale /= getsize;
                    Target.transform.localEulerAngles = TargetRor;
                    Target.transform.localPosition = TargetPos;
                    Target.GetComponent<BoxCollider>().isTrigger = true;
                    Target.GetComponent<Rigidbody>().isKinematic = true;

                    grab = true;//掴みフラグをtrue
                    Cancel = Target;//キャンセルするオブジェクトを設定
                }
               
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

            if(Memmapcip == hit.collider.gameObject)
            {
                if (first_setblock)
                {
                    //判定ブロック生成
                    cloneblock = Instantiate(Judgeblock, new Vector3(worldPos.x, worldPos.y += 0.25f, worldPos.z), Quaternion.identity);
                    first_setblock = false;
                }
                Debug.Log("aaa");
            }
            else
            {
                first_setblock = true;
                Existence_Check = false;
                Destroy(cloneblock);
            }



            if (grab == true)
            {
                hit.collider.gameObject.GetComponent<MapcipSlect>().ChangeMaterial();//掴んでるときのみ選択先の場所に色を出す
            }

            //左クリックされたときにマップチップの座標をTargetに上書きする
            if (Input.GetMouseButtonDown(0) && grab == true && hit.collider.gameObject.GetComponent<MapcipSlect>().Onplayer==false)
            {
                //線の上に置けるオブジェクトかどうか判断して置ける処理を変更
                //線の上に置ける
                if (setlineblock)
                {
                    //マップチップの上にオブジェクトが置いていない時のみオブジェクトを設置する
                    if (hit.collider.gameObject.GetComponent<MapcipSlect>().Onblock == false)
                    {
                        //マップチップの高さが一定以上の時オブジェクトを置いた時の高さを調整する

                        //worldPos.y += Target.transformr.localPosition.y;
                        //worldPos.y += Target.transform.localScale.y / 2;//Y軸を固定する
                        worldPos.y += 0.5f;//Y軸を固定する

                        //手に持ったオブジェクトを元の大きさに戻す
                        Target.gameObject.transform.parent = null;
                        Target.transform.localScale = TargetScale;
                        Target.transform.localEulerAngles = TargetRotate;
                        //手に持ったオブジェクトの当たり判定を復活させる
                        Target.GetComponent<BoxCollider>().isTrigger = false;
                        Target.GetComponent<Rigidbody>().isKinematic = false;

                        Target.transform.position = worldPos;
                        Target = null;//タ-ゲットの初期化
                        grab = false;//掴みフラグをfalse
                        setlineblock = false;//線の上に置けるオブジェクト設定を初期化
                    }
                }
                //線の上に置けない
                else
                {
                    if (!Existence_Check&&!NosetLight)
                    {
                        //マップチップの上にオブジェクトが置いていない時のみオブジェクトを設置する
                        if (hit.collider.gameObject.GetComponent<MapcipSlect>().Onblock == false)
                        {
                            //マップチップの高さが一定以上の時オブジェクトを置いた時の高さを調整する

                            //worldPos.y += Target.transformr.localPosition.y;
                            //worldPos.y += Target.transform.localScale.y / 2;//Y軸を固定する
                            worldPos.y += 0.5f;//Y軸を固定する

                            //手に持ったオブジェクトを元の大きさに戻す
                            Target.gameObject.transform.parent = null;
                            Target.transform.localScale = TargetScale;
                            Target.transform.localEulerAngles = TargetRotate;
                            //手に持ったオブジェクトの当たり判定を復活させる
                            Target.GetComponent<BoxCollider>().isTrigger = false;
                            Target.GetComponent<Rigidbody>().isKinematic = false;

                            Target.transform.position = worldPos;
                            Target = null;//タ-ゲットの初期化
                            grab = false;//掴みフラグをfalse
                            setlineblock = false;//線の上に置けるオブジェクト設定を初期化
                        }
                    }
                }
            }

        }
        else
        {
            Existence_Check = false;
        }

        //現在の選択中のオブジェクト（マップチップ）を記憶  
        Memmapcip = hit.collider.gameObject;
        //-----------使ってないレイヤーの処理（コメント解除で使えるよ！）--------------------


        ////穴マップチップにレイが接触しているか判定(rayを線に変更）
        //else if (Physics.Raycast(ray, out hit, 4.0f, LayerMask.GetMask("Hole")))
        //{

        //    Vector3 worldPos = hit.collider.gameObject.transform.position;//マップチップの座標を取得する

        //    if (grab == true)
        //    {
        //        hit.collider.gameObject.GetComponent<MapcipSlect>().ChangeMaterial();//掴んでるときのみ選択先の場所に色を出す
        //    }

        //    //左クリックされたときにマップチップの座標をTargetに上書きする
        //    if (Input.GetMouseButtonDown(0) && grab == true)
        //    {
        //        //マップチップの上にオブジェクトが置いていない時のみオブジェクトを設置する
        //        if (hit.collider.gameObject.GetComponent<MapcipSlect>().Onblock == false)
        //        {
        //            //マップチップの高さが一定以上の時オブジェクトを置いた時の高さを調整する

        //            //worldPos.y += Target.transformr.localPosition.y;
        //            //worldPos.y += Target.transform.localScale.y / 2;//Y軸を固定する
        //            worldPos.y -= 0.5f;//Y軸を固定する

        //            //手に持ったオブジェクトを元の大きさに戻す
        //            Target.gameObject.transform.parent = null;
        //            Target.transform.localScale = TargetScale;
        //            Target.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        //            //手に持ったオブジェクトの当たり判定を復活させる
        //            Target.GetComponent<BoxCollider>().isTrigger = false;
        //            Target.GetComponent<Rigidbody>().isKinematic = false;

        //            Target.transform.position = worldPos;
        //            Target = null;//タ-ゲットの初期化
        //            grab = false;//掴みフラグをfalse
        //        }
        //    }
        //}

        ////床（電気を流すオブジェクト）にレイが接触しているか
        //else if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Plane")))
        //{
        //    //右クリックでオブジェクトを回転
        //    if (Input.GetMouseButtonDown(1))
        //    {
        //        hit.collider.gameObject.transform.eulerAngles += new Vector3(0.0f, 90.0f, 0.0f);
        //    }
        //}


        //-----------------------------------------------------------------------------------------

        
        //ブロックを持っている時に回転させる
        if (Input.GetMouseButtonDown(1) && grab ==true)
        {
           // Debug.Log("あばばばばばば");
            TargetRotate += new Vector3(0.0f, 90.0f, 0.0f);
        }

        //ドアにレイが接触しているか判定(rayを線に変更）
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Door")))
        {
            if (Input.GetMouseButtonDown(0) && grab == false)
            {
                hit.collider.gameObject.GetComponent<DoorOpoen>().RayOpenDoor();//ドアを開ける
                hit.collider.gameObject.GetComponent<DoorOpoen>().RayOpenWarpDoor();//ワープドアを開ける
            }
            

            hit.collider.gameObject.GetComponent<DoorOpoen>().RayTargetDoor();//色付け
        }

        //ボタンがレイに接触しているか判定
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Button")))
        {
            if (Input.GetMouseButtonDown(0) && grab == false)
            {
                hit.collider.gameObject.GetComponent<button>().RayPushButton();//ボタンが沈む
            }
             
            hit.collider.gameObject.GetComponent<button>().RayTargetButton();//色付け
        }


    }

    void OnDrawGizmos()
    {
        //　Cubeのレイを疑似的に視覚化
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + transform.forward * distanceFromTargetObj, Vector3.one);
    }

}