using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class killcount : MonoBehaviour
{
    public TextMeshProUGUI text;
    public int killCount = 0;

    public void Update()
    {
        text.text = "Kill Count = " + killCount;
    }
}
