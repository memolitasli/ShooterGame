using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hareket : MonoBehaviour
{

    public float speed;
    public float jumpforce;
    public float moveInput;
    public Rigidbody2D rb;
    private bool sagaDonuk = true;
    private bool karakterYerdemi;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    private int extraJump;
    public int extrajumpDeger;
    public Animator animator;


    // startmetodu program çalışğı anda çalışır. Başlangıçta olacak olan değerleri atamak için kullanıyorum
    void Start()
    {
        extraJump = extrajumpDeger;
    }

   /*Update metodu karede bir kere çalışabilir. FixedUpdate metodu ise karede birden fazla çalışır. Bu nedenle hızlı çalışmasına gerek
    olmayan zıplama gibi eylemler için update metodu kullanılırken hareket eylemleri veya karakterin herhangi bir oyun nesnesi
   ile temas edip etmediğini kontrol etmek için fixedUpdate metodunu kullanıyorum*/
    void Update()
    {
        // karakterim layer etiketi zemin olan nesnelerle temas halinde ise yapabileceği zıplama sayısını sabit bir değere eşitliyorum
        if(karakterYerdemi == true)
        {
            extraJump = extrajumpDeger;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && extraJump > 0)
        {
            rb.velocity = Vector2.up * jumpforce;
            extraJump--;
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow) && extraJump == 0 &&karakterYerdemi == true)
        {
            rb.velocity = Vector2.up * jumpforce;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            saldir();
        }
    }
    private void FixedUpdate()
    {
        // groundCheck karakterin zemin ile temas edip etmediğini kontrol edebilmem için oluşturduğum bir oyun objesi, 
        //groundCheck objesinin bulunduğu konumda checkRadius değerinin boyutunda bir çember oluşturuyorum ve eğer bu çember whatIsground 
        //adındaki layerlara temas ediyor ise karakterYerdemi true oluyor
        karakterYerdemi = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround); 
        //moveInput adlı değişkene kullanıcıdan girilen inputu atıyorum sağ tarafa doğru ise 0 ile 1, sol tara ise 0 -1 arasında bir değer alıyor
        moveInput = Input.GetAxisRaw("Horizontal");
        //karakterin animasyonlarını kontrol edebilmek için animasyon adında bir parametrem var içerisindeki speed değerine kullanıcının girdiği 
        // input değerinini pozitif olarak yolluyorum ki sağ veya sola doğru hareket ettiği zaman yürüme animasyonu devreye girsin ancak sabit durduğu
        //zaman idle animasyonu çalışsın
        animator.SetFloat("speed", Mathf.Abs(moveInput));
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        // karakter hangi tarafa hareket ediyor görselini o tarafa döndürüyorum
        if(sagaDonuk == false && moveInput >0)
        {
            karakterDondur();
        }
        else if(sagaDonuk == true && moveInput < 0)
        {
            karakterDondur();
        }
    
    }
    void karakterDondur()
    {
        // karakterin harekte ettiği tarafa doğru görselini çeviren fonksiyon
        sagaDonuk = !sagaDonuk;
        if (sagaDonuk) {
            transform.Rotate(0,-180, 0);
        }
        else
        {
            transform.Rotate(0,180, 0);
        } 

    }
    void saldir()
    {
        Debug.Log("Karakter Saldirdi...");
    }
}
