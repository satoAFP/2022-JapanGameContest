using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCastRayTest : MonoBehaviour
{

    [SerializeField]
    private Transform targetTra;
    //�@�^�[�Q�b�g�Ƃ̋���
    private float distanceFromTargetObj;

    public GameObject Target;//���C���Փ˂��Ă���I�u�W�F�N�g������ 

    public GameObject Cancel;//�I���L�����Z���p�̕ϐ�

    public bool grab;//�͂݃t���O

    void Start()
    {
        grab = false;//������
    }

    // Update is called once per frame
    void Update()
    {
        //�@�^�[�Q�b�g�Ƃ̋���
        distanceFromTargetObj = Vector3.Distance(transform.position, targetTra.position);

        RaycastHit hit;
        //�@Cube�̃��C���΂��^�[�Q�b�g�ƐڐG���Ă��邩����
        if (Physics.BoxCast(transform.position, Vector3.one * 0.5f, transform.forward, out hit, Quaternion.identity, 100f, LayerMask.GetMask("Target")))
        {
            Debug.Log(hit.transform.name);

            Cancel = hit.collider.gameObject;//���C������������I�u�W�F�N�g���擾����i�����I�u�W�F�N�g����N���b�N�őI�������������邽�߁j

            //���N���b�N���ꂽ�Ƃ��Ƀ��C�ƐڐG���Ă���I�u�W�F�N�g�̍��W��Target�ɓ����
            if (Input.GetMouseButtonDown(0) && grab == false)
            {
                Target = hit.collider.gameObject;
                grab = true;//�͂݃t���O��true
                hit.collider.gameObject.GetComponent<ClickObj>().ChangeMaterial(1);//�F�t��
                Cancel = Target;//�L�����Z������I�u�W�F�N�g��ݒ�
            }
            //�ēx�����I�u�W�F�N�g��I���Ŏ�����Ԃ�����
            else if (Input.GetMouseButtonDown(0) && grab == true && Cancel == Target)//Target��Cancel
            {
                //Debug.Log("w");
                //�I�u�W�F�N�g�̏�����
                Target = null;
                Cancel = null;
                //�͂݃t���O��false
                grab = false;

                hit.collider.gameObject.GetComponent<ClickObj>().ChangeMaterial(0);//�F�t��
            }

        }

        //�}�b�v�`�b�v�Ƀ��C���ڐG���Ă��邩����(ray����ɕύX�j
        else if(Physics.BoxCast(transform.position, Vector3.one * 0.000005f, transform.forward, out hit, Quaternion.identity, 100f, LayerMask.GetMask("Mapcip")))
        {

            Vector3 worldPos = hit.collider.gameObject.transform.position;//�}�b�v�`�b�v�̍��W���擾����

            if (grab == true)
            {
                hit.collider.gameObject.GetComponent<MapcipSlect>().ChangeMaterial();//�͂�ł�Ƃ��̂ݑI���̏ꏊ�ɐF���o��
            }

            //���N���b�N���ꂽ�Ƃ��Ƀ}�b�v�`�b�v�̍��W��Target�ɏ㏑������
            if (Input.GetMouseButtonDown(0) && grab == true)
            {
                worldPos.y = 0.5f;//Y�����Œ肷��
                Target.transform.position = worldPos;
                Target.GetComponent<ClickObj>().ChangeMaterial(0);//�I��obj�̐F��߂�
                Target = null;//�^-�Q�b�g�̏�����
                grab = false;//�͂݃t���O��false
                
            }

            //Debug.Log(hit.transform.name);
           
        }
    }

    void OnDrawGizmos()
    {
        //�@Cube�̃��C���^���I�Ɏ��o��
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + transform.forward * distanceFromTargetObj, Vector3.one);
    }

}