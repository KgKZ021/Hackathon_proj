using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(EmojiAnimator))]
public class EmojiButton : MonoBehaviour
{
    public event EventHandler<OnEmojiPressedEventArgs> OnEmojiPressed;
    public class OnEmojiPressedEventArgs : EventArgs
    {
        public EmojiDataSO selectedEmoji;
    }
    public EmojiDataSO emojiData;

    private Image buttonImage;
    private Button button;

    private EmojiAnimator emojiAnimator;


    private void Awake()
    {
        emojiAnimator = GetComponent<EmojiAnimator>();
    }
    public void Setup(EmojiDataSO data)
    {

        emojiData = data;

        // Get the components
        buttonImage = GetComponent<Image>();
        button = GetComponent<Button>();

        // Set the button's image
        buttonImage.sprite = emojiData.emojiSprite;

        if (emojiAnimator != null)
        {
            emojiAnimator.SetupAnimation(data.clickAnimation);
        }
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        if (emojiAnimator != null)
        {
            emojiAnimator.PlayClickAnimation();
        }
        // Tell the GameManager that this button (and its emoji) was clicked
        OnEmojiPressed?.Invoke(this, new OnEmojiPressedEventArgs
        {
            selectedEmoji = emojiData
        });
    }

    public void ResetVisual()
    {
        // 1. Reset the sprite
        if (buttonImage != null && emojiData != null)
        {
            buttonImage.sprite = emojiData.emojiSprite;
        }
        
        // 2. Tell the animator to reset
        if (emojiAnimator != null)
        {
            emojiAnimator.ResetVisual();
        }
    }
    public void SetInteractable(bool interactable)
    {
        if (button == null)
        {
            button = GetComponent<Button>();

        }
        button.interactable = interactable;
    }
}
