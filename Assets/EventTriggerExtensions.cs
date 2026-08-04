using UnityEngine;
using UnityEngine.EventSystems;
using System;

#pragma warning disable
public static class EventTriggerExtensions
{
    public static void AddEvent(
        this EventTrigger trigger,
        EventTriggerType type,
        Action<BaseEventData> action)
    {
        EventTrigger.Entry entry = new();
        entry.eventID = type;

        entry.callback.AddListener((data) => action(data));

        trigger.triggers.Add(entry);
    }
}