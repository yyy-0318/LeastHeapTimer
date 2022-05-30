using System;

public interface ITimer
{
    /// <summary>
    /// 过期时间戳
    /// </summary>
    long TimeoutMs { get; }

    /// <summary>
    /// 延时任务
    /// </summary>
    Action DelayTask { get; }

    /// <summary>
    /// 取消任务
    /// </summary>
    bool Cancel();

}
