using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Imageコンポーネントを必要とする
[RequireComponent(typeof(Image))]

public class Allmap_move : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    // ドラッグ前の位置(中心位置)
    private Vector3 prevPos;

    //移動対象
    public GameObject All_map;
    //変数取得用
    Vector3 trans_C;

    //MAP限界点座標
    [SerializeField, Header("座標限界点(z=縦,x=横)")]
    public float END_z_front, END_z_back,END_x_right,END_x_left;

    // Use this for initialization
    void Start()
    {
        //rootPos = new Vector3(512.0f, 287.0f, 0f); //画面の半分
    }

    // Update is called once per frame
    void Update()
    {
        //常に今カメラが何処か取得
        trans_C = All_map.GetComponent<Allmap_moveCamera>().Getpos();

        //マウスホイールの回転量取得
        var scroll = Input.mouseScrollDelta.y;

        //一度sizeに落とし込む
        var size = All_map.GetComponent<Camera>().orthographicSize;

        //限界点の場合スルー
        if (size - scroll > 20)
            ;
        else if (size - scroll < 0)
            ;
        //限界点に引っかからない場合MAPの拡大率に影響させる（CameraのSIZE変更）
        else
        All_map.GetComponent<Camera>().orthographicSize =size - scroll;
    }


    //ドラッグ＆ドロップ関係

    public void OnBeginDrag(PointerEventData eventData)
    {
        // ドラッグ前の位置を記憶しておく
        prevPos = Input.mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //縦のみ反映(2Dy→3Dzに変換（縦）)
        trans_C.z = trans_C.z - ((prevPos.y - eventData.position.y) *0.01f);
        //横用
        trans_C.x = trans_C.x - ((prevPos.x - eventData.position.x) * 0.01f);

        //移動上限(縦)
        if (trans_C.z < END_z_front)
            ;
        else if (trans_C.z > END_z_back)
            ;
        //反映
        else
        All_map.GetComponent<Allmap_moveCamera>().Setpos_z(trans_C.z);

        //移動上限(横)
        if (trans_C.x > END_x_right)
            ;
        else if (trans_C.x < END_x_left)
            ;
        //反映
        else
            All_map.GetComponent<Allmap_moveCamera>().Setpos_x(trans_C.x);

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //そんなに違和感ないので今のところ無し
       
    }

}
