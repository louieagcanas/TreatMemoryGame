using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Card : MonoBehaviour
{
    public event Action<int> OnCardSelected;

    [SerializeField]
    private Image cardImage;

    [SerializeField]
    private Transform cardFront;

    [SerializeField]
    private Transform cardBack;

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

        FlipShowCardFront();
    }

    public void Initialize(int index, Sprite sprite)
    {
        cardIndex = index;
        cardImage.sprite = sprite;
        CardName = sprite.name;
        isDone = false;

        CardScaleIn();
    }

    private void CardScaleIn()
    {
        cardFront.gameObject.SetActive(false);
        cardBack.gameObject.SetActive(true);

        cardBack.localScale = Vector2.zero;
        float randomDelay = UnityEngine.Random.Range(0.2f, 0.4f);
        cardBack.DOScale(1.0f, 0.3f).SetDelay(randomDelay);

        EnableInput();
    }

    public void FlipShowCardFront()
    {
        var scale = cardFront.localScale;
        scale.x = 0.0f;
        cardFront.localScale = scale;

        cardBack.DOScaleX(0.0f, 0.1f).OnComplete(() =>
        {
            cardBack.gameObject.SetActive(false);

            cardFront.gameObject.SetActive(true);
            cardFront.DOScaleX(1.0f, 0.1f);
        });

        DisableInput();
    }

    public void FlipShowCardBack()
    {
        var scale = cardBack.localScale;
        scale.x = 0.0f;
        cardBack.localScale = scale;

        cardFront.DOScaleX(0.0f, 0.1f).OnComplete(() =>
        {
            cardFront.gameObject.SetActive(false);

            cardBack.gameObject.SetActive(true);
            cardBack.DOScaleX(1.0f, 0.1f);
        });
    }

    public void MarkAsDone()
    {
        isDone = true;
        cardFront.gameObject.SetActive(false);
        cardBack.gameObject.SetActive(false);
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
