using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Webtoon Game/Character Data")]
public class CharacterData : ScriptableObject {
    public string characterName;
    public int characterID;
    public Sprite portrait;
    public Sprite fullBodyImage;
    public string characterDescription;
    public int initialFollowers = 0;
    public int unlockedStories = 1;
}