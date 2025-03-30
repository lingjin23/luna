using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LunaController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rigidbody2d;
    public float moveSpeed = 3;
    
    private Animator animator;
    private Vector2 move;
    private Vector2 lookDirection = new Vector2(0, 0);
    private float moveScale; //�ƶ�ϵ��

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        
        animator = GetComponentInChildren<Animator>();
        // animator.SetFloat("MoveValue", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.EnterBattle){
            return;
        }
        if(!GameManager.Instance.CanControlLuna){
            return ;
        }
        //����������
        // float horizontal = Input.GetAxis("Horizontal");
        // float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        move=new Vector2(horizontal, vertical);
        //�ӽǷ�λΪ���ж�
        if (!Mathf.Approximately(move.x, 0) || !Mathf.Approximately(move.y, 0))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        //��������
        animator.SetFloat("LookX", lookDirection.x);
        animator.SetFloat("LookY", lookDirection.y);
        moveScale = move.magnitude;
        if (move.magnitude > 0)
        {
            if (Input.GetKey(KeyCode.LeftShift)) //��
            {
                moveScale = 2;
                moveSpeed = 5;
            }
            else //��
            {
                moveScale = 1;
                moveSpeed = 3;
            }
        }
        //Debug.Log(Input.GetKeyDown(KeyCode.LeftShift));
        animator.SetFloat("MoveValue", moveScale);
        //����Ƿ���NPC�Ի�
        if(Input.GetKeyDown(KeyCode.Space)){
            Talk();
        }


    }

    private void FixedUpdate()
    {
        if(GameManager.Instance.EnterBattle){
            return;
        }
        //�ƶ�
        Vector2 position = transform.position;
        position += moveSpeed * move * Time.fixedDeltaTime;
        //transform.position = position;
        rigidbody2d.MovePosition(position);
    }
    public void climb(bool start){
        animator.SetBool("Climb",start);

    }
    public void Jump(bool start){
        animator.SetBool("Jump",start);
        rigidbody2d.simulated = !start;
    }

    /// <summary>
    /// �Ի�
    /// </summary>
    public void Talk()
    {
        Collider2D collider = Physics2D.OverlapCircle(rigidbody2d.position,0.5f, LayerMask.GetMask("NPC"));
        if (collider != null)
        {
            if (collider.name == "Nala")
            {
                // Debug.Log("Aa?");
                GameManager.Instance.CanControlLuna = false;
                collider.GetComponent<NPCDialog>().DisplayDialog();
            }
            else if (collider.name == "Dog"&& !GameManager.Instance.HasPetTheDog && GameManager.Instance.DialogInfoIndex == 2)
            {
                // Debug.Log("Aaa?");
                PetTheDog();
                GameManager.Instance.CanControlLuna = false;
                collider.GetComponent<Dog>().BeHappy();
            }
        }
    }
     /// <summary>
    /// ��������
    /// </summary>
    private void PetTheDog()
    {
        // Debug.Log("Aaaaa?");
        animator.CrossFade("Pet", 0);
        transform.position = new Vector3(-0.46f,-7.98f,  0);
    }
    

    
}
