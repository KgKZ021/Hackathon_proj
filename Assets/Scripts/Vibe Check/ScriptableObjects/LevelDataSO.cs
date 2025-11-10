using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDataSO", menuName = "Scriptable Objects/LevelDataSO")]
public class LevelDataSO : ScriptableObject
{
    public string promptText;

    // The sound prompt (you can add this later)
    // public AudioClip promptSound;


    public EmojiDataSO correctEmoji;

    public List<EmojiDataSO> levelOptions;
}
