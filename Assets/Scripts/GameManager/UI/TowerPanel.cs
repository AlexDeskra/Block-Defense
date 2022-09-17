using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    UIManager UIManagerInstance;
    string Text;
    public void Initialize(UIManager UImanagerInstance, string text)
    {
        UIManagerInstance = UImanagerInstance;
        Text = text;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManagerInstance.UpdateTowerInfoPanel(Text, GetComponent<RectTransform>().anchoredPosition );
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        UIManagerInstance.DisableTowerInfoPanel();
    }
}
