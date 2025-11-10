using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
   
    [Header("Level Data")]
    [SerializeField] private List<LevelDataSO> allLevels;
    private int currentLevelIndex = 0;
    private LevelDataSO currentLevel;

    [Header("UI References")]
    [SerializeField]private TextMeshProUGUI promptText;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private Image resultBox;
    [SerializeField]private Transform buttonContainer;

    [Header("Prefab")]
    [SerializeField] private GameObject emojiButtonPrefab;

    [Header("Settings")]
    [SerializeField] private float resultDelay = 0f;

    private List<EmojiButton> currentButtons = new List<EmojiButton>();

    
    private void Start()
    {
        // Check if we have any levels
        if (allLevels.Count > 0)
        {
            LoadLevel(currentLevelIndex);
        }
        
        
    }

    private void LoadLevel(int levelIndex)
    {
        CancelInvoke();
        if (levelIndex >= allLevels.Count)
        {
            // finished all levels!
            promptText.text = "You Won the Game!";
            resultText.text = "Great Job!";
            ClearButtons();
            return;
        }
        currentLevel = allLevels[levelIndex];
        promptText.text = currentLevel.promptText;
        resultText.text = "";
        resultBox.gameObject.SetActive(false);

        ClearButtons();

        
        foreach (EmojiDataSO emoji in currentLevel.levelOptions)
        {
            GameObject newButtonObj = Instantiate(emojiButtonPrefab, buttonContainer);
            EmojiButton emojiButton = newButtonObj.GetComponent<EmojiButton>();

            emojiButton.Setup(emoji);

            emojiButton.OnEmojiPressed += OnEmojiClicked;
            currentButtons.Add(emojiButton);

           
        }
    }

    public void OnEmojiClicked(object sender, EmojiButton.OnEmojiPressedEventArgs e)
    {
        EmojiDataSO clickedEmoji = e.selectedEmoji;
        SetAllButtonsInteractable(false);

        if (clickedEmoji == currentLevel.correctEmoji)
        {

            HandleCorrectAnswer();
        }
        else
        {

            HandleIncorrectAnswer();
        }
    }



private void HandleCorrectAnswer()
    {
        resultText.text = "Correct!";

        resultBox.gameObject.SetActive(true);
        resultText.color = new Color32(20, 133, 140, 255);
        Invoke(nameof(LoadNextLevel), resultDelay);
    }

    private void HandleIncorrectAnswer()
    {
        resultText.text = "Try Again!";

        resultBox.gameObject.SetActive(true);
        resultText.color = new Color32(128, 55, 55, 255);
        Invoke(nameof(ResetLevelAttempt), resultDelay);
    }
    


    private void LoadNextLevel()
    {
        currentLevelIndex++;
        LoadLevel(currentLevelIndex);
    }

    private void ResetLevelAttempt()
    {
        resultText.text = "";

        resultBox.gameObject.SetActive(false);
        SetAllButtonsInteractable(true);
    }
    void ClearButtons()
    {
        foreach (EmojiButton button in currentButtons)
        {
            button.OnEmojiPressed -= OnEmojiClicked;
            Destroy(button.gameObject);
        }
        currentButtons.Clear();
    }

    private void SetAllButtonsInteractable(bool interactable)
    {
        foreach (EmojiButton button in currentButtons)
        {
            button.SetInteractable(interactable);
        }
    }
}
