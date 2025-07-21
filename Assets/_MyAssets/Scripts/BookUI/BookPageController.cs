using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class BookPageController : MonoBehaviour
{
    [SerializeField] private BookUI bookUI;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button prevButton;

    private bool isProcessing = false;

    void Start()
    {
        if (nextButton != null)
            nextButton.onClick.AddListener(() => GoToPageAsync(1).Forget());

        if (prevButton != null)
            prevButton.onClick.AddListener(() => GoToPageAsync(-1).Forget());
    }

    private async UniTaskVoid GoToPageAsync(int direction)
    {
        if (isProcessing) return;

        isProcessing = true;

        // ページを進める/戻す
        bookUI.CurrentPage += direction;

        // ページ遷移アニメーションなどの終了待機（0.5秒は仮。必要に応じて変更）
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));

        isProcessing = false;
    }
}