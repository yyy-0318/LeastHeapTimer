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
        if (m_inputField != null)
        {
            int seconds = int.Parse(m_inputField.text);
            DateTime dateTime = DateTime.UtcNow;
            TimerManager.Instance.AddTimer(TimeSpan.FromSeconds(seconds), () =>
            {
                Debug.LogError($" 加入时间:{dateTime}  延迟:{seconds}秒  执行时间:{ DateTime.UtcNow}");
            });
        }
    }
    public void OnRandomAddTimer()
    {
        List<ITimer> m_ITimers = new List<ITimer>();
        for (int i = 0; i < 10; i++)
        {
            int seconds = i + 1;
            DateTime dateTime = DateTime.UtcNow;
            m_ITimers.Add(TimerManager.Instance.AddTimer(TimeSpan.FromSeconds(seconds), () =>
            {
                Debug.LogError($" 加入时间:{dateTime}  延迟:{seconds}秒  执行时间:{ DateTime.UtcNow}");
            }));
        }
        int num = UnityEngine.Random.Range(0, 10);
        m_ITimers[num].Cancel();
        Debug.LogError($"随机移除延迟:{num + 1}秒 的任务");
    }
}
