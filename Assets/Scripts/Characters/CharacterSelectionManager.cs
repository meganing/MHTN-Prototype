using UnityEngine;
using UnityEngine.UI; // Added for Button
using TMPro; // Added if CharacterCard uses TMPro, good practice
using UnityEngine.SceneManagement; // Added if manager needs to load scenes directly (it doesn't here, but good practice)

public class CharacterSelectionManager : MonoBehaviour {

    [Header("Setup")] // Added header
    public GameObject characterCardPrefab; // The prefab with CharacterCard.cs attached
    public Transform contentParent; // The Content Transform of the Scroll View

    [Header("Data")] // Added header
    public CharacterData[] availableCharacters; // Array of CharacterData ScriptableObjects

    void Start() {
        PopulateCharacters();
    }

    void PopulateCharacters() {
        // Optional: Clear existing cards (useful if repopulating)
        // foreach (Transform child in contentParent)
        // {
        //     Destroy(child.gameObject);
        // }

        // Instantiate a card for each character data asset
        foreach (CharacterData character in availableCharacters) {
            GameObject cardGO = Instantiate(characterCardPrefab, contentParent);
            CharacterCard cardScript = cardGO.GetComponent<CharacterCard>();

            // Check if the prefab has the CharacterCard script
            if (cardScript == null)
            {
                Debug.LogError("Character Card Prefab is missing the CharacterCard script!", characterCardPrefab);
                Destroy(cardGO); // Clean up
                continue; // Skip this character
            }

            // Assign data to the CharacterCard script
            if (cardScript.characterImage != null && character.portrait != null)
            {
                 cardScript.characterImage.sprite = character.portrait;
            }
             if (cardScript.characterName != null && character.characterName != null)
            {
                 cardScript.characterName.text = character.characterName;
            }
            cardScript.characterID = character.characterID;

            // Find the Button component on the instantiated card and hook up the listener
            Button cardButton = cardGO.GetComponent<Button>(); // Assumes Button is on the root of the prefab
            // OR, if selectButton is correctly assigned in the prefab:
            // Button cardButton = cardScript.selectButton;


            if (cardButton != null) // Check if a Button component was found
            {
                // Remove any existing listeners to prevent duplicates if this runs again
                cardButton.onClick.RemoveAllListeners();
                // Add the listener to call the SelectCharacter method on the CharacterCard script
                cardButton.onClick.AddListener(() => cardScript.SelectCharacter());
            }
            else
            {
                Debug.LogError("Character Card Prefab is missing a Button component on its root or selectButton is not assigned!", characterCardPrefab);
            }
        }
    }

    // --- Optional: Button for going back to Start ---
    public void GoBackToStart()
    {
        SceneManager.LoadScene("StartScene"); // Or your actual start scene name
    }
}