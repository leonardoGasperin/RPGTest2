using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
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
            str.gameObject.SetActive(!str.gameObject.activeSelf);
            pro.gameObject.SetActive(!pro.gameObject.activeSelf);
            agi.gameObject.SetActive(!agi.gameObject.activeSelf);
            dex.gameObject.SetActive(!dex.gameObject.activeSelf);
            intt.gameObject.SetActive(!intt.gameObject.activeSelf);
            eru.gameObject.SetActive(!eru.gameObject.activeSelf);
            pointTxt.enabled = true;
            pointV.text = points + "";
            pointV.enabled = true;
        }
        else
        {
            str.gameObject.SetActive(!str.gameObject.activeSelf);
            pro.gameObject.SetActive(!pro.gameObject.activeSelf);
            agi.gameObject.SetActive(!agi.gameObject.activeSelf);
            dex.gameObject.SetActive(!dex.gameObject.activeSelf);
            intt.gameObject.SetActive(!intt.gameObject.activeSelf);
            eru.gameObject.SetActive(!eru.gameObject.activeSelf);
            pointTxt.enabled = false;
            pointV.enabled = false;
        }
    }

    private void AddPoints(int st)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<CalcStats>().AddStats(st, 1);
    }

    private void CloseWindow()
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
