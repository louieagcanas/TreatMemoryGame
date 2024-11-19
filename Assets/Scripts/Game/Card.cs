using System;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public event Action<int> OnCardSelected;

    [SerializeField]
    private Image cardImage;

    [SerializeField]
    private GameObject cardFront;

    [SerializeField]
    private GameObject cardBack;

    public string CardName { private set; get; }

    private Button button;
    private int cardIndex;
    private bool isDone = false;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(CardSelectedHandler);
    }

    private void CardSelectedHandler()
    {
        OnCardSelected?.Invoke(cardIndex);

        ShowCardFront();
    }

    public void Initialize(int index, Sprite sprite)
    {
        cardIndex = index;
        cardImage.sprite = sprite;
        CardName = sprite.name;
        isDone = false;

        ShowCardBack();
    }

    public void ShowCardFront()
    {
        cardFront.SetActive(true);
        cardBack.SetActive(false);
        DisableInput();
    }

    public void ShowCardBack()
    {
        cardFront.SetActive(false);
        cardBack.SetActive(true);
        EnableInput();
    }

    public void MarkAsDone()
    {
        isDone = true;
        cardFront.SetActive(false);
        cardBack.SetActive(false);
        DisableInput();
    }

    public void EnableInput()
    {
        if (isDone) return;
        button.interactable = true;
    }

    public void DisableInput()
    {
        button.interactable = false;
    }
}
