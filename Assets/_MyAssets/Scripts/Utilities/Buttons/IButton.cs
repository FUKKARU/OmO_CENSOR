using UnityEngine.EventSystems;

namespace Scripts.Utilities.Buttons
{
    internal interface IButton
    {
        void OnEnter(PointerEventData data);
        void OnExit(PointerEventData data);
        void OnDown(PointerEventData data);
        void OnUp(PointerEventData data);
    }
}