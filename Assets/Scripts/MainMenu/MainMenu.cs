using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenu, worldSelect;
    public void WorldSelection(){
        mainMenu.SetActive(false);
        worldSelect.SetActive(true);
    }
    public void SelectWorld(){
        SceneManager.LoadScene("Game");
    }
}
