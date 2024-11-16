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

    private Button button;
    private int cardIndex;
    public string CardName { private set; get; }

    private void Start()
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

        ShowCardBack();
    }

    public void ShowCardFront()
    {
        cardFront.SetActive(true);
        cardBack.SetActive(false);
    }

    public void ShowCardBack()
    {
        cardFront.SetActive(false);
        cardBack.SetActive(true);
    }

    public void HideCard()
    {
        cardFront.SetActive(false);
        cardBack.SetActive(false);
        button.interactable = false;
    }

    public void EnableInput()
    {
        button.interactable = true;
    }

    public void DisableInput()
    {
        button.interactable = false;
    }
}
