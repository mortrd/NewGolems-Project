using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string content;
    public RectTransform rect;
    public Vector2 Offset;
    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipSystem.Show(content);
        rect.pivot = Offset;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
    }
}
