using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MC_Judgment : MonoBehaviour
{
    MusicJudgment_Sctipt Music_script;
    ColorJudgment_Sctipt Color_script;

    [SerializeField]
    private bool[] clearflag = new bool[2];

    [SerializeField]
    private GameObject[] objs;

    public bool OK;

    // Start is called before the first frame update
    void Start()
    {
        Music_script = this.gameObject.GetComponent<MusicJudgment_Sctipt>();
        Color_script = this.gameObject.GetComponent<ColorJudgment_Sctipt>();
    }

    // Update is called once per frame
    void Update()
    {
        //フラグ更新
        clearflag[0]=Music_script.GetEnergization();
        clearflag[1]=Color_script.GetEnergization();

        //全要素一致か判断
        IEnumerable<bool> results = (IEnumerable<bool>)clearflag.Distinct();

        int num = results.Count();

        //bool型で判定できるように変換
        foreach (bool Check in results)
        {
            OK = Check;
          
            if(num==2)
            {
                objs[0].SetActive(true);
                objs[1].SetActive(false);
            }
            else if (num == 1)
            {
               
                if (OK == false)
                {
                    objs[0].SetActive(true);
                    objs[1].SetActive(false);
                   
                }
                else
                {
                    //ここにクリアの証的なコード
                    objs[0].SetActive(false);
                    objs[1].SetActive(true);
                }
            }
        }
    }
}
