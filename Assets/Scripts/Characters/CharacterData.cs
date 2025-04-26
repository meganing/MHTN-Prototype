using UnityEngine;
using System.Collections.Generic;

// --- Story Info Class (nested or separate, Serializable) ---
[System.Serializable] // Make StoryInfo visible in Inspector when used in a List/Array
public class StoryInfo
{
    [Tooltip("Unique ID for this story (e.g., Yayang_Story_1). Used for saving progress and loading data.")]
    public string storyID;
    // public string storyTitle; // Optional: Display title for the story thumbnail
    [Tooltip("Thumbnail image shown when the story is unlocked.")]
    public Sprite unlockedThumbnail;
    [Tooltip("Thumbnail image shown when the story is locked.")]
    public Sprite lockedThumbnail;
}


// --- Character Data Scriptable Object Definition ---
[CreateAssetMenu(fileName = "New Character Data", menuName = "Webtoon Game/Character Data")]
public class CharacterData : ScriptableObject {

    [Header("Basic Info")]
    public string characterName = "New Character";
    [Tooltip("Unique numerical ID for this character.")]
    public int characterID = 0; // Assign unique IDs (e.g., Yayang=1, Nikom=2)

    [Header("Visuals")]
    [Tooltip("Small portrait used in the character selection card.")]
    public Sprite portrait;
    [Tooltip("Larger image used in the character profile view.")]
    public Sprite fullBodyImage;

    [Header("Details")]
    [Tooltip("Biography or description shown in the character profile.")]
    [TextArea(3, 10)] // Makes the description field larger in Inspector
    public string characterDescription;
    // public int initialFollowers = 0; // Keep if needed for your game mechanics

    [Header("Associated Stories")]
    [Tooltip("List of stories related to this character.")]
    public List<StoryInfo> stories; // List of stories for this character
}