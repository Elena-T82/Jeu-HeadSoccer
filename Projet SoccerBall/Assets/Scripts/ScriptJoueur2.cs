using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptJoueur2 : MonoBehaviour
{
    float vitesse = 10f;
    public Vector3 frontiere;
    public GameObject Ballon;
    bool SautPossible;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Controle de mouvement du joueur2

        if (Input.GetKey(KeyCode.RightArrow)) //GetKey : tant que la key est appuyée
        {
            transform.position += new Vector3(0.7f, 0, 0) * vitesse * Time.deltaTime;
            GetComponent<Animator>().SetFloat("Speed", 2);
            transform.GetComponent<SpriteRenderer>().flipX = false;

        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-0.7f, 0, 0) * vitesse * Time.deltaTime;
            GetComponent<Animator>().SetFloat("Speed", 2);
            transform.GetComponent<SpriteRenderer>().flipX = true;

        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) //GetKeyDown : si la key est appuyée
        {
            if (SautPossible)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 5);
            }
        }
        if (Input.GetKeyUp(KeyCode.RightArrow)) //GetKeyUp : quand la key est relachée
        {
            GetComponent<Animator>().SetFloat("Speed", 0);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow)) //GetKeyUp : quand la key est relachée
        {
            GetComponent<Animator>().SetFloat("Speed", 0);
        }


        //Empêcher les sorties d'écran
        Vector3 pos = transform.position;

        if (pos.x > frontiere.x)
        {
            pos.x = frontiere.x;
        }

        if (pos.x < -frontiere.x)
        {
            pos.x = -frontiere.x;
        }

        if (pos.y > frontiere.y)
        {
            pos.y = frontiere.y;
        }

        if (pos.y < -frontiere.y)
        {
            pos.y = -frontiere.y;
        }

        transform.position = pos;

    }

    public void getFrontiere(Vector3 p_frontiere)
    {
        frontiere = p_frontiere;
    }

    private void OnCollisionEnter2D(Collision2D collision) //frontiere de l'écran
    {
        if (collision.gameObject.name == "Terrain")
        {
            SautPossible = true;
            GetComponent<Animator>().SetFloat("Jump", 0);
        }

        if (collision.gameObject.name == "Ballon") //Tirer dans le ballon
        {
            StartCoroutine(AnimBallon()); //J'appelle ma coroutine

            if (transform.GetComponent<SpriteRenderer>().flipX)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-250, 50));
            }
            else
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(250, 50));
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision) //Empêcher le double saut
    {
        if (collision.gameObject.name == "Terrain")
        {
            SautPossible = false;
            GetComponent<Animator>().SetFloat("Jump", 2);
        }
    }

    IEnumerator AnimBallon() //Cette fonction est une coroutine, elle permet de faire des temps d'attente pendant l'execution d'un code.
    {
        Ballon.GetComponent<Animator>().SetFloat("Enerve", 1);

        yield return new WaitForSeconds(2);

        Ballon.GetComponent<Animator>().SetFloat("Enerve", 0);

    }
}
