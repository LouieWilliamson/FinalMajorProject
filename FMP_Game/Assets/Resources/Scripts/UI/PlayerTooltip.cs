using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTooltip : MonoBehaviour
{
    public enum TipType { Gun, XPlatform, YPlatform, Upgrade };

    public string gunTip;
    public string interactionTip;
    public string pickupTip;

    // Start is called before the first frame update
    private Camera maincam;
    public Canvas playerCanvas;

    private bool isTooltipActive;
    public GameObject Tooltip;
    private Text tooltipText;

    private float timer;
    private float tipTime;
    private bool tipAnimationFinished;
    public float timeTilNextLetter;

    private int GunTipCount;
    private int YplatformTipCount;
    private int XplatformTipCount;
    private int UpgradeTipCount;
    private int maxTips;
    void Start()
    {
        GunTipCount = 0;
        YplatformTipCount = 0;
        XplatformTipCount = 0;
        UpgradeTipCount = 0;
        maxTips = 3;

        tipTime = 3;
        timer = 0;

        tooltipText = Tooltip.GetComponent<Text>();
        maincam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        playerCanvas.worldCamera = maincam;
        isTooltipActive = false;
        tipAnimationFinished = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTooltipActive)
        {
            if (tipAnimationFinished)
            {
                timer += Time.deltaTime;

                if (timer >= tipTime)
                {
                    DisableTooltip();
                    timer = 0;
                }
            }
        }
    }
    public void SetTipText(TipType tip)
    {
        string tiptext = "";

        switch (tip)
        {
            case TipType.Gun:
                tiptext = gunTip;
                break;
            case TipType.XPlatform:
                tiptext = interactionTip;
                break;
            case TipType.YPlatform:
                tiptext = interactionTip;
                break;
            case TipType.Upgrade:
                tiptext = pickupTip;
                break;
        }
        if (CheckTipCount(tip))
        {
            StopAllCoroutines();
            EnableTooltip();
            StartCoroutine(TipAnimation(tiptext));
        }
    }
    private bool CheckTipCount(TipType tip)
    {
        bool ShouldTip = false;

        switch (tip)
        {
            case TipType.Gun:
                ShouldTip = !(GunTipCount >= maxTips);
                GunTipCount++;
                break;
            case TipType.XPlatform:
                ShouldTip = !(XplatformTipCount >= maxTips);
                XplatformTipCount++;
                break;
            case TipType.YPlatform:
                ShouldTip = !(YplatformTipCount >= maxTips);
                YplatformTipCount++;
                break;
            case TipType.Upgrade:
                ShouldTip = !(UpgradeTipCount >= maxTips);
                UpgradeTipCount++;
                break;
        }
        return ShouldTip;
    }
    private void EnableTooltip()
    {
        if (!isTooltipActive)
        {
            Tooltip.SetActive(true);
            isTooltipActive = true;
        }
    }
    private void DisableTooltip()
    {
        Tooltip.SetActive(false);
        isTooltipActive = false;
        tipAnimationFinished = false;
    }
    IEnumerator TipAnimation(string sentence)
    {
        tooltipText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            tooltipText.text += letter;
            yield return new WaitForSeconds(timeTilNextLetter * Time.deltaTime); ;
        }

        tipAnimationFinished = true;
    }
}
