using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class BookPageController : MonoBehaviour
{
    [SerializeField] private BookUI bookUI;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button prevButton;

    void Start()
    {
        if (nextButton != null)
            nextButton.onClick.AddListener(GoToNextPage);

        if (prevButton != null)
            prevButton.onClick.AddListener(GoToPreviousPage);
    }

    public void GoToNextPage()
    {
        bookUI.CurrentPage++;
    }

    public void GoToPreviousPage()
    {
        bookUI.CurrentPage--;
    }
}