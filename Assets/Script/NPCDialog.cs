using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*****************************************
//创建人： Trigger 
//功能说明：NPC談話
//***************************************** 
public class NPCDialog : MonoBehaviour
{
    private List<DialogInfo[]> dialogInfoList;
    public int contentIndex;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        dialogInfoList = new List<DialogInfo[]>()
        {
            new DialogInfo[]{
                new DialogInfo() {name="Luna",content="(,,･∀･)ﾉ゛hello，我是LuNa，你可以用上下左右控制我移动，空格键与NPC进行对话，战斗中需要简单点击按钮执行相应行为" }
            },
            new DialogInfo[]{
                new DialogInfo() {name="Nala",content="好久不见了，小猫咪(*ΦωΦ*)，Luna~" },
                new DialogInfo() {name="Luna",content="好久不见，Nala,你还是那么有活力，哈哈" },
                new DialogInfo() {name="Nala",content="还好吧~" },
                new DialogInfo() {name="Nala",content="我的狗一直在叫，但是我这会忙不过来，你能帮我安抚一下它吗？" },
                new DialogInfo() {name="Luna",content="啊？" },
                new DialogInfo() {name="Nala",content="(,,´•ω•)ノ(´っω•｀。)摸摸他就行，摸摸说呦西呦西，真是个好孩子呐" },
                new DialogInfo() {name="Nala",content="别看他叫的这么凶，其实他就是想引起别人的注意" },
                new DialogInfo() {name="Luna",content="可是。。。。" },
                new DialogInfo() {name="Luna",content="我是猫女郎啊" },
                new DialogInfo() {name="Nala",content="安心啦，不会咬你哒，去吧去吧~" },
            },
            new DialogInfo[]{
                new DialogInfo() {name="Nala",content="他还在叫呢" }
            },
            //3
            new DialogInfo[]{
                new DialogInfo() {name="Nala",content="感谢你呐，Luna，你还是那么可靠！" },
                new DialogInfo() {name="Nala",content="我想请你帮个忙好吗" },
                new DialogInfo() {name="Nala",content="说起来这事怪我。。。" },
                new DialogInfo() {name="Nala",content="今天我睡过头了，出门比较匆忙" },
                new DialogInfo() {name="Nala",content="然后装蜡烛的袋子口子没封好!o(╥﹏╥)o" },
                new DialogInfo() {name="Nala",content="结果就。。。蜡烛基本丢完了" },
                new DialogInfo() {name="Luna",content="你还是老样子，哈哈。。" },
                new DialogInfo() {name="Nala",content="所以，所以喽，你帮帮忙，帮我把蜡烛找回来" },
                new DialogInfo() {name="Nala",content="如果你能帮我找回全部的5根蜡烛，我就送你一把神器" },
                new DialogInfo() {name="Luna",content="神器？(¯﹃¯)" },
                new DialogInfo() {name="Nala",content="是的，我感觉很适合你，加油呐~" },
            },
            new DialogInfo[]{
                new DialogInfo() {name="Nala",content="你还没帮我收集到所有的蜡烛，宝~" },
            },
            //5
            new DialogInfo[]{
                new DialogInfo() {name="Nala",content="可靠啊！竟然一个不差的全收集回来了" },
                new DialogInfo() {name="Luna",content="你知道多累吗？" },
                new DialogInfo() {name="Luna",content="你到处跑，真的很难收集" },
                new DialogInfo() {name="Nala",content="辛苦啦辛苦啦" },
                new DialogInfo() {name="Nala",content="这是给你的奖励" },
                new DialogInfo() {name="Nala",content="蓝纹火锤，传说中的神器" },
                new DialogInfo() {name="Nala",content="应该挺适合你的" },
                new DialogInfo() {name="Luna",content="~~获得蓝纹火锤~~（遇到怪物可触发战斗）" },
                new DialogInfo() {name="Luna",content="哇，谢谢你！Thanks♪(･ω･)ﾉ" },
                new DialogInfo() {name="Nala",content="嘿嘿(*^▽^*)，咱们的关系不用客气" },
                new DialogInfo() {name="Nala",content="正好，最近山里出现了一堆怪物，你也算为民除害，帮忙清理5只怪物" },
                new DialogInfo() {name="Luna",content="啊？" },
                new DialogInfo() {name="Luna",content="这才是你的真实目的吧？！" },
                new DialogInfo() {name="Nala",content="拜托拜托啦，否则真的很不方便我卖东西" },
                new DialogInfo() {name="Luna",content="无语中。。。" },
                new DialogInfo() {name="Nala",content="求求你了，啵啵~" },
                new DialogInfo() {name="Luna",content="哎，行吧，谁让你漂亮呢~" },
                new DialogInfo() {name="Nala",content="嘻嘻，那辛苦宝子啦" }
            },
            new DialogInfo[]{
                new DialogInfo() {name="Nala",content="宝，你还没清理干净呢,这样我不方便嘛~" },
            },
            new DialogInfo[]{
                new DialogInfo() {name="Nala",content="真棒，luna，周围的居民都会十分感谢你的，有机会来我家喝一杯吧~" },
                new DialogInfo() {name="Luna",content="我觉得可行，哈哈~" }
            },
            new DialogInfo[]{
                new DialogInfo() {name="Nala",content="改天再见喽~" },
            }
        };
        GameManager.Instance.DialogInfoIndex = 0;
        contentIndex = 1;
    }
    /// <summary>
    /// 显示对话内容
    /// </summary>
    public void DisplayDialog()
    {
        if (GameManager.Instance.DialogInfoIndex > 7)
        {
            return;
        }
        if (contentIndex >= dialogInfoList[GameManager.Instance.DialogInfoIndex].Length)
        {
            if (GameManager.Instance.DialogInfoIndex == 2 &&
                !GameManager.Instance.HasPetTheDog)
            {

            }
            else if (GameManager.Instance.DialogInfoIndex == 4 &&
                GameManager.Instance.CandleNum < 5)
            {

            }
            else if (GameManager.Instance.DialogInfoIndex == 6 &&
                GameManager.Instance.KillNum < 3)
            {

            }
            else
            {
                GameManager.Instance.DialogInfoIndex++;
            }
            if (GameManager.Instance.DialogInfoIndex == 6)
            {
                GameManager.Instance.ShowMonsters();
            }

            //当前这段对话结束了，可以开始控制luna
            contentIndex = 0;
            UIManager.Instance.ShowDialog();
            GameManager.Instance.CanControlLuna = true;
        }
        else
        {
            DialogInfo dialogInfo = dialogInfoList[GameManager.Instance.DialogInfoIndex][contentIndex];
            UIManager.Instance.ShowDialog(dialogInfo.content, dialogInfo.name);
            contentIndex++;
            animator.SetTrigger("Talk");
        }
    }

    /// <summary>
    /// 任务完成设置索引
    /// </summary>
    public void SetContentIndex()
    {
        // Debug.Log("meishi?");
        contentIndex = dialogInfoList[GameManager.Instance.DialogInfoIndex].Length;
        // Debug.Log("meishiqqq?");
    }
}
/// <summary>
/// 类外的结构体 对话信息
/// </summary>
public struct DialogInfo
{
    public string name;
    public string content;
}