using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool vertical;
    public float speed =5;
    //方向控制
    private int direction =1;
    //方向改变时间间隔
    public float changeTime =5;
    //计时器
    private float timer;
    //刚体组件引用，为了使用刚体进行移动
    private Rigidbody2D rigidbody2d;
    //动画控制器组件，为了播放动画
    private Animator animator;
    private bool MonsterMove;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
        MonsterMove = true;
        animator =GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update(){
        if(GameManager.Instance.EnterBattle){
            return;
        }
        timer -= Time.deltaTime;
        if(timer<0){
            direction =-direction;
            timer = changeTime;
        }
        rigidbody2d.simulated = !GameManager.Instance.IsFighting;
        
    }
    private void FixedUpdate(){
        if(GameManager.Instance.EnterBattle){
            return;
        }

        Vector2 pos= rigidbody2d.position;
        if(MonsterMove){
            // 垂直轴向移动
            if(vertical){
                animator.SetFloat("LookX", 0);
                animator.SetFloat("LookY", direction);
                pos.y = pos.y + speed * direction * Time.fixedDeltaTime;
            }
            //水平轴向移动
            else
            {
                animator.SetFloat("LookX", direction);
                animator.SetFloat("LookY", 0);
                pos.x= pos.x + speed * direction * Time.fixedDeltaTime;
            }
            // Debug.Log(" "+pos.x+pos.y);
            }
        
        rigidbody2d.MovePosition(pos);

    }
     private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Luna"))
        {
            // 获取当前物体的刚体并停止移动
            // Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rigidbody2d != null)
            {
                rigidbody2d.velocity = Vector2.zero; // 停止速度
                rigidbody2d.angularVelocity = 0;    // 停止旋转
            }
            Rigidbody2D lunaRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (lunaRb != null)
            {
                lunaRb.velocity = Vector2.zero; // 停止速度
                lunaRb.angularVelocity = 0;    // 停止旋转
            }

            GameManager.Instance.EnterOrExitBattle(true);
            GameManager.Instance.SetMonster(gameObject);
            MonsterMove = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Luna"))
        {
            Collider2D collider = GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = true;
            }
            MonsterMove = true;
        }
    }

}
