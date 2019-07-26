using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnableGameObjectOnMouseover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField]
    GameObject objectToEnable;
    public void OnPointerEnter(PointerEventData eventData)
    {
        objectToEnable.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        objectToEnable.SetActive(false);
    }
}
