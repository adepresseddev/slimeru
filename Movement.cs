using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    //Variavel para armazenar o RigidBody2d do personagem
    private Rigidbody2D playerRb;
    //Variavel para armazenar o Animator do personagem
    private Animator playerAnimator;

    //Variavel para determinar a velocidade do personagem
    public float speed;
    //Variavel para determinar a força do pulo do personagem
    public float jumpForce;

    //Variavel para saber se o personagem está olhando para esquerda ou direita
    public bool isLookLeft;
    //Variavel para saber se o personagem pode dar o double jump
    public bool doubleJump;

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

            //Pega o Rigidbody2D do personagem e armazena na variavel playerRb
            playerRb = GetComponent<Rigidbody2D>();
            //Pega o Animator do personagem e armazena na variavel playerAnimator
            playerAnimator = GetComponent<Animator>();

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

        //if para fazer o personagem pular e faz ele poder dar o doublejump
        if(Input.GetButtonDown("Jump") )
        {

            if (isGrounded)
            {

                if (!isCeiled)
                {

                    DoJump();
                    doubleJump = true;

                } 

            }
            else if (!isGrounded && doubleJump)
            {

                DoJump();
                doubleJump = false;

            }

        }

        //Faz o personagem andar
        playerRb.velocity = new Vector2(h * speed, speedY);

        //Determina o parametro h do animator para receber o valor da variavel h
        playerAnimator.SetInteger("h", (int)h);

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

    //Código para fazer ele pular
    void DoJump()
    {

        playerRb.AddForce(new Vector2(0, jumpForce));

    }

}


