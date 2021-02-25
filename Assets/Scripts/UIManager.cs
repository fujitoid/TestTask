using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    Mediator mediator;

    [SerializeField]
    Text text;

    void Start()
    {
        
    }


    void Update()
    {
        if(mediator._trackerController.isTracked)
        {
            TextAfterTracking();
        }
        else
        {
            TextBeforeTracking();
        }
    }

    public void TextBeforeTracking()
    {
        text.text = "Відскануйте стіл";
    }  
    
    void TextAfterTracking()
    {
        text.text = "Куди кинути куб?";
    }
}
