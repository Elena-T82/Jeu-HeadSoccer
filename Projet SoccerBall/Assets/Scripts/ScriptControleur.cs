using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ScriptControleur : MonoBehaviour
{

    float time = 60f;
    string resultat;

    public GameObject MenuFinJeu;

    public GameObject Joueur1;
    public GameObject Joueur2;
    public GameObject Ballon;
    public AudioSource Musique;

    public GameObject Temps;
    public Text ScoreFinaux;


    // Start is called before the first frame update
    void Start()
    {

        //Calcule de la taille d'écran
        float dist = Camera.main.transform.position.z;
        float MaxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x ;
        float MaxY = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, dist)).y;
        Vector3 tailleEcran = new Vector3(MaxX, MaxY, 0);

        //On envoie la taille de l'écran à tout nos objets.
        Joueur1.GetComponent<ScriptJoueur>().getFrontiere(tailleEcran);
        Joueur2.GetComponent<ScriptJoueur2>().getFrontiere(tailleEcran);
        Ballon.GetComponent<ScriptBallon>().getFrontiere(tailleEcran);

        StartCoroutine(Timer()); //J'appelle ma coroutine
        Debug.Log(MenuFinJeu);
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Timer() //Cette fonction est une coroutine, elle permet de faire des temps d'attente pendant l'execution d'un code.
    {
        while (time > 0)
        {
            time--;
            yield return new WaitForSeconds(1); //Permet de retourner mon temps toute les unes seconde. (yield ->attendre)

            Temps.GetComponent<Text>().text = time.ToString();    
        }

        if(time == 0)
        {
            ActiverMenuFinJeu();
        }
    }

    public void ActiverMenuFinJeu()
    {
        MenuFinJeu.SetActive(true);
        Time.timeScale = 0f; //arrête le temps dans le jeu.
        Musique.Stop(); //La musique prend fin.

        resultat = Ballon.GetComponent<ScriptBallon>().ResultatFinaux(); //On va chercher les résultats de la partie


        //On affiche le nom du gagnant à la fin de la partie sur le menu de fin de jeu.
        if(resultat == "CowBoy" || resultat =="Ninja")
        {
            ScoreFinaux.text = resultat + " a gagné !";
        }
        else
        {
            ScoreFinaux.text = resultat;
        }
    }
}
