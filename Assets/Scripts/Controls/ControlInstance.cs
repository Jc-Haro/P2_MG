using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlInstance : MonoBehaviour
{
    private static ControlsAsset m_Instance;

    public static ControlsAsset Instanse
    {
        get
        {
            return m_Instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //If controlls already exist
        if (m_Instance != null)
        {
            //Destroy this game object
            Destroy(gameObject);
            return;
        }

        //create new controlls
        m_Instance = new ControlsAsset();
    }

    private void OnEnable()
    {
        //enable controlls
        m_Instance.Enable();
    }
    private void OnDisable()
    {
        //disable controlls 
        m_Instance.Disable();
    }
}
