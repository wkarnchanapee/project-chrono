using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Text))]
public class TextControl : MonoBehaviour {

    Text textBox;

    private void Start()
    {
        textBox = GetComponent<Text>();
    }
    public void GiveText(string str)
    {
        textBox.text = str;
    }

    public void GiveTimerFloat(float f)
    {
        textBox.text = f.ToString("F2");
    }
}
