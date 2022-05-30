using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTurnOnPower : MonoBehaviour
{
    public bool turn_on_power = false;
    public GameObject Door;
    public int Clear_num;
    private GameObject NonlitingChild;//光源となってない子オブジェクト取得用
    private GameObject LiteingChild;//光源となってる子オブジェクト取得用
    private bool isEnergized;        //通電してるかどうかの確認用変数

    [SerializeField, Header("電気のみかどうか(prefubで管理)")]
    private bool enegeronly;

    //音源取得
    [SerializeField]
    private AudioClip sound1;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        if (enegeronly == true)
        {
            NonlitingChild = gameObject.transform.Find("Nonliting").gameObject;
            LiteingChild = gameObject.transform.Find("Liting").gameObject;
        }
        //音源はいつもいる
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enegeronly == true)
        {
            if (isEnergized == false)
            {
                turn_on_power = false;
                Door.GetComponent<DoorOpoen>().ClearTaskflag[Clear_num] = false;
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
    }


    private void OnCollisionStay(Collision c)
    {
        //エナジーオンリーかどうかで判定OFF
        if (enegeronly == true)
        {
            //タグ名Conductorと接触しつづけてる時
            if (c.gameObject.CompareTag("Conductor"))
            {
                //つながってる導体のenergization(通電確認用変数)で初期化
                isEnergized = c.gameObject.GetComponent<NewConductor>().GetEnergization();

                //Debug.Log(c.gameObject.name);
                //つながっている導体のenergizasionがtrueならこのobjのcounductorhitもtrueにする
                if (isEnergized == true)
                {
                    //counductor_hitをtrueにする(連続で信号飛ばさん様に最初の一度のみ適用)
                    if (turn_on_power == false)
                    {
                        audioSource.PlayOneShot(sound1);
                        turn_on_power = true;
                        Door.GetComponent<DoorOpoen>().ClearTaskflag[Clear_num] = true;
                    }

                }
                else 
                {
                    //counductor_hitをtrueにする(連続で信号飛ばさん様に最初の一度のみ適用)
                    if (turn_on_power == true)
                    {
                        turn_on_power = false;
                        Door.GetComponent<DoorOpoen>().ClearTaskflag[Clear_num] = false;
                    }
                }
            }
        }
    }


    private void OnCollisionExit(Collision c)
    {
        //エナジーオンリーかどうかで判定OFF
        if (enegeronly == true)
        {
            //タグ名Conductorと離れたとき
            if (c.gameObject.CompareTag("Conductor"))
            {
                turn_on_power = false;
                Door.GetComponent<DoorOpoen>().ClearTaskflag[Clear_num] = false;

            }
        }
    }

    public void else_Switch_on()
    {
        //counductor_hitをtrueにする(連続で信号飛ばさん様に最初の一度のみ適用)
        if (turn_on_power == false)
        {
            audioSource.PlayOneShot(sound1);
            turn_on_power = true;
            Door.GetComponent<DoorOpoen>().ClearTaskflag[Clear_num] = true;
        }

    }

    public void else_Switch_off()
    {
        //OFFる
        turn_on_power = false;
        Door.GetComponent<DoorOpoen>().ClearTaskflag[Clear_num] = false;


    }
}
