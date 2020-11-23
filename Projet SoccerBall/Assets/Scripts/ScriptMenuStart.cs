using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptMenuStart : MonoBehaviour

{
    public bool PauseActive = false;
    public GameObject PauseMenu;
    public AudioSource Musique;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Activer menu pause
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (PauseActive)
            {
                DesactiverPause();
            }
            else
            {
                ActiverLaPause();
            }
        }

        if (PauseMenu.activeSelf == false) //Vérification du menu pause lorsqu'on utilise le click
        {
            PauseActive = false;
        }
        else if (PauseMenu.activeSelf)
        {
            PauseActive = true;
        }  
    }

    public void LancerJeu()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        Time.timeScale = 1f;
    }

    public void QuitterJeu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public void QuitterAppli()
    {
        Application.Quit();
    }

    public void ActiverLaPause()
    {
        PauseMenu.SetActive(true); //Active l'interface de pause
        PauseActive = true;
        Time.timeScale = 0f; //arrête le temps dans le jeu.
        Musique.Pause(); //met la musique en pause
        
    }

    public void DesactiverPause()
    {
        PauseMenu.SetActive(false); //Désactive l'interface de pause
        PauseActive = false;
        Time.timeScale = 1f; //relance le temps du jeu
        Musique.UnPause(); //relance musique

    }

}
