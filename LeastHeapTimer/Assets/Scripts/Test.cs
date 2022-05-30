using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public InputField m_inputField;

    void Start()
    {
        
    }

   
    void Update()
    {
        
    }
    public void OnAddTimer()
    {
        if (m_inputField!=null)
        {
            int seconds = int.Parse(m_inputField.text);
            DateTime dateTime = DateTime.UtcNow;
            TimerManager.Instance.AddTimer(TimeSpan.FromSeconds(seconds), () => 
            {
                Debug.LogError($" 加入时间:{dateTime}  延迟:{seconds}秒  执行时间:{ DateTime.UtcNow}");
            });
        }
    }
}
