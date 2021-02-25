using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TrackingBtnManager : MonoBehaviour
{
    [SerializeField]
    Text text;

    public UnityEvent startTracking;

    bool isClicked;
    public void OnClckTrackedBtn()
    {
        isClicked = !isClicked;

        if(isClicked)
        {
            text.text = "Розпочати трекінг";
        }
        else
        {
            text.text = "Зупинити трекінг";
            startTracking.Invoke();
        }
    }
}
