using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnonPower_Script : MonoBehaviour
{
    public bool turn_on_power = false;
    public GameObject Door;
    public int Clear_num;
    private GameObject NonlitingChild;//光源となってない子オブジェクト取得用
    private GameObject LiteingChild;//光源となってる子オブジェクト取得用
    private bool energi_investigate;//電気ついてるかの確認用変数
    // Start is called before the first frame update
    void Start()
    {
        NonlitingChild = gameObject.transform.Find("Nonliting").gameObject;
        LiteingChild = gameObject.transform.Find("Liting").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (energi_investigate == false)
        {
            turn_on_power = false;
            //Door.GetComponent<DoorOpoen>().ClearTaskflag[Clear_num] = false;
        }

        if (turn_on_power == true)
        {
            NonlitingChild.SetActive(false);
            LiteingChild.SetActive(true);
        }
        else
        {
            NonlitingChild.SetActive(true);
            LiteingChild.SetActive(false);
        }
    }


    private void OnCollisionStay(Collision c)
    {
        //タグ名Conductorと接触しつづけてる時
        if (c.gameObject.tag == "Conductor")
        {
            //つながってる導体のenergization(通電確認用変数)で初期化
            energi_investigate = c.gameObject.GetComponent<Conductor_Script>().GetEnergization();

            //Debug.Log(c.gameObject.name);
            //つながっている導体のenergizasionがtrueならこのobjのcounductorhitもtrueにする
            if (energi_investigate == true)
            {
                //counductor_hitをtrueにする
                turn_on_power = true;
                //Door.GetComponent<DoorOpoen>().ClearTaskflag[Clear_num] = true;
            }
        }
    }


    private void OnCollisionExit(Collision c)
    {
        //タグ名Conductorと離れたとき
        if (c.gameObject.tag == "Conductor")
        {
            turn_on_power = false;
            //Door.GetComponent<DoorOpoen>().ClearTaskflag[Clear_num] = false;

        }
    }

}
