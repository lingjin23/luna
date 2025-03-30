using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BattleController : MonoBehaviour
{
    public Animator lunaAnimator;
    public Transform lunaTrans;
    public Transform monsterTrans;
    private Vector3 monsterInitPos;
    private Vector3 lunaInitPos;
    public SpriteRenderer monsterSr;
    public SpriteRenderer lunaSr;
    public GameObject skillEffectGo;
    public GameObject healEffectGo;

    public AudioClip attackSound;
    public AudioClip lunaAttackSound;
    public AudioClip monsterAttackSound;
    public AudioClip skillSound;
    public AudioClip recoverSound;
    public AudioClip hitSound;
    public AudioClip monsterHitSound;
    public AudioClip dieSound;


    // Start is called before the first frame update
    private void Awake()
    {
        monsterInitPos = monsterTrans.localPosition;
        lunaInitPos = lunaTrans.localPosition;
        
    }
    private void OnEnable(){
        monsterSr.DOFade(1,0.01f);
        lunaSr.DOFade(1,0.01f);
        monsterTrans.localPosition = monsterInitPos;
        lunaTrans.localPosition = lunaInitPos;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 绑定按钮的各个函数
    /// </summary>
    public void LunaAttack(){
        StartCoroutine(PerformAttackLogic());
    }
    public void LunaDefend(){
        StartCoroutine(PerformDefendLogic());
    }
    public void LunaUseSkill(){
        if(GameManager.Instance.CanUsePlayerMP(30)){
            StartCoroutine(PerformSkillLogic());
        }
        else return;
        
    }

    public void LunaRecoverHP(){
        if(GameManager.Instance.CanUsePlayerMP(50)){
            StartCoroutine(PerformRecoverHPLogic());
        }
        else return;
        
    }

    /// <summary>
    /// 协程管理回合制战斗
    /// </summary>
    /// <returns></returns>

    IEnumerator PerformAttackLogic(){
        UIManager.Instance.ShowOrHideBattlePanel(false);
        lunaAnimator.SetBool("MoveState",true);
        lunaAnimator.SetFloat("MoveValue",1);
        bool monsterLive = true;
        lunaTrans.DOLocalMove(monsterInitPos+new Vector3(-1.5f,0,0),0.5f).OnComplete(
            () => 
            {
                GameManager.Instance.PlaySound(attackSound);
                GameManager.Instance.PlaySound(lunaAttackSound);
                lunaAnimator.SetBool("MoveState",false);
                lunaAnimator.SetFloat("MoveValue",0.0f);
                lunaAnimator.CrossFade("Attack",0);
                DOVirtual.DelayedCall(0.3f,()=>{
                    monsterSr.DOFade(0.3f,0.2f).OnComplete(()=>{
                        GameManager.Instance.PlaySound(monsterHitSound);
                        monsterLive = JudgeMonsterHP(-20);
                    });
                });
                    
               
            }
        );
        yield return new WaitForSeconds(1.167f);
        lunaAnimator.SetBool("MoveState",true);
        lunaAnimator.SetFloat("MoveValue",-1);
        lunaTrans.DOLocalMove(lunaInitPos,0.5f).OnComplete(
            () => 
            {
                lunaAnimator.SetBool("MoveState",false);
                
            }
        );
        yield return new WaitForSeconds(0.5f);
        if(monsterLive) StartCoroutine(MonsterAttack());
    }

    IEnumerator PerformDefendLogic(){
        UIManager.Instance.ShowOrHideBattlePanel(false);
        lunaAnimator.SetBool("Defend",true);

        monsterTrans.DOLocalMove(lunaInitPos+new Vector3(1.5f,0,0),0.5f);

        yield return new WaitForSeconds(0.5f);
        monsterTrans.DOLocalMove(lunaInitPos+new Vector3(0.5f,0,0),0.2f).OnComplete(
            () => 
            {
                GameManager.Instance.PlaySound(monsterAttackSound);
                monsterTrans.DOLocalMove(lunaInitPos+new Vector3(1.5f,0,0),0.2f);
                lunaTrans.DOLocalMove(lunaInitPos+new Vector3(-0.5f,0,0),0.2f).OnComplete(()=>{
                    lunaTrans.DOLocalMove(lunaInitPos,0.2f);
                });

            }
        );
        yield return new WaitForSeconds(0.4f);
        monsterTrans.DOLocalMove(monsterInitPos,0.5f).OnComplete(
            ()=>{
                UIManager.Instance.ShowOrHideBattlePanel(true);
                lunaAnimator.SetBool("Defend",false);
            }
        );

    }

    IEnumerator PerformSkillLogic(){
        UIManager.Instance.ShowOrHideBattlePanel(false);
        lunaAnimator.CrossFade("Skill",0);
        GameManager.Instance.AddOrDecreaseMP(-30f);
        yield return new WaitForSeconds(0.35f);
        GameObject go = Instantiate(skillEffectGo,monsterTrans);
        go.transform.localPosition = Vector3.zero;
        GameManager.Instance.PlaySound(lunaAttackSound);
        GameManager.Instance.PlaySound(skillSound);
        yield return new WaitForSeconds(0.4f);
        GameManager.Instance.PlaySound(monsterHitSound);
        bool monsterLive = true;
        monsterSr.DOFade(0.3f,0.2f).OnComplete(()=>{
            monsterLive = JudgeMonsterHP(-40);
        });
        yield return new WaitForSeconds(0.4f);
        if(monsterLive) StartCoroutine(MonsterAttack());

    }
    IEnumerator PerformRecoverHPLogic(){
        UIManager.Instance.ShowOrHideBattlePanel(false);
        lunaAnimator.CrossFade("RecoverHP",0);
        GameManager.Instance.AddOrDecreaseMP(-50f);
        GameManager.Instance.PlaySound(lunaAttackSound);
        GameManager.Instance.PlaySound(recoverSound);
        yield return new WaitForSeconds(0.1f);
        GameObject go = Instantiate(healEffectGo,lunaTrans);
        go.transform.localPosition = Vector3.zero;
        
        GameManager.Instance.AddOrDecreaseHP(40f);
        yield return new WaitForSeconds(0.4f);
        // monsterSr.DOFade(0.3f,0.2f).OnComplete(()=>{
        //     JudgeMonsterHP(40);
        // });
        // yield return new WaitForSeconds(0.4f);
        StartCoroutine(MonsterAttack());

        
    }


    IEnumerator MonsterAttack(){
        monsterTrans.DOLocalMove(lunaInitPos+new Vector3(1.5f,0,0),0.5f);

        yield return new WaitForSeconds(0.5f);
        monsterTrans.DOLocalMove(lunaInitPos+new Vector3(0.5f,0,0),0.2f).OnComplete(
            () => 
            {
                GameManager.Instance.PlaySound(monsterAttackSound);
                monsterTrans.DOLocalMove(lunaInitPos+new Vector3(1.5f,0,0),0.2f);
                lunaAnimator.CrossFade("Hit",0);
                GameManager.Instance.PlaySound(hitSound);
                lunaSr.DOFade(0.3f,0.2f).OnComplete(()=>{
                    lunaSr.DOFade(1,0.2f);

                });
                JudgePlayerHP(-20);
            }
        );
        yield return new WaitForSeconds(0.4f);
        monsterTrans.DOLocalMove(monsterInitPos,0.5f).OnComplete(
            ()=>{
                UIManager.Instance.ShowOrHideBattlePanel(true);
            }
        );
        
    }
    
    

    private void JudgePlayerHP(int value){
        GameManager.Instance.AddOrDecreaseHP((float)value);
        if(GameManager.Instance.lunaCurrentHP<=0){
            lunaAnimator.CrossFade("Die",0);
            GameManager.Instance.PlaySound(dieSound);
            lunaSr.DOFade(0,0.8f).OnComplete(()=>{
               GameManager.Instance.EnterOrExitBattle(false); 
            });
        }
    }


    

    private bool JudgeMonsterHP(int value){
        GameManager.Instance.AddOrDecreaseMonsterHP(value);
        if(GameManager.Instance.monsterCurrentHP<=0){
            monsterSr.DOFade(0,0.8f).OnComplete(()=>{
                GameManager.Instance.EnterOrExitBattle(false,1); 
            //    Destroy(gameObject,DestroyTime);
            });
            return false;
        }
        else monsterSr.DOFade(1,0.2f);
        return true;
    }

    public void LunaEscape(){
        lunaTrans.DOLocalMove(lunaInitPos+new Vector3(-5,0,0),0.5f).OnComplete(()=>{
            GameManager.Instance.EnterOrExitBattle(false);
        });
        lunaAnimator.SetBool("MoveState",true);
        lunaAnimator.SetFloat("MoveValue",-1);
    }

}
