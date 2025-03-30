using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Image hpMaskImage;
    public Image mpMaskImage;
    public float originalSize;//原始血条宽度
    public GameObject BattlePanelGo;
    public GameObject TaklPanel;
    public Text NameText;
    public Text ContentText;
    public Image CharacterImage;
    public Sprite[] CharacterSprtes;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        originalSize = hpMaskImage.rectTransform.rect.width;
    }
    /// <summary>
    /// 血条填充显示
    /// </summary>
    /// <param name="fillPercent">填充百分比</param>
    public void SetHPValue(float fillPercent){
        hpMaskImage.rectTransform.SetSizeWithCurrentAnchors(
            RectTransform.Axis.Horizontal,fillPercent*originalSize
        );
    }

     /// <summary>
    /// 蓝条填充显示
    /// </summary>
    /// <param name="fillPercent">填充百分比</param>
    public void SetMPValue(float fillPercent){
        mpMaskImage.rectTransform.SetSizeWithCurrentAnchors(
            RectTransform.Axis.Horizontal,fillPercent*originalSize
        );
    }
    public void ShowOrHideBattlePanel(bool show){
        BattlePanelGo.SetActive(show);
    }
     /// <summary>
    /// 显示对话内容（包含人物的切换，名字的更换，对话内容的更换）
    /// </summary>
    /// <param name="content"></param>
    /// <param name="name"></param>
    public void ShowDialog(string content = null, string name = null)
    {
        //关闭
        if (content == null)
        {
            TaklPanel.SetActive(false);
        }
        else
        {
            TaklPanel.SetActive(true);
            if (name != null)
            {
                if (name == "Luna")
                {
                    CharacterImage.sprite = CharacterSprtes[0];
                }
                else
                {
                    CharacterImage.sprite = CharacterSprtes[1];
                }
                CharacterImage.SetNativeSize();
            }
            NameText.text = name;
            ContentText.text = content;
        }
    }
}
