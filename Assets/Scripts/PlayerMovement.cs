using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour {
    public float moveSpeed = 5f;
    public bool isFacingRight = true;
    private float flipDistance = 1f;
    public Rigidbody2D rb;
    public Animator animator;
    public GameObject pauseMenu;
    Vector2 movement;

    public float dashSpeed = 10f;
    public float dashDuration = 1f;
    public float dashCooldown = 1f;

    private bool isDashing = false;
    private bool canDash = true;
    private bool pauseMenuActive = false; // Determina si el menu de pausa esta activo.
    public TrailRenderer tr;

    void Update() {
        // Utilizamos Update() para instrucciones referentes al Input.
        if (isDashing)
            return;
        
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.K) && canDash && (movement.x != 0 || movement.y != 0))
            StartCoroutine(Dash());

        if (Input.GetKeyDown(KeyCode.P) && !pauseMenuActive) {
            pauseMenuActive = true;
            pauseMenu.GetComponent<PauseMenu>().StartMenu();
        }
        else if (Input.GetKeyDown(KeyCode.P) && pauseMenuActive) {
            pauseMenuActive = false;
            pauseMenu.GetComponent<PauseMenu>().ResumeGame();
        }
    }

    private void FixedUpdate() {
        // Utilizamos FixedUpdate() para instrucciones referentes al movimiento del personaje.
        if (isDashing)
            return;

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        FlipCharacter();
        SetIsWalking();
    }

    // Funcion para rotar la direccion del personaje
    private void FlipCharacter() {
        if (isFacingRight && movement.x < 0f || !isFacingRight && movement.x > 0f) {
            // Muevo un poco al personaje para que se de vuelta en la misma posicion.
            Vector2 newPosition = transform.position;

            if (isFacingRight)
                newPosition.x = newPosition.x - flipDistance;
            else
                newPosition.x = newPosition.x + flipDistance;

            transform.position = newPosition;

            // Doy vuelta el elemento que representa al jugador.
            isFacingRight = !isFacingRight;
            Vector2 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }

    }
    
    // Sirve para cambiar el estado del parametro 'IsWalking' dentro del animator.
    private void SetIsWalking() {
        if (movement.x != 0 || movement.y != 0)
            animator.SetBool("IsWalking", true);
        else
            animator.SetBool("IsWalking", false);
    }

    private IEnumerator Dash() {
        canDash = false;
        isDashing = true;
        tr.emitting = true;

        rb.velocity = new Vector2(movement.x * dashSpeed, movement.y * dashSpeed);
        
        yield return new WaitForSeconds(dashDuration);
        tr.emitting = false;
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
