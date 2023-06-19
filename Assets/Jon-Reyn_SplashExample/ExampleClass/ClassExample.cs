using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class ClassExample : MonoBehaviour
{
    public int FieldInt = 0;
    public string FieldString = "";
    public float FieldFloat = 0;
    public float FieldFloatDisabled = 0;
    public string CustomString = "";
    public Color CustomColor;
    
    
    
    
    public UnityEvent OnEnableExample = new UnityEvent();
    
    private void OnEnable()
    {
        OnEnableExample?.Invoke();
    }
}
