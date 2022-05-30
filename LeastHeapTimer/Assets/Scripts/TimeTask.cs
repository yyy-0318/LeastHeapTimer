using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTask : ITimer
{
    /// <summary>
    /// 过期时间戳
    /// </summary>
    public long TimeoutMs { get; }

    /// <summary>
    /// 延时任务
    /// </summary>
    public Action DelayTask { get; }

    public TimeTask(long timeoutMs, Action callBack)
    {
        TimeoutMs = timeoutMs;
        DelayTask = callBack;
    }
    /// <summary>
    /// 取消任务
    /// </summary>
    public bool Cancel()
    {
        return TimerManager.Instance.RemoveTimer(this);
    }
}
