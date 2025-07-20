using System;
using UnityEngine.EventSystems;

namespace Scripts.Utilities.Buttons
{
    public static class GeneralExtension
    {
        /// <summary>
        /// EventTriggerにイベントを登録する
        /// </summary>
        public static void AddListener(this EventTrigger eventTrigger, EventTriggerType type, Action<PointerEventData> action)
        {
            EventTrigger.Entry entry = new() { eventID = type };
            entry.callback.AddListener(data =>
            {
                if (data is PointerEventData pointerData)
                    action?.Invoke(pointerData);
            });
            eventTrigger.triggers.Add(entry);
        }
    }
}