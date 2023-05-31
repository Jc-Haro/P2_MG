using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls_Instance : MonoBehaviour
{
    //Almacena una unica instancia de controles
    private static Controls_Asset m_Instance;
    
    //Me regresa el objeto de controles
    public static Controls_Asset Instance 
    {
        get
        {
            return m_Instance;
        }
    }

    private void Awake()
    {
        //Si ya existen los controles
        if (m_Instance != null)
        {
            //Destruye el objeto actual
            Destroy(gameObject);
            return;
        }

        //Crea los nuevos controles
        m_Instance = new Controls_Asset();
    }

    private void OnEnable()
    {
        //Activa los controles
        m_Instance.Enable();
    }

    private void OnDisable()
    {
        //Desactiva los controles
        m_Instance.Disable();
    }
}
