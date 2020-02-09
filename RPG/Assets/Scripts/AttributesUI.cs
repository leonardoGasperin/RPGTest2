using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributesUI : MonoBehaviour
{
    [SerializeField] GameObject characterC = null;
    [SerializeField] Button str;
    [SerializeField] Button pro;
    [SerializeField] Button agi;
    [SerializeField] Button dex;
    [SerializeField] Button intt;
    [SerializeField] Button eru;
    [SerializeField] Text strV;
    [SerializeField] Text proV;
    [SerializeField] Text agiV;
    [SerializeField] Text dexV;
    [SerializeField] Text intV;
    [SerializeField] Text eruV;
    [SerializeField] Text pointTxt;
    [SerializeField] Text pointV;

    private void Start()
    {
        RemainingPoints(0);
    }

    private void Update()
    {
        if (gameObject.activeSelf)
            ShowStatsPoint();
    }

    public void RemainingPoints(int points)
    {
        if (points > 0)
        {
            str.gameObject.SetActive(true);
            pro.gameObject.SetActive(true);
            agi.gameObject.SetActive(true);
            dex.gameObject.SetActive(true);
            intt.gameObject.SetActive(true);
            eru.gameObject.SetActive(true);
            pointTxt.enabled = true;
            pointV.text = points + "";
            pointV.enabled = true;
        }
        else
        {
            str.gameObject.SetActive(false);
            pro.gameObject.SetActive(false);
            agi.gameObject.SetActive(false);
            dex.gameObject.SetActive(false);
            intt.gameObject.SetActive(false);
            eru.gameObject.SetActive(false);
            pointTxt.enabled = false;
            pointV.enabled = false;
        }
    }

    public void AddPoints(int st)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<CalcStats>().AddStats(st, 1);
    }

    public void CloseWindow()
    {
        characterC.SetActive(!characterC.activeSelf);
    }

    private void ShowStatsPoint()
    {
        CalcStats sts = GameObject.FindGameObjectWithTag("Player").GetComponent<CalcStats>();
        strV.text = string.Format("{0:0}", sts.GetStats(Stats.Str));
        proV.text = string.Format("{0:0}", sts.GetStats(Stats.Pro));
        agiV.text = string.Format("{0:0}", sts.GetStats(Stats.Agi));
        dexV.text = string.Format("{0:0}", sts.GetStats(Stats.Dex));
        intV.text = string.Format("{0:0}", sts.GetStats(Stats.Int));
        eruV.text = string.Format("{0:0}", sts.GetStats(Stats.Eru));
    }
}
