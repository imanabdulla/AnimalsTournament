using UnityEngine;
using UnityEngine.UI;

public class GameInitialization : MonoBehaviour
{
    [SerializeField] private Dropdown playersDropdown;
    [SerializeField] private Dropdown animalsDropdown;
    int playerIndex, animalIndex;


    //when user choose a player from Player DropDown Menu
    public void OnPlayerChoice()
    {
        //set chosen player number to the database
        XMLManager.xMLManager.tournamantDB.informations[playerIndex].playerNumber = playersDropdown.value + 1;

        //increase index to next choice
        playerIndex++;

        //disable chosen player
        playersDropdown.options[playersDropdown.value].text = "";

        //deactivate dropdown menue after chosen all animals
        if (playerIndex > XMLManager.xMLManager.tournamantDB.informations.Count - 1)
        {
            playersDropdown.gameObject.SetActive(false);
            animalsDropdown.gameObject.SetActive(true);
        }

    }

    //when user choose an animal from Animal DropDown Menu
    public void OnAnimalCoice()
    {
        //set chosen animal name to the database
        XMLManager.xMLManager.tournamantDB.informations[animalIndex].animalName = animalsDropdown.options[animalsDropdown.value].text;

        //increase index to next choice
        animalIndex++;

        //deactivate dropdown menue after chosen all animals
        if (animalIndex > XMLManager.xMLManager.tournamantDB.informations.Count - 1)
            animalsDropdown.gameObject.SetActive(false);
    }


    public void UpdateCardSigns()
    {
        foreach (var info in XMLManager.xMLManager.tournamantDB.informations)
        {
            info.cardSign = info.playerNumber.ToString() + info.animalName[0];
        }
        //save them to database
        XMLManager.xMLManager.Save();
    }
}
