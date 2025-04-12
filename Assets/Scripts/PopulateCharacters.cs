using UnityEngine;

public class CharacterSelectionManager : MonoBehaviour {
    public GameObject characterCardPrefab;
    public Transform contentParent;
    public CharacterData[] availableCharacters;
    
    void Start() {
        PopulateCharacters();
    }
    
    void PopulateCharacters() {
        foreach (CharacterData character in availableCharacters) {
            GameObject card = Instantiate(characterCardPrefab, contentParent);
            CharacterCard cardScript = card.GetComponent<CharacterCard>();
            cardScript.characterImage.sprite = character.portrait;
            cardScript.characterName.text = character.characterName;
            cardScript.characterID = character.characterID;
        }
    }
}