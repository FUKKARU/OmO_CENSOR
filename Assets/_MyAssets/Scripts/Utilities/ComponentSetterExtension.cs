using UnityEngine;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;

namespace Scripts.Utilities
{
    public static class ComponentSetterExtension
    {
        public static void SetPosX(this Transform transform, float x)
        {
            Vector3 position = transform.position;
            position.x = x;
            transform.position = position;
        }

        public static void SetPosY(this Transform transform, float y)
        {
            Vector3 position = transform.position;
            position.y = y;
            transform.position = position;
        }

        public static void SetPosZ(this Transform transform, float z)
        {
            Vector3 position = transform.position;
            position.z = z;
            transform.position = position;
        }

        public static void SetPosXY(this Transform transform, float x, float y)
        {
            Vector3 position = transform.position;
            position.x = x;
            position.y = y;
            transform.position = position;
        }

        public static void SetPosXZ(this Transform transform, float x, float z)
        {
            Vector3 position = transform.position;
            position.x = x;
            position.z = z;
            transform.position = position;
        }

        public static void SetPosYZ(this Transform transform, float y, float z)
        {
            Vector3 position = transform.position;
            position.y = y;
            position.z = z;
            transform.position = position;
        }

        public static void SetRotX(this Transform transform, float x)
        {
            Vector3 rotation = transform.eulerAngles;
            rotation.x = x;
            transform.eulerAngles = rotation;
        }

        public static void SetRotY(this Transform transform, float y)
        {
            Vector3 rotation = transform.eulerAngles;
            rotation.y = y;
            transform.eulerAngles = rotation;
        }

        public static void SetRotZ(this Transform transform, float z)
        {
            Vector3 rotation = transform.eulerAngles;
            rotation.z = z;
            transform.eulerAngles = rotation;
        }

        public static void SetRotXY(this Transform transform, float x, float y)
        {
            Vector3 rotation = transform.eulerAngles;
            rotation.x = x;
            rotation.y = y;
            transform.eulerAngles = rotation;
        }

        public static void SetRotXZ(this Transform transform, float x, float z)
        {
            Vector3 rotation = transform.eulerAngles;
            rotation.x = x;
            rotation.z = z;
            transform.eulerAngles = rotation;
        }

        public static void SetRotYZ(this Transform transform, float y, float z)
        {
            Vector3 rotation = transform.eulerAngles;
            rotation.y = y;
            rotation.z = z;
            transform.eulerAngles = rotation;
        }

        public static void SetAnchorPosX(this RectTransform rectTransform, float x)
        {
            Vector2 anchoredPosition = rectTransform.anchoredPosition;
            anchoredPosition.x = x;
            rectTransform.anchoredPosition = anchoredPosition;
        }

        public static void SetAnchorPosY(this RectTransform rectTransform, float y)
        {
            Vector2 anchoredPosition = rectTransform.anchoredPosition;
            anchoredPosition.y = y;
            rectTransform.anchoredPosition = anchoredPosition;
        }

        public static void SetLocalPosX(this Transform transform, float x)
        {
            Vector3 localPosition = transform.localPosition;
            localPosition.x = x;
            transform.localPosition = localPosition;
        }

        public static void SetLocalPosY(this Transform transform, float y)
        {
            Vector3 localPosition = transform.localPosition;
            localPosition.y = y;
            transform.localPosition = localPosition;
        }

        public static void SetLocalPosZ(this Transform transform, float z)
        {
            Vector3 localPosition = transform.localPosition;
            localPosition.z = z;
            transform.localPosition = localPosition;
        }

        public static void SetLocalPosXY(this Transform transform, float x, float y)
        {
            Vector3 localPosition = transform.localPosition;
            localPosition.x = x;
            localPosition.y = y;
            transform.localPosition = localPosition;
        }

        public static void SetLocalPosXZ(this Transform transform, float x, float z)
        {
            Vector3 localPosition = transform.localPosition;
            localPosition.x = x;
            localPosition.z = z;
            transform.localPosition = localPosition;
        }

        public static void SetLocalPosYZ(this Transform transform, float y, float z)
        {
            Vector3 localPosition = transform.localPosition;
            localPosition.y = y;
            localPosition.z = z;
            transform.localPosition = localPosition;
        }

        public static void SetLocalRotX(this Transform transform, float x)
        {
            Vector3 localRotation = transform.localEulerAngles;
            localRotation.x = x;
            transform.localEulerAngles = localRotation;
        }

        public static void SetLocalRotY(this Transform transform, float y)
        {
            Vector3 localRotation = transform.localEulerAngles;
            localRotation.y = y;
            transform.localEulerAngles = localRotation;
        }

        public static void SetLocalRotZ(this Transform transform, float z)
        {
            Vector3 localRotation = transform.localEulerAngles;
            localRotation.z = z;
            transform.localEulerAngles = localRotation;
        }

        public static void SetLocalRotXY(this Transform transform, float x, float y)
        {
            Vector3 localRotation = transform.localEulerAngles;
            localRotation.x = x;
            localRotation.y = y;
            transform.localEulerAngles = localRotation;
        }

        public static void SetLocalRotXZ(this Transform transform, float x, float z)
        {
            Vector3 localRotation = transform.localEulerAngles;
            localRotation.x = x;
            localRotation.z = z;
            transform.localEulerAngles = localRotation;
        }

        public static void SetLocalRotYZ(this Transform transform, float y, float z)
        {
            Vector3 localRotation = transform.localEulerAngles;
            localRotation.y = y;
            localRotation.z = z;
            transform.localEulerAngles = localRotation;
        }

        public static void SetScaleX(this Transform transform, float x)
        {
            Vector3 scale = transform.localScale;
            scale.x = x;
            transform.localScale = scale;
        }

        public static void SetScaleY(this Transform transform, float y)
        {
            Vector3 scale = transform.localScale;
            scale.y = y;
            transform.localScale = scale;
        }

        public static void SetScaleZ(this Transform transform, float z)
        {
            Vector3 scale = transform.localScale;
            scale.z = z;
            transform.localScale = scale;
        }

        public static void SetScaleXY(this Transform transform, float x, float y)
        {
            Vector3 scale = transform.localScale;
            scale.x = x;
            scale.y = y;
            transform.localScale = scale;
        }

        public static void SetScaleXZ(this Transform transform, float x, float z)
        {
            Vector3 scale = transform.localScale;
            scale.x = x;
            scale.z = z;
            transform.localScale = scale;
        }

        public static void SetScaleYZ(this Transform transform, float y, float z)
        {
            Vector3 scale = transform.localScale;
            scale.y = y;
            scale.z = z;
            transform.localScale = scale;
        }

        public static void SetAlpha(this Image image, float alpha)
        {
            Color color = image.color;
            color.a = Mathf.Clamp01(alpha);
            image.color = color;
        }

        public static void SetAlpha(this Text text, float alpha)
        {
            Color color = text.color;
            color.a = Mathf.Clamp01(alpha);
            text.color = color;
        }
    }
}