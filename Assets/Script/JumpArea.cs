using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class JumpArea : MonoBehaviour
{
    public Transform jumpPointA;
    public Transform jumpPointB;

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Luna")){
            LunaController lunaController= collision.transform.GetComponent<LunaController>();
            lunaController.Jump(true);
            float disA= Vector3.Distance(lunaController.transform.position,jumpPointA.position);
            float disB= Vector3.Distance(lunaController.transform.position, jumpPointB.position);
            Transform targetTrans;
            targetTrans =disA >disB ?jumpPointA : jumpPointB;
            lunaController.transform.DOMove(targetTrans.position, 0.5f).SetEase(Ease.Linear).OnComplete(() => {EndJump(lunaController);});
            // cout<<"hello world!";
            Transform lunaLocalTrans = lunaController.transform.GetChild(0);
            Sequence sequence = DOTween.Sequence();
            sequence.Append(lunaLocalTrans.DOLocalMoveY(1.5f,0.25f).SetEase(Ease.InOutSine));
            sequence.Append(lunaLocalTrans.DOLocalMoveY(0.61f,0.25f).SetEase(Ease.InOutSine));
            sequence.Play();
        }
    }
    private void EndJump(LunaController lunaController){
        lunaController.Jump(false);
    }
}
