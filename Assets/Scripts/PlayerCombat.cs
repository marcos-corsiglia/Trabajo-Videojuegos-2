using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerCombat : MonoBehaviour {
    public Animator animator;
    public PlayerMovement playerMovement;

    public int health = 7;
    public int damage = 1;
    public float attackingSpeed = 1f; // Cada cuantos segundos danna a los enemigos.
    public float attackingMoveSpeed = 2f; // Velocidad de movimiento del jugador mientras ataca.
    private float walkingMoveSpeed;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    
    void Start() {
        walkingMoveSpeed = playerMovement.moveSpeed;
    }

    void Update() {
        checkIfDead();

        if (Input.GetKeyDown(KeyCode.Space))
            StartBasicAtack();
        else if (Input.GetKeyUp(KeyCode.Space))
            EndBasicAttack();
    }

    //Dentro de esta funcion se iniciara la animacion de ataque, detectara a los enemigos dentro del rango y les hara danno
    private void StartBasicAtack() {
        // Comienzo la animacion de ataque.
        animator.SetBool("IsAttacking", true);
        
        // Asigno la velocidad de movimiento en ataque a la velocidad de movimiento del personaje.
        playerMovement.moveSpeed = attackingMoveSpeed;

        //Ejecuto la coroutine de ataque
        if (Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(BasicAttack(attackingSpeed));
    }

    IEnumerator BasicAttack(float time) {
        while (Input.GetKey(KeyCode.Space)) {
            // Espero una cantidad de segundos
            yield return new WaitForSeconds(time);

            // Detectamos a los enemigos dentro del area de ataque.
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            // Dannamos a esos enemigos que se encuentran dentro del area.
            foreach (Collider2D enemy in hitEnemies) {
                if (enemy.CompareTag("Enemy")) {
                    Debug.Log("Se ataco a " + enemy.name);
                    enemy.GetComponent<EnemyBehavior>().health -= 1;
                }

                //if (enemy.gameObject.layer == LayerMask.NameToLayer("Enemies")) {
                //    Debug.Log("Se ataco a " + enemy.name);
                //    enemy.GetComponent<EnemyBehavior>().health -= 1;
                //}
            }
        }
    }

    private void EndBasicAttack() {
        animator.SetBool("IsAttacking", false);
        playerMovement.moveSpeed = walkingMoveSpeed;
    }

    private void checkIfDead() {
        if (health < 1)
            Destroy(gameObject);
    }

    private void OnDrawGizmosSelected() {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}