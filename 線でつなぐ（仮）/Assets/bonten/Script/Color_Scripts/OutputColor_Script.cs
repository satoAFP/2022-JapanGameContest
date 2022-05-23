using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputColor_Script : Base_Color_Script
{
    protected int[] my_color = new int[3];
    protected GameObject ClearObj;
    protected GameObject MixObj;
    [SerializeField]
    protected bool mixObj_hit;        //�F������obj�ƐڐG���Ă邩�m�F����p�̕ϐ�
    [SerializeField]
    protected bool clearObj_hit;      //�N���A�����obj�ƐڐG���Ă邩�m�F����v�̕ϐ�
    [SerializeField]
    protected int cnt;          // ����ʍs�̂��߂̗D��x

    [SerializeField]
    GameObject efflight;

    ParticleSystem.MainModule par;

    //�A�N�Z�T�[
    public int GetPrecedence()
    {
        return cnt;
    }
    public void SetPrecedence(int num)
    {
        cnt = num;
    }

    private void Start()
    {
        colorchange_signal = false;
        par = GetComponent<ParticleSystem>().main;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="ColorInput")
        {
            energization = true;
            cnt = 1;
            SetColor(collision.gameObject, ADDITION);
            efflight.GetComponent<ColorLight_Script1>().SetLight(color);
            if (this.gameObject.GetComponent<ClickObj>()!=null)
            {
                this.gameObject.GetComponent<ClickObj>().SetColor(new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], (byte)200));
            }
            //���g�̒��F���s������ɁAMixColorObj�����Clear����Obj�ƐڐG���Ă邩�m�F��
            //�ڐG���Ă��璅�F����
            if (mixObj_hit)
            {
                MixObj.GetComponent<Base_Color_Script>().SetColor(color, ADDITION);
                MixObj.GetComponent<Base_Color_Script>().SetColorChange(true);
            }
            else if (clearObj_hit)
            {
                ClearObj.GetComponent<Base_Color_Script>().SetColor(color, ADDITION);
                ClearObj.GetComponent<Base_Color_Script>().SetColorChange(true);
            }
        }
        if (collision.gameObject.tag == "ColorMix")
        {
            MixObj = collision.gameObject;
            mixObj_hit = true;
        }
        if (collision.gameObject.tag == "Power_Supply")
        {
            ClearObj = collision.gameObject;
            clearObj_hit = true;
        }

    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "ColorInput")
        {
            //ColorInput�����ꂽ��AColorInput�ɓd�C�������Ȃ��Ȃ������̏���
            if (collision.gameObject.GetComponent<InputColor_Script>().GetEnergization() == false && energization == true)
            {
                //���g�̒E�F���s���O�ɁAMixColorObj�����Clear����Obj�ƐڐG���Ă邩�m�F��
                //�ڐG���Ă����ɒE�F�������s��
                if (mixObj_hit)
                {
                    MixObj.GetComponent<MixColor_Script>().Decolorization(color);
                }
                else if (clearObj_hit)
                {
                    ClearObj.GetComponent<Base_Color_Script>().SetColor(color, SUBTRACTION);
                }
                //���L��MixColorObj��E�F�ł���悤��true�ɂ���
                energization = false;
                //ColorInput����F���擾
                SetColor(collision.gameObject.GetComponent<Base_Color_Script>().GetColor(), SUBTRACTION);
                efflight.GetComponent<ColorLight_Script1>().SetLight(color);
            }
        }
        else if (collision.gameObject.tag == "ColorOutput")
        {
            //�d�C���ʂ��Ă��邩�ǂ����m�F�B
            if (collision.gameObject.GetComponent<OutputColor_Script>().GetEnergization() == false && energization == true)
            {
                //�������Ă���Obj�̗D��x(cnt�ϐ�)��0(0�Ȃ炷�łɒE�F����Ă�)�łȂ��A����Obj��菬�����Ȃ�Aenergization�͓r�؂�Ă�̂ŐF��j������B
                if (collision.gameObject.GetComponent<OutputColor_Script>().GetPrecedence() != 0 && cnt > collision.gameObject.GetComponent<OutputColor_Script>().GetPrecedence())
                {
                    energization = false;
                    //���g�̒E�F���s���O�ɁAMixColorObj�����Clear����Obj�ƐڐG���Ă邩�m�F��
                    //�ڐG���Ă����ɒE�F�������s��
                    if (mixObj_hit)
                    {
                        MixObj.GetComponent<MixColor_Script>().Decolorization(color);
                    }
                    else if(clearObj_hit)
                    {
                        ClearObj.GetComponent<Base_Color_Script>().SetColor(color, SUBTRACTION);
                        ClearObj.GetComponent<Base_Color_Script>().SetColorChange(true);
                    }
                    //ColorInput����F���擾
                    SetColor(collision.gameObject.GetComponent<OutputColor_Script>().GetColor());
                    efflight.GetComponent<ColorLight_Script1>().SetLight(color);
                    if (this.gameObject.GetComponent<ClickObj>() != null)
                    {
                        this.gameObject.GetComponent<ClickObj>().SetColor(new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], (byte)200));
                    }
                }
            }
            else if (collision.gameObject.GetComponent<OutputColor_Script>().GetEnergization() == true && energization == false)
            {
                //�D��x(cnt�ϐ�)��0(0�Ȃ�E�F����Ă�)�łȂ��A����Obj��菬�����Ȃ炻��Obj�̐F���擾����B
                if ((collision.gameObject.GetComponent<OutputColor_Script>().GetPrecedence() != 0 || cnt < collision.gameObject.GetComponent<OutputColor_Script>().GetPrecedence()))
                {
                    Debug.Log("�͂����"+this.gameObject.name);
                    //�ڐG���Ă�RelayColor�̃J�E���g���1�傫���l���擾����i����ʍs�ɂ��邽�߁j
                    cnt = collision.gameObject.GetComponent<OutputColor_Script>().GetPrecedence() + 1;
                    energization = true;
                    colorchange_signal = true;
                    //ColorInput����F���擾
                    SetColor(collision.gameObject, ADDITION);
                    efflight.GetComponent<ColorLight_Script1>().SetLight(color);
                    if (this.gameObject.GetComponent<ClickObj>() != null)
                    {
                        this.gameObject.GetComponent<ClickObj>().SetColor(new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], (byte)200));
                    }
                    //���g�̒E�F���s������ɁAMixColorObj�����Clear����Obj�ƐڐG���Ă邩�m�F��
                    //�ڐG���Ă���F������
                    if (mixObj_hit)
                    {
                        MixObj.GetComponent<Base_Color_Script>().SetColor(color, ADDITION);
                        MixObj.GetComponent<Base_Color_Script>().SetColorChange(true);
                    }
                    else if (clearObj_hit)
                    {
                        ClearObj.GetComponent<Base_Color_Script>().SetColor(color, ADDITION);
                        ClearObj.GetComponent<Base_Color_Script>().SetColorChange(true);
                    }
                }
            }
        }
        else if(collision.gameObject.tag == "Rotate")
        {
            //�d�C���ʂ��Ă��邩�ǂ����m�F�B
            if (collision.gameObject.GetComponent<Rotate_OutputColor>().GetEnergization() == false && energization == true)
            {
                //�������Ă���Obj�̗D��x(cnt�ϐ�)��0(0�Ȃ炷�łɒE�F����Ă�)�łȂ��A����Obj��菬�����Ȃ�Aenergization�͓r�؂�Ă�̂ŐF��j������B
                if (collision.gameObject.GetComponent<Rotate_OutputColor>().GetPrecedence() != 0 && cnt > collision.gameObject.GetComponent<Rotate_OutputColor>().GetPrecedence())
                {
                    Debug.Log("2");
                    energization = false;
                    //���g�̒E�F���s���O�ɁAMixColorObj�����Clear����Obj�ƐڐG���Ă邩�m�F��
                    //�ڐG���Ă����ɒE�F�������s��
                    if (mixObj_hit)
                    {
                        MixObj.GetComponent<MixColor_Script>().Decolorization(color);
                    }
                    else if (clearObj_hit)
                    {
                        ClearObj.GetComponent<Base_Color_Script>().SetColor(color, SUBTRACTION);
                        ClearObj.GetComponent<Base_Color_Script>().SetColorChange(true);
                    }
                    //ColorInput����F���擾
                    SetColor(collision.gameObject.GetComponent<Rotate_OutputColor>().GetColor());
                    efflight.GetComponent<ColorLight_Script1>().SetLight(color);
                    if (this.gameObject.GetComponent<ClickObj>() != null)
                    {
                        this.gameObject.GetComponent<ClickObj>().SetColor(new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], (byte)200));
                    }
                }
            }
            else if (collision.gameObject.GetComponent<Rotate_OutputColor>().GetEnergization() == true && energization == false)
            {
                //�D��x(cnt�ϐ�)��0(0�Ȃ�E�F����Ă�)�łȂ��A����Obj��菬�����Ȃ炻��Obj�̐F���擾����B
                if ((collision.gameObject.GetComponent<Rotate_OutputColor>().GetPrecedence() != 0 || cnt < collision.gameObject.GetComponent<Rotate_OutputColor>().GetPrecedence()))
                {
                    //�ڐG���Ă�RelayColor�̃J�E���g���1�傫���l���擾����i����ʍs�ɂ��邽�߁j
                    cnt = collision.gameObject.GetComponent<Rotate_OutputColor>().GetPrecedence() + 1;
                    energization = true;
                    colorchange_signal = true;
                    //ColorInput����F���擾
                    SetColor(collision.gameObject, ADDITION);
                    efflight.GetComponent<ColorLight_Script1>().SetLight(color);
                    if (this.gameObject.GetComponent<ClickObj>() != null)
                    {
                        this.gameObject.GetComponent<ClickObj>().SetColor(new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], (byte)200));
                    }
                    //���g�̒E�F���s������ɁAMixColorObj�����Clear����Obj�ƐڐG���Ă邩�m�F��
                    //�ڐG���Ă���F������
                    if (mixObj_hit)
                    {
                        MixObj.GetComponent<Base_Color_Script>().SetColor(color, ADDITION);
                        MixObj.GetComponent<Base_Color_Script>().SetColorChange(true);
                    }
                    else if (clearObj_hit)
                    {
                        ClearObj.GetComponent<Base_Color_Script>().SetColor(color, ADDITION);
                        ClearObj.GetComponent<Base_Color_Script>().SetColorChange(true);
                    }
                }
            }
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        //OutputColor����F���̂Ă�
        if (collision.gameObject.tag == "ColorInput")
        {
            //���g�̒E�F���s���O�ɁAMixColorObj�����Clear����Obj�ƐڐG���Ă邩�m�F��
            //�ڐG���Ă����ɒE�F�������s��
            if (mixObj_hit)
            {
                MixObj.GetComponent<MixColor_Script>().Decolorization(color);
            }
            else if (clearObj_hit)
            {
                ClearObj.GetComponent<Base_Color_Script>().SetColor(color, SUBTRACTION);
                ClearObj.GetComponent<Base_Color_Script>().SetColorChange(true);
            }
            energization = false;
            colorchange_signal = false;
            SetColor(color, SUBTRACTION);
            efflight.GetComponent<ColorLight_Script1>().SetLight(color);

        }
        else if (collision.gameObject.tag == "ColorMix")
        {
            collision.gameObject.GetComponent<MixColor_Script>().Decolorization(color);
        }
        else if(collision.gameObject.tag == "ColorOutput")
        {
            Debug.Log(this.gameObject.name);
            if(cnt<collision.gameObject.GetComponent<OutputColor_Script>().GetPrecedence())
            {
                collision.gameObject.GetComponent<OutputColor_Script>().SetEnergization(false);
                collision.gameObject.GetComponent<OutputColor_Script>().SetColor(collision.gameObject.GetComponent<OutputColor_Script>().GetColor(), SUBTRACTION);
                efflight.GetComponent<ColorLight_Script1>().SetLight(color);
                if (this.gameObject.GetComponent<ClickObj>() != null)
                {
                    this.gameObject.GetComponent<ClickObj>().SetColor(new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], (byte)200));
                }
            }
        }
    }
}
