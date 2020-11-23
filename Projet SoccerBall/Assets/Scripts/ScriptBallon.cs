using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptBallon : MonoBehaviour
{
    Vector3 frontiere;

    public GameObject Joueur1;
    public GameObject Joueur2;

    float ButCowBoy = 0;
    float ButNinja = 0;

    public Text Score1;
    public Text Score2;


    float rayon = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rayon = transform.GetComponent<CircleCollider2D>().bounds.size.x / 2;

    }

    // Update is called once per frame
    void Update()
    {
        //Empêcher les sorties d'écran.
        Vector3 pos = transform.position;

        if (pos.x + rayon > frontiere.x)
        {
            pos.x = frontiere.x - rayon;

        }

        if (pos.x - rayon < -frontiere.x)
        {
            pos.x = -frontiere.x + rayon;

        }

        if (pos.y + rayon > frontiere.y)
        {
            pos.y = frontiere.y - rayon;
            pos.y *= -1;

        }

        if (pos.y - rayon < -frontiere.y)
        {
            pos.y = -frontiere.y + rayon;
            pos.y *= -1;
        }


        transform.position = pos;
    }

    public void getFrontiere(Vector3 p_frontiere) //frontière de l'écran
    {
        frontiere = p_frontiere;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "DetectBut1")
        {
            ButNinja++;
            Score2.text = ButNinja.ToString(); //Mise à joueur du score de ninja
                                              
            Repositionnement();

        }

        if (collision.gameObject.name == "DetectBut2")
        {
            ButCowBoy++;
            Score1.text = ButCowBoy.ToString(); //Mise à jour du score de cowboy

            Repositionnement();

        }
    }

    public void Repositionnement() //permet de remettre les joueurs et le ballon en position de base
    {
        //reposition du joueur 1
        Joueur1.transform.position = new Vector3(-4, -1, -0.6f);
        Joueur1.GetComponent<Rigidbody2D>().Sleep();
        Joueur1.GetComponent<SpriteRenderer>().flipX = false;


        //reposition du joueur2
        Joueur2.transform.position = new Vector3(4, -1, -0.6f);
        Joueur2.GetComponent<Rigidbody2D>().Sleep();
        Joueur2.GetComponent<SpriteRenderer>().flipX = true;


        //reposition du ballon
        transform.position = new Vector3(0, 1, -0.6f);
        GetComponent<Rigidbody2D>().Sleep();
        GetComponent<Rigidbody2D>().rotation = 0;
    }

    public string ResultatFinaux()
    {
        string resultat;

        if(ButCowBoy > ButNinja)
        {
            resultat = "CowBoy";
        }
        else if(ButNinja > ButCowBoy)
        {
            resultat = "Ninja";
        }
        else
        {
            resultat = "Egalité !";
        }

        return resultat;
    }
}
