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

    [SerializeField] private int maxPageCount = 5;
    [SerializeField] private float pageChangeCooldown = 0.5f;

    private bool isProcessing = false;

    void Start()
    {
        if (nextButton != null)
            nextButton.onClick.AddListener(OnNextButtonClick);

        if (prevButton != null)
            prevButton.onClick.AddListener(OnPrevButtonClick);
    }

    private void OnNextButtonClick()
    {
        GoToPageAsync(1).Forget();
    }

    private void OnPrevButtonClick()
    {
        GoToPageAsync(-1).Forget();
    }

    private async UniTaskVoid GoToPageAsync(int direction)
    {
        if (isProcessing) return;

        int nextPage = bookUI.CurrentPage + direction;

        if (!IsValidPage(nextPage)) return;

        isProcessing = true;

        bookUI.CurrentPage = nextPage;

        await UniTask.Delay(TimeSpan.FromSeconds(pageChangeCooldown));

        isProcessing = false;
    }

    private bool IsValidPage(int page)
    {
        return page >= 0 && page < maxPageCount;
    }
}