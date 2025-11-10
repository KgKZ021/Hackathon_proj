using UnityEngine;

[CreateAssetMenu(fileName = "EmojiDataSO", menuName = "Scriptable Objects/EmojiDataSO")]
public class EmojiDataSO : ScriptableObject
{
    public string emojiName;

    public Sprite emojiSprite;
    public AnimationClip clickAnimation;
}
