using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicJudgment_Sctipt : MonoBehaviour
{
    //�Q�[���N���A�ƂȂ�J���[
   
    [SerializeField]
    private int clearMusic,muisc_num;

    //������Ώہi�҂��҂������p�j
    [SerializeField]
    private GameObject[] objs;

    // Update is called once per frame
    void Update()
    {
        
        
        objs[0].SetActive(true);
        objs[1].SetActive(false);

        if (clearMusic==muisc_num)
        {
            //�����ɃN���A�̏ؓI�ȃR�[�h
            objs[0].SetActive(false);
            objs[1].SetActive(true);
        }
    }

    public void now_music(int a)
    {
        muisc_num = a;
    }
}
