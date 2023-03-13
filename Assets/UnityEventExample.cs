using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventExample : MonoBehaviour
{
    [Header("Header Example")]
    public UnityEvent OnEnableExample = new UnityEvent();

    [SerializeField] int number = 0;

    
    private void OnEnable()
    {
        OnEnableExample?.Invoke();
    }
}
