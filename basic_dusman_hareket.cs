using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class basic_dusman_hareket : MonoBehaviour
{
    public float speed;
    public bool movingRight = true;
    public Transform yerKontrol;
    public float mesafe;
    public Animator anim;


    private void Update()
    {
        //Düşman karakterin konumunu değiştiriyorum 
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D yerBilgisi = Physics2D.Raycast(yerKontrol.position, Vector2.down, mesafe);
        if(yerBilgisi.collider == false)
        {
            //eğer karakter sağ tarafta platformun sonuna geldi ise
            if(movingRight == true)
            {   //karakterin sprite'ini 180 derece çeviriyorum ve sol tarafa hareket ettiriyorum
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false; 
             
            }
            else
            {
                //sol tarafta ens ona geldi ise
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // düşman birim oyun karakterimiz ile etkileşime girdi ise
        // sahne yeniden başlatılıyor

        if(collision.gameObject.tag == "mainCharacter"){
            SceneManager.LoadScene("sahne_1");
        }
    }

}
