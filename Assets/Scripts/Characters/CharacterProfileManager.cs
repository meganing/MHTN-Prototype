using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro; // Use this if using TextMeshPro
using System.Collections.Generic;
using System.Linq; // Needed for FirstOrDefault

public class CharacterProfileManager : MonoBehaviour
{
    [Header("Character Display References")]
    [Tooltip("Assign the UI Image for the character's main portrait.")]
    public Image characterPortraitImage; // Assign CharacterPortrait's Image component
    [Tooltip("Assign the TextMeshProUGUI for the character's name.")]
    public TextMeshProUGUI characterNameText; // Assign CharacterName's TextMeshPro component
    [Tooltip("Assign the TextMeshProUGUI for the character's description.")]
    public TextMeshProUGUI characterBioText; // Assign CharacterDescription's TextMeshPro component
    [Tooltip("Assign the TextMeshProUGUI for the follower count.")]
    public TextMeshProUGUI followerCountText; // Assign FollowerCount's TextMeshPro component
    [Tooltip("Assign the TextMeshProUGUI for the unlocked stories count.")]
    public TextMeshProUGUI storiesUnlockedCountText; // Assign StoriesUnlockedCount's TextMeshPro component

    [Header("Story Display References")]
    [Tooltip("Assign the prefab for the story thumbnail button.")]
    public StoryThumbnail storyButtonPrefab; // Assign your StoryThumbnailButton prefab
    [Tooltip("Assign the 'Content' RectTransform inside the story Scroll View.")]
    public Transform storyContentParent; // Assign ScrollView -> Viewport -> Content

    [Header("Data Source")]
    [Tooltip("Assign ALL CharacterData ScriptableObjects here.")]
    public List<CharacterData> allCharacterData; // Assign Yayang_Data, Nikom_Data, etc.

    private CharacterData currentCharacterData;
    private int unlockedStoryCount = 0; // Variable to track the count

    void Start()
    {
        if (!LoadCharacterData())
        {
            Debug.LogError("Failed to load character profile. Returning to selection.");
            SceneManager.LoadScene("CharacterSelect"); // Or your selection scene name
            return;
        }
        // Populate stories first to calculate the count
        PopulateStoryList();
        // Then display character info, including the calculated count
        DisplayCharacterInfo();
    }

    bool LoadCharacterData()
    {
        int selectedID = PlayerPrefs.GetInt("SelectedCharacterID", -1); // Use -1 or other invalid ID as default

        if (selectedID == -1 || allCharacterData == null || allCharacterData.Count == 0)
        {
            Debug.LogError("No SelectedCharacterID found or no CharacterData assigned.");
            return false;
        }

        currentCharacterData = allCharacterData.FirstOrDefault(profile => profile.characterID == selectedID);

        if (currentCharacterData == null)
        {
            Debug.LogError($"Profile data not found for character ID: {selectedID}");
            return false;
        }
        return true;
    }

    // Displays static info + calculated counts
    void DisplayCharacterInfo()
    {
        if (currentCharacterData == null) return;

        // Assign visual/text data
        if (characterPortraitImage != null) characterPortraitImage.sprite = currentCharacterData.fullBodyImage; // Use the larger image here
        if (characterNameText != null) characterNameText.text = currentCharacterData.characterName;
        if (characterBioText != null) characterBioText.text = currentCharacterData.characterDescription;

        // --- Update Counts ---
        // Followers (Example: Using PlayerPrefs, default to 0)
        // You might have a different way to store/calculate followers
        int currentFollowers = PlayerPrefs.GetInt($"Followers_{currentCharacterData.characterID}", 0);
        if (followerCountText != null) followerCountText.text = $"Followers: {currentFollowers}";

        // Stories Unlocked Count (uses the count calculated in PopulateStoryList)
        if (storiesUnlockedCountText != null) storiesUnlockedCountText.text = $"Stories unlocked: {unlockedStoryCount}";
    }

    // Populates the list AND counts unlocked stories
    void PopulateStoryList()
    {
        if (currentCharacterData == null || storyContentParent == null || storyButtonPrefab == null)
        {
             Debug.LogError("Cannot populate story list - missing references or character data.");
             return;
        }

        // Clear existing story buttons
        foreach (Transform child in storyContentParent)
        {
            Destroy(child.gameObject);
        }

        unlockedStoryCount = 0; // Reset count before iterating
        bool isFirstStory = true; // First story in the list is always unlocked by default

        foreach (StoryInfo story in currentCharacterData.stories)
        {
            StoryThumbnail buttonGO = Instantiate(storyButtonPrefab, storyContentParent);
            // Button button = buttonGO.GetComponent<Button>();
            // Assuming prefab structure: Find image components (adjust names if different)
            // Image thumbnailImage = buttonGO.GetComponentInChildren<Image>(); // Simple find if only one image
            // If  have specific names:
            // Image thumbnailImage = buttonGO.transform.Find("ThumbnailImage")?.GetComponent<Image>();
            // GameObject lockedOverlay = buttonGO.transform.Find("LockedOverlay")?.gameObject;

            // Key format: "StoryUnlock_CharacterID_StoryID"
            string unlockKey = $"StoryUnlock_{currentCharacterData.characterID}_{story.storyID}";
            bool isUnlocked = isFirstStory || PlayerPrefs.GetInt(unlockKey, 0) == 1;

            if (isUnlocked)
            {
                unlockedStoryCount++; // Increment the counter
            }

            // Update visuals based on lock state
            // if (thumbnailImage != null)
            // {
            //     thumbnailImage.sprite = isUnlocked ? story.unlockedThumbnail : story.lockedThumbnail;
            //     thumbnailImage.color = isUnlocked ? Color.white : Color.grey; // Optional visual cue
            // }
            buttonGO.sprite = isUnlocked ? story.unlockedThumbnail : story.lockedThumbnail;
            buttonGO.isUnlocked = isUnlocked;


            // if (lockedOverlay != null) lockedOverlay.SetActive(!isUnlocked); // Show/hide lock icon

            // Setup button interaction
            // if (button != null)
            // {
            //     if (isUnlocked)
            //     {
            //         string currentStoryID = story.storyID; // Capture loop variable
            //         button.onClick.RemoveAllListeners(); // Clear previous listeners
            //         button.onClick.AddListener(() => SelectStory(currentStoryID));
            //         button.interactable = true;
            //     }
            //     else
            //     {
            //         button.interactable = false;
            //     }
            // }

            buttonGO.storyId = story.storyID;
            buttonGO.onClick.AddListener((currentStoryID) => SelectStory(currentStoryID));

            isFirstStory = false; // Only the very first counts as "default" unlocked
        }
    }

    void SelectStory(string storyID)
    {
        Debug.Log("Selected Story: " + storyID);
        PlayerPrefs.SetString("SelectedStoryID", storyID); // Save the ID of the story to load
        PlayerPrefs.Save();

        // Load the Story Scene
        SceneManager.LoadScene("StoryScene"); //
    }

    // Button for going back ---
    public void GoBackToCharacterSelect()
    {
        SceneManager.LoadScene("CharacterSelect"); // 
    }
}