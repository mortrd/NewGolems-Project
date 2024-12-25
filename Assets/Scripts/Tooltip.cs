using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour
{
    public TextMeshProUGUI contentfiel;
    public LayoutElement layoutelement;
    public int charcterWrapLimit;



    public void SetText(string content)
    {
        contentfiel.text = content;
        int contentleanght = contentfiel.text.Length;

        layoutelement.enabled = (contentleanght > charcterWrapLimit) ? true : false;
    }

    private void Update()
    {
        transform.position = Input.mousePosition;
    }
}
