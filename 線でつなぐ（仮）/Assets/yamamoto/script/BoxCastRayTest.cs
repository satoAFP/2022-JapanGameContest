using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCastRayTest : MonoBehaviour
{

    [SerializeField]
    private Transform targetTra;    public GameObject Target;//���C���Փ˂��Ă���I�u�W�F�N�g������   

    [SerializeField] private int getsize; //�I�u�W�F�N�g���E�������Ɏ�Ɏ��܂�T�C�Y�ɂ���

    [SerializeField] private Vector3 TargetPos; //�I�u�W�F�N�g���E�������Ɏ�Ɏ��܂�ʒu

    [SerializeField] private Vector3 TargetRor; //�I�u�W�F�N�g���E�������Ɏ�Ɏ��܂��]

    private Vector3 TargetScale;//�^�[�Q�b�g�̌��̑傫��

    private Vector3 TargetRotate;//�^�[�Q�b�g�̌��̊p�x

    //�@�^�[�Q�b�g�Ƃ̋���
  //  private float distanceFromTargetObj;


    public GameObject Cancel;//�I���L�����Z���p�̕ϐ�

    public bool grab;//�͂݃t���O

    private bool setlineblock = false;//ClickObj��Nosetline�̎󂯎��t���O

    private bool Pause = false;//�ꎞ���f�t���O

    //�A���ŉ�����Ȃ����߂̔���
    private bool key_check_E = true;


    [System.NonSerialized]
    public bool Existence_Check = false;//����u���b�N���݃t���O

    [System.NonSerialized]
    public bool  NosetLight = false;//���C�g�I�u�W�F�N�g�̏�ɃI�u�W�F�N�g��u�����Ȃ�

    // ����u���b�N�v���n�u�i�[�p
    public GameObject Judgeblock;

    private bool first_setblock = true;//����u���b�N���}�b�v�`�b�v�Ƀ��C�����������������ݒu

    private GameObject Memmapcip=null;//���ݑI�����Ă���}�b�v�`�b�v�L��

    private GameObject cloneblock = null;//�������ꂽ����u���b�N������

    private bool second_set = false;//���݃��C���������Ă���}�b�v�`�b�v�œ��ڈȍ~�u���Ƃ��̃t���O

    public AudioClip get_se;//�擾SE

    public AudioClip set_se;//�ݒuSE
    AudioSource audioSource;

    Vector3 worldPos;//�}�b�v�`�b�v�̍��W�ۑ��p�ϐ�

    private bool ray_Mapcip = false;//�}�b�v�`�b�v�����C�ɓ������Ă�����A�u���b�N�}�b�v�`�b�v�̏�����ʂ��Ȃ�

    void Start()
    {
        grab = false;//������

        //Component���擾
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        //�@�^�[�Q�b�g�Ƃ̋���
       //distanceFromTargetObj = Vector3.Distance(transform.position, targetTra.position);

        RaycastHit hit;

        RaycastHit hit2;//2�{�ڂ̃��C��hit����

        Ray ray = new Ray(transform.position, transform.forward);//���C�̐ݒ�

        Ray ray2 = new Ray(transform.position, transform.forward);//2�{�ڂ̃��C�̐ݒ�i�}�b�v�`�b�v�̏����̎��Ɏg�p�j

        //E�L�[�Ńu���b�N�̎擾�A�ݒu���ł��Ȃ��悤�ɂ���
        //�ēxE�L�[�������ƁA�擾���ݒu����
        if (Input.GetKey(KeyCode.E))
        {
            if (key_check_E)
            {
                if (Pause)
                {
                    Pause = false;
                }
                else
                {
                    Pause = true;//Pause��true���ƃu���b�N�̐ݒuor�擾���ł��Ȃ��悤�ɂ���
                }
            }
            key_check_E = false;
        }
        else { key_check_E = true; }

        //�ǂɃ��C���ڐG���Ă��邩(�ڐG���Ă����瑼�̃I�u�W�F�N�g�Ƃ̃��C�̏������s��Ȃ��j
        if (Physics.Raycast(ray, out hit, 4.0f, LayerMask.GetMask("Wall")) || Physics.Raycast(ray, out hit, 4.0f, LayerMask.GetMask("Door")))
        {
            Debug.Log("Wall");
        }
        //Cube�̃��C���΂��^�[�Q�b�g�ƐڐG���Ă��邩����
        else if (grab==false)
        {
            if (Physics.Raycast(ray, out hit, 3.0f, LayerMask.GetMask("Target")) && !Pause)
            {
                Debug.Log(hit.transform.name);

                hit.collider.gameObject.GetComponent<ClickObj>().ChangeMaterial();//���C�����������Ƃ���ɐF�t��

                // Debug.Log(hit.transform.position.y);

                Cancel = hit.collider.gameObject;//���C������������I�u�W�F�N�g���擾����i�����I�u�W�F�N�g����N���b�N�őI�������������邽�߁j

                //�V�����_�[�̏�ɒu����u���b�N�̏ꍇ�A�V�����_�[�ݒuON
                if(hit.collider.GetComponent<ClickObj>().Setlineblock == true)
                {
                    setlineblock = true;
                }

                //���N���b�N���ꂽ�Ƃ��Ƀ��C�ƐڐG���Ă���I�u�W�F�N�g�̍��W��Target�ɓ����
                if (Input.GetMouseButtonDown(0) && grab == false)
                {
                    Target = hit.collider.gameObject;

                    audioSource.PlayOneShot(get_se);//�u���b�N�擾SE

                    //��Ɏ��p�ɃI�u�W�F�N�g�̃T�C�Y�Ɖ�]���L��
                    TargetScale = Target.transform.localScale;
                    TargetRotate = Target.transform.eulerAngles;

                    //�J�����̎q�I�u�W�F�N�g�ɂ���
                    Target.transform.parent = gameObject.transform;

                    //��Ɏ��p�ɃI�u�W�F�N�g�̃T�C�Y�Ɖ�]�̕ύX
                    Target.transform.localScale /= getsize;
                    Target.transform.localEulerAngles = TargetRor;
                    Target.transform.localPosition = TargetPos;
                    Target.GetComponent<BoxCollider>().isTrigger = true;
                    Target.GetComponent<Rigidbody>().isKinematic = true;

                    grab = true;//�͂݃t���O��true
                    Cancel = Target;//�L�����Z������I�u�W�F�N�g��ݒ�
                }
               
                ////�E�N���b�N�ŃI�u�W�F�N�g����]
                //else if (Input.GetMouseButtonDown(1))
                //{
                //    hit.collider.gameObject.transform.eulerAngles += new Vector3(0.0f, 90.0f, 0.0f);
                //}

            }
        }

     
        //�}�b�v�`�b�v�Ƀ��C���ڐG���Ă��邩����(ray����ɕύX�j
        else if (Physics.Raycast(ray, out hit, 4.0f, LayerMask.GetMask("Mapcip")) && !Pause)
        {

            
            //2�{�ڂ̃��C��target���C���[
            if (Physics.Raycast(ray2, out hit2, 3.0f, LayerMask.GetMask("Target")) && !Pause && grab)
            {
                Existence_Check = false;
                Debug.Log("�Ȃ�Ŏ��@�ɋ@�B�����񂾂�");

            }
            else
            {
                Debug.Log("�����͂ǂ��Ȃ��Ă񂾋�����");
            }


            ray_Mapcip = true;
            worldPos = hit.collider.gameObject.transform.position;//�}�b�v�`�b�v�̍��W���擾����

            if(Memmapcip == hit.collider.gameObject && second_set == false)
            {
                if (first_setblock)
                {
                    //����u���b�N����
                    cloneblock = Instantiate(Judgeblock, new Vector3(worldPos.x, worldPos.y += 0.25f, worldPos.z), Quaternion.identity);
                    first_setblock = false;
                }
              
              
            }
            else
            {
                //�Ⴄ���C���[���}�b�v�`�b�v�Ƀ��C��������ƃt���O���������O�̈ʒu�ɂ�������u���b�N�폜
                second_set = false;
                first_setblock = true;
                Existence_Check = false;
                Destroy(cloneblock);
            }

           



            if (grab == true)
            {
                hit.collider.gameObject.GetComponent<MapcipSlect>().ChangeMaterial();//�͂�ł�Ƃ��̂ݑI���̏ꏊ�ɐF���o��
            }

            //���N���b�N���ꂽ�Ƃ��Ƀ}�b�v�`�b�v�̍��W��Target�ɏ㏑������
            if (Input.GetMouseButtonDown(0) && grab == true && hit.collider.gameObject.GetComponent<MapcipSlect>().Onplayer==false)
            {
                //���̏�ɒu����I�u�W�F�N�g���ǂ������f���Ēu���鏈����ύX
                //���̎��Ƀ��C�g�I�u�W�F�N�g�����邩���f����A����Βu���Ȃ��悤�ɂ���
                //���̏�ɒu����
                if (setlineblock && hit.collider.gameObject.GetComponent<MapcipSlect>().Onobj == false)
                {
                  //  Debug.Log("����1");
                    //�}�b�v�`�b�v�̏�ɃI�u�W�F�N�g���u���Ă��Ȃ����̂݃I�u�W�F�N�g��ݒu����
                    if (hit.collider.gameObject.GetComponent<MapcipSlect>().Onblock == false)
                    {
                       // Debug.Log("����2");
                        //�}�b�v�`�b�v�̍��������ȏ�̎��I�u�W�F�N�g��u�������̍����𒲐�����

                        audioSource.PlayOneShot(set_se);//�ݒuSE

                        //worldPos.y += Target.transformr.localPosition.y;
                        //worldPos.y += Target.transform.localScale.y / 2;//Y�����Œ肷��
                        worldPos.y += 0.5f;//Y�����Œ肷��

                        //��Ɏ������I�u�W�F�N�g�����̑傫���ɖ߂�
                        Target.gameObject.transform.parent = null;
                        Target.transform.localScale = TargetScale;
                        Target.transform.localEulerAngles = TargetRotate;
                        //��Ɏ������I�u�W�F�N�g�̓����蔻��𕜊�������
                        Target.GetComponent<BoxCollider>().isTrigger = false;
                        Target.GetComponent<Rigidbody>().isKinematic = false;

                        Target.transform.position = worldPos;
                        Target = null;//�^-�Q�b�g�̏�����
                        grab = false;//�͂݃t���O��false
                        setlineblock = false;//���̏�ɒu����I�u�W�F�N�g�ݒ��������
                        second_set = true;//���݂̃}�b�v�`�b�v�łQ��ڂ̃u���b�N��u������
                    }
                }
                //���̏�ɒu���Ȃ�
                else
                {
                   // Debug.Log("����3");
                    //�}�b�v�`�b�v�ɂ���I�u�W�F�N�g�𔻒f����A����Βu���Ȃ��悤�ɂ���
                    if (!Existence_Check)
                    {
                        //Debug.Log("����4");
                        //�}�b�v�`�b�v�̏�ɃI�u�W�F�N�g���u���Ă��Ȃ����̂݃I�u�W�F�N�g��ݒu����
                        if (hit.collider.gameObject.GetComponent<MapcipSlect>().Onblock == false)
                        {

                            audioSource.PlayOneShot(set_se);//�ݒuSE

                          //  Debug.Log("����5");
                            //�}�b�v�`�b�v�̍��������ȏ�̎��I�u�W�F�N�g��u�������̍����𒲐�����

                            //worldPos.y += Target.transformr.localPosition.y;
                            //worldPos.y += Target.transform.localScale.y / 2;//Y�����Œ肷��
                            worldPos.y += 0.5f;//Y�����Œ肷��

                            //��Ɏ������I�u�W�F�N�g�����̑傫���ɖ߂�
                            Target.gameObject.transform.parent = null;
                            Target.transform.localScale = TargetScale;
                            Target.transform.localEulerAngles = TargetRotate;
                            //��Ɏ������I�u�W�F�N�g�̓����蔻��𕜊�������
                            Target.GetComponent<BoxCollider>().isTrigger = false;
                            Target.GetComponent<Rigidbody>().isKinematic = false;

                            Target.transform.position = worldPos;
                            Target = null;//�^-�Q�b�g�̏�����
                            grab = false;//�͂݃t���O��false
                            setlineblock = false;//���̏�ɒu����I�u�W�F�N�g�ݒ��������
                            second_set = true;//���݂̃}�b�v�`�b�v�łQ��ڂ̃u���b�N��u������
                        }
                    }
                }
            }

            //���݂̑I�𒆂̃I�u�W�F�N�g�i�}�b�v�`�b�v�j���L��  
            Memmapcip = hit.collider.gameObject;

        }
        else 
        {
            ray_Mapcip = false;//���C����O����true
            Existence_Check = false;
        }

      
        



        //-----------�g���ĂȂ����C���[�̏����i�R�����g�����Ŏg�����I�j--------------------


        ////���}�b�v�`�b�v�Ƀ��C���ڐG���Ă��邩����(ray����ɕύX�j
        //else if (Physics.Raycast(ray, out hit, 4.0f, LayerMask.GetMask("Hole")))
        //{

        //    Vector3 worldPos = hit.collider.gameObject.transform.position;//�}�b�v�`�b�v�̍��W���擾����

        //    if (grab == true)
        //    {
        //        hit.collider.gameObject.GetComponent<MapcipSlect>().ChangeMaterial();//�͂�ł�Ƃ��̂ݑI���̏ꏊ�ɐF���o��
        //    }

        //    //���N���b�N���ꂽ�Ƃ��Ƀ}�b�v�`�b�v�̍��W��Target�ɏ㏑������
        //    if (Input.GetMouseButtonDown(0) && grab == true)
        //    {
        //        //�}�b�v�`�b�v�̏�ɃI�u�W�F�N�g���u���Ă��Ȃ����̂݃I�u�W�F�N�g��ݒu����
        //        if (hit.collider.gameObject.GetComponent<MapcipSlect>().Onblock == false)
        //        {
        //            //�}�b�v�`�b�v�̍��������ȏ�̎��I�u�W�F�N�g��u�������̍����𒲐�����

        //            //worldPos.y += Target.transformr.localPosition.y;
        //            //worldPos.y += Target.transform.localScale.y / 2;//Y�����Œ肷��
        //            worldPos.y -= 0.5f;//Y�����Œ肷��

        //            //��Ɏ������I�u�W�F�N�g�����̑傫���ɖ߂�
        //            Target.gameObject.transform.parent = null;
        //            Target.transform.localScale = TargetScale;
        //            Target.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        //            //��Ɏ������I�u�W�F�N�g�̓����蔻��𕜊�������
        //            Target.GetComponent<BoxCollider>().isTrigger = false;
        //            Target.GetComponent<Rigidbody>().isKinematic = false;

        //            Target.transform.position = worldPos;
        //            Target = null;//�^-�Q�b�g�̏�����
        //            grab = false;//�͂݃t���O��false
        //        }
        //    }
        //}

        ////���i�d�C�𗬂��I�u�W�F�N�g�j�Ƀ��C���ڐG���Ă��邩
        //else if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Plane")))
        //{
        //    //�E�N���b�N�ŃI�u�W�F�N�g����]
        //    if (Input.GetMouseButtonDown(1))
        //    {
        //        hit.collider.gameObject.transform.eulerAngles += new Vector3(0.0f, 90.0f, 0.0f);
        //    }
        //}


        //-----------------------------------------------------------------------------------------


        ////�u���b�N�������Ă��鎞�ɉ�]������
        //if (Input.GetMouseButtonDown(1) && grab == true && !Pause)
        //{
        //   // Debug.Log("���΂΂΂΂΂�");
        //    TargetRotate += new Vector3(0.0f, 90.0f, 0.0f);
        //}

        //�h�A�Ƀ��C���ڐG���Ă��邩����(ray����ɕύX�j
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Door")) && !Pause)
        {
            if (Input.GetMouseButtonDown(0) && grab == false)
            {
                hit.collider.gameObject.GetComponent<DoorOpoen>().RayOpenDoor();//�h�A���J����
                hit.collider.gameObject.GetComponent<DoorOpoen>().RayOpenWarpDoor();//���[�v�h�A���J����
            }
            

            hit.collider.gameObject.GetComponent<DoorOpoen>().RayTargetDoor();//�F�t��
        }

        //�{�^�������C�ɐڐG���Ă��邩����
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Button")) && !Pause)
        {
            if (Input.GetMouseButtonDown(0) && grab == false)
            {
                hit.collider.gameObject.GetComponent<button>().RayPushButton();//�{�^��������
            }
             
            hit.collider.gameObject.GetComponent<button>().RayTargetButton();//�F�t��
        }


    }

    //void OnDrawGizmos()
    //{
    //    //�@Cube�̃��C���^���I�Ɏ��o��
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawWireCube(transform.position + transform.forward * distanceFromTargetObj, Vector3.one);
    //}

}