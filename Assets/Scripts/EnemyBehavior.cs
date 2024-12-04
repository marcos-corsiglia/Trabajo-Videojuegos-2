using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {
    public GameObject player;
    public Animator animator;
    public GameObject portal;

    public int health = 3;
    public int damage = 1;
    public float attackingSpeed = 1f;
    public float speed = 3;
    public float alertDistance = 5;

    private float distance;
    private bool enemieAlerted = false;
    private bool canAttack = true;
    public bool isFacingRight = false;
    private float lastEnemyPositionX;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayer;

    private void Start() {
        lastEnemyPositionX = transform.position.x;
    }

    // Update is called once per frame
    void Update() {
        checkIfDead();
        Movement();
        
        Flip();
    }

    // Funcion para verificar si el enemigo tiene vida, si no es asi sera eliminado.
    private void checkIfDead() {
        if (health < 1) {
            portal.GetComponent<PortalBehaviour>().cantEnemies -= 1;
            Debug.Log("Cantidad de enemigos: " + portal.GetComponent<PortalBehaviour>().cantEnemies);

            portal.GetComponent<PortalBehaviour>().PlayDeathSound();

            Destroy(gameObject);
        }
    }

    private void Movement() {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;

        if (distance < alertDistance) {
            enemieAlerted = true;
            animator.SetBool("IsWalking", true);
        }

        if (enemieAlerted)
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
    }

    private void Flip() {
        if ((transform.position.x > lastEnemyPositionX && !isFacingRight) || 
            (transform.position.x < lastEnemyPositionX) && isFacingRight) {
                Vector3 localScale = transform.localScale;
                localScale.x *= -1;
                transform.localScale = localScale;
                isFacingRight = !isFacingRight;
        }

        lastEnemyPositionX = transform.position.x;
    }


    // Ataque del enemigo:

    
    private IEnumerator OnTriggerStay2D(Collider2D other) {
        // Verificamos si el elemento que entro en el trigger es el jugador y si el enemigo tiene permitido atacar.
        if (other.CompareTag("Player") && canAttack) {
            canAttack = false;
            Debug.Log("El jugador ha entrado en el trigger!");
            player.GetComponent<PlayerCombat>().health -= damage;
            yield return new WaitForSeconds(attackingSpeed);
            canAttack = true;
        }
    }

    private void basicAttack() {
        // Detectamos si el jugador se encuentra dentro del area de ataque.
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        Debug.Log("Se ataco al jugador");
        player.GetComponent<PlayerCombat>().PlayDamagedSound();
        player.GetComponent<PlayerCombat>().health -= damage;
    }
    

    private void OnDrawGizmosSelected() {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
