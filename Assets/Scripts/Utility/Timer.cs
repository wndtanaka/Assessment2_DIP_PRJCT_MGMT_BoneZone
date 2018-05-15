using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private class TimedEvent
    {
        public float TimeToExecute;
        public TimeCallBack TimeCallBack;
    }

    public delegate void TimeCallBack();

    private List<TimedEvent> events;

    private void Start()
    {
        events = new List<TimedEvent>();
    }

    // other script able to access this function, to add timer mechanics
    public void Add(TimeCallBack timeCallBack, float inSeconds)
    {
        events.Add(new TimedEvent { TimeCallBack = timeCallBack, TimeToExecute = Time.time + inSeconds });
    }

    private void Update()
    {
        if (events.Count == 0)
        {
            return;
        }
        for (int i = 0; i < events.Count; i++)
        {
            TimedEvent timedEvent = events[i];
            if (timedEvent.TimeToExecute <= Time.time)
            {
                timedEvent.TimeCallBack();
                events.Remove(timedEvent);
            }
        }
    }
}
