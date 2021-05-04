using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    //Variavel para armazenar o RigidBody2d do personagem
    private Rigidbody2D playerRb;

    //Variavel para determinar a velocidade do personagem
    public float speed;
    //Variavel para determinar a força do pulo do personagem
    public float jumpForce;

    //Variavel para saber se o personagem está olhando para esquerda ou direita
    public bool isLookLeft;

    //Armazena o objeto que vai ser utilizado para fazer a verificação se o personagem está no chão
    public Transform groundCheck;
    //Variavel para saber se o personagem está no chão
    public bool isGrounded;

    //Armazena o objeto que vai ser utilizado para fazer a verificação se o personagem está com algo a cima da cabeça
    public Transform ceilingCheck;
    //Variavel para saber se personagem está com a cabeça batendo em algum objeto
    public bool isCeiled;

    private void Start()
        {

            //Pega o Rigidbody2D do personagem e armazena na variavel
            playerRb = GetComponent<Rigidbody2D>();

        }

    private void Update()
    {

        //Armazena quando se aperta as setas direita e esquerda
        float h = Input.GetAxisRaw("Horizontal");

        //if para chamar o código Flip()
        if(h > 0 && isLookLeft)
        {

            Flip();

        } else if (h < 0 && !isLookLeft)
        {

            Flip();

        }

        //Armazena a posição vertical do personagem
        float speedY = playerRb.velocity.y;

        //if para fazer o personagem pular
        if(Input.GetButtonDown("Jump") && isGrounded == true && isCeiled == false)
        {

            playerRb.AddForce(new Vector2(0, jumpForce));

        }

        //Faz o personagem andar
        playerRb.velocity = new Vector2(h * speed, speedY);

    }

    private void FixedUpdate()
    {

        //Seta uma região em volta dos objetos
        isCeiled = Physics2D.OverlapCircle(ceilingCheck.position, 0.02f);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f);

    }

    //Código para fazer o personagem girar
    void Flip()
    {

        isLookLeft = !isLookLeft;

        transform.Rotate(0f, 180f, 0f);

    }

}


