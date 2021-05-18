using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HiddenFirstScreen : MonoBehaviour
{

    public GameObject InfoObject;
    private bool Show = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Showing and hidding
    public void ShowAndHideInfo()
    {
        if (!Show)
        {
            InfoObject.SetActive(true);
            Show = true;
        }
        else
        {
            InfoObject.SetActive(false);
            Show = false;
        }
    }
}
