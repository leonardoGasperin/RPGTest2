using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Button str;
    [SerializeField] Button pro;
    [SerializeField] Button agi;
    [SerializeField] Button dex;
    [SerializeField] Button intt;
    [SerializeField] Button eru;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddPoints()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<CalcStats>().AddStats(Stats.Str, 1);
    }
}
