using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    
    public float lunaHP = 100; //最大生命值
    public float lunaCurrentHP; //当前生命值
    public float lunaMP = 100;
    public float lunaCurrentMP; 
    public GameObject BattleMonsterGo;
    public GameObject MonstersGo;
    public bool IsFighting;
    public bool EnterBattle;


    public int DialogInfoIndex;
    public bool CanControlLuna;
    public bool HasPetTheDog;
    public int CandleNum;
    public NPCDialog NPC;
    public int KillNum;

    public AudioSource audioSource;
    public AudioClip NormalClip;
    public AudioClip BattleClip;
    

    /// <summary>
    /// 怪物状态
    /// </summary>
    public int monsterHP = 50; //最大生命值
    public int monsterCurrentHP;
    public GameObject battleGo;//战斗场景
    private void Awake(){
        Instance = this;
        IsFighting = false;
        lunaCurrentHP = 100f;
        lunaCurrentMP = 100f;

    
    }


    private void Update()
    {
        if (!EnterBattle)
        {
            if (lunaCurrentMP <= 100)
            {
                AddOrDecreaseMP(Time.deltaTime);
            }
            if (lunaCurrentHP <= 100)
            {
                AddOrDecreaseHP(Time.deltaTime);
            }
        }
    }





    /// <summary>
    /// 改变玩家血量
    /// </summary>
    /// <param name="amount"></param>
    public void AddOrDecreaseHP(float amount)
    {
        lunaCurrentHP = Mathf.Clamp(lunaCurrentHP + amount, 0, lunaHP);
        UIManager.Instance.SetHPValue((float)lunaCurrentHP/lunaHP);
        // Debug.Log(lunaCurrentHP + "/" + lunaHP);
    }
    /// <summary>
    /// 改变玩家蓝量
    /// </summary>
    /// <param name="amount"></param>
    public void AddOrDecreaseMP(float amount)
    {
        lunaCurrentMP = Mathf.Clamp(lunaCurrentMP + amount, 0, lunaMP);
        UIManager.Instance.SetMPValue((float)lunaCurrentMP/lunaMP);
        // Debug.Log(lunaCurrentHP + "/" + lunaHP);
    }


    /// <summary>
    /// 进入战斗窗口
    /// </summary>
    /// <param name="enter"></param>

    /// <summary>
    /// 进入战斗状态
    /// </summary>
    /// <param name="enter"></param>
    /// <param name="addKillNum"></param>
    public void EnterOrExitBattle(bool enter = true,int addKillNum = 0)
    {
        UIManager.Instance.ShowOrHideBattlePanel(enter);
        battleGo.SetActive(enter);
        if (!enter)  //非战斗状态，或者说战斗结束
        {
            KillNum += addKillNum;
            if (addKillNum > 0)
            {
                DestoryMonster();
            }
            monsterCurrentHP = 50;
            PlayMusic(NormalClip);
            if (lunaCurrentHP <= 0)
            {
                lunaCurrentHP = 100;
                lunaCurrentMP = 0;
                BattleMonsterGo.transform.position += new Vector3(0, 2, 0);
            }
            if(KillNum >= 3)
            {
                GameManager.Instance.SetContentIndex();
            }
        }
        else
        {
            PlayMusic(BattleClip);
        }
        EnterBattle = enter;
    }

    public void DestoryMonster()
    {
        Destroy(BattleMonsterGo);
    }

    public void SetMonster(GameObject go)
    {
        BattleMonsterGo = go;
    }

    /// <summary>
    /// 显示怪物
    /// </summary>
    public void ShowMonsters()
    {
        if (!MonstersGo.activeSelf)
        {
            MonstersGo.SetActive(true);
        }
    }

    /// <summary>
    /// 判断是否可以使用技能
    /// </summary>
    /// <param name="value">技能耗费蓝量</param>
    /// <returns></returns>
    public bool CanUsePlayerMP(int value){
        return lunaCurrentMP>=value;

    }


    /// <summary>
    /// 改变怪物血量
    /// </summary>
    /// <param name="amount"></param>
    public void AddOrDecreaseMonsterHP(int amount)
    {
        monsterCurrentHP = Mathf.Clamp(monsterCurrentHP + amount, 0, monsterHP);
        // Debug.Log(lunaCurrentHP + "/" + lunaHP);
    }

    /// <summary>
    /// 任务完成设置索引
    /// </summary>
    public void SetContentIndex()
    {
        // Debug.Log("haole?");
        NPC.SetContentIndex();
    }


    public void PlayMusic(AudioClip audioClip)
    {
        if (audioSource.clip != audioClip)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }

    public void PlaySound(AudioClip audioClip)
    {
        if (audioClip)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
}
