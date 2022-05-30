using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : SingletonBehaviour<TimerManager>
{
    public override void InitInstance()
    {
        base.InitInstance();
    }
    void Start()
    {
        
    }
    /// <summary>
    ///  插入
    /// </summary>
    /// <param name="index"></param>
    public void HeapInsert(int index)
    {
        while (m_TimerTaskList[index].TimeoutMs < m_TimerTaskList[(index - 1) / 2].TimeoutMs)
        {
            Swap(index, (index - 1) / 2);
            index = (index - 1) / 2;
        }
    }
    /// <summary>
    /// 交换两个数
    /// </summary>
    /// <param name="i"></param>
    /// <param name="j"></param>
    public void Swap(int i, int j)
    {
        ITimer temp = m_TimerTaskList[i];
        m_TimerTaskList[i] = m_TimerTaskList[j];
        m_TimerTaskList[j] = temp;
    }
    /// <summary>
    /// 初始化长度20的列表
    /// </summary>
    List<ITimer> m_TimerTaskList = new List<ITimer>(20);


    public ITimer AddTimer(long timeoutMs, Action callBack)
    {
        TimeTask timeTask = new TimeTask(DateTime.UtcNow.Ticks / 10000 + timeoutMs, callBack);
        m_TimerTaskList.Add(timeTask);
        HeapInsert(m_TimerTaskList.Count - 1);
        return timeTask;
    }
    public ITimer AddTimer(TimeSpan tickSpan, Action callBack)
    {
        TimeTask timeTask = new TimeTask(DateTime.UtcNow.Ticks / 10000 + (long)tickSpan.TotalMilliseconds, callBack);
        m_TimerTaskList.Add(timeTask);
        HeapInsert(m_TimerTaskList.Count - 1);
        return timeTask;
    }
    void Remove()
    {
        if (m_TimerTaskList.Count == 1)
        {
            m_TimerTaskList.RemoveAt(0);
            return;
        }
        Swap(0, m_TimerTaskList.Count - 1);
        m_TimerTaskList.RemoveAt(m_TimerTaskList.Count - 1);

        UpdateHeap();
    }
    void UpdateHeap()
    {
        int count = m_TimerTaskList.Count;
        int least = 0;
        while (true)
        {
            int left = 2 * least + 1;
            int minPos = least;
            //先比较左子节
            if (left < count && m_TimerTaskList[left].TimeoutMs < m_TimerTaskList[least].TimeoutMs)
            {
                minPos = left;
            }
            int right = 2 * least + 2;
            //在比较右子节
            if (right < count && m_TimerTaskList[right].TimeoutMs < m_TimerTaskList[minPos].TimeoutMs)
            {
                minPos = right;
            }
            if (minPos == least)
            {
                break;
            }
            Swap(least, minPos);

            least = minPos;
        }
    }
    bool Peek(long time,out ITimer timer)
    {
        if (m_TimerTaskList.Count > 0 && m_TimerTaskList[0].TimeoutMs <= time)
        {
            timer = m_TimerTaskList[0];
            Remove();
            return true;
        }
        timer = null;
        return false;
    }  

    void Update()
    {
        if (Peek(DateTime.UtcNow.Ticks / 10000, out ITimer timer))
        {
            timer.DelayTask();
        }
    }
}
