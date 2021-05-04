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
    //Variavel para determinar a for�a do pulo do personagem
    public float jumpForce;

    //Variavel para saber se o personagem est� olhando para esquerda ou direita
    public bool isLookLeft;
    //Variavel para saber se o personagem pode dar o double jump
    public bool doubleJump;

    //Armazena o objeto que vai ser utilizado para fazer a verifica��o se o personagem est� no ch�o
    public Transform groundCheck;
    //Variavel para saber se o personagem est� no ch�o
    public bool isGrounded;

    //Armazena o objeto que vai ser utilizado para fazer a verifica��o se o personagem est� com algo a cima da cabe�a
    public Transform ceilingCheck;
    //Variavel para saber se personagem est� com a cabe�a batendo em algum objeto
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

        //if para chamar o c�digo Flip()
        if(h > 0 && isLookLeft)
        {

            Flip();

        } else if (h < 0 && !isLookLeft)
        {

            Flip();

        }

        //Armazena a posi��o vertical do personagem
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

        //Seta uma regi�o em volta dos objetos
        isCeiled = Physics2D.OverlapCircle(ceilingCheck.position, 0.02f);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f);

    }

    //C�digo para fazer o personagem girar
    void Flip()
    {

        isLookLeft = !isLookLeft;

        transform.Rotate(0f, 180f, 0f);

    }

    //C�digo para fazer ele pular
    void DoJump()
    {

        playerRb.AddForce(new Vector2(0, jumpForce));

    }

}


