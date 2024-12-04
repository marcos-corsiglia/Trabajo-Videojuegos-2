using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour {
    public Animator animator;
    public PlayerMovement playerMovement;

    public int health = 25;
    public int damage = 1;
    public float attackingSpeed = 1f; // Cada cuantos segundos danna a los enemigos.
    //public float attackingDelay = 3f; // Cada cuantos segundos se podra volver a atacar de nuevo.
    public float attackingMoveSpeed = 2f; // Velocidad de movimiento del jugador mientras ataca.
    private float walkingMoveSpeed;
    private bool canAttack = true;
    private AudioSource screamAttackAudioSource;
    private AudioSource SwordAudioSource;
    private AudioSource DamagedAudioSource;

    public AudioClip screamAttackingAudioClip;
    public AudioClip SwordAudioClip;
    public AudioClip DamagedAudioClip;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    
    void Start() {
        walkingMoveSpeed = playerMovement.moveSpeed;

        //Agrego componentes y clips de audio a los audio source

        // Scream Attack
        screamAttackAudioSource = gameObject.AddComponent<AudioSource>();
        screamAttackAudioSource.clip = screamAttackingAudioClip;
        screamAttackAudioSource.loop = true;

        // Sword
        SwordAudioSource = gameObject.AddComponent<AudioSource>();
        SwordAudioSource.clip = SwordAudioClip;
        SwordAudioSource.loop = true;
    
        // Damaged
        DamagedAudioSource = gameObject.AddComponent<AudioSource>();
        DamagedAudioSource.clip = DamagedAudioClip;
    }

    void Update() {
        checkIfDead();

        if (Input.GetKeyDown(KeyCode.Space) && canAttack)
            StartBasicAtack();
        else if (Input.GetKeyUp(KeyCode.Space) && canAttack) 
            EndBasicAttack();
    }
        
    //Dentro de esta funcion se iniciara la animacion de ataque, detectara a los enemigos dentro del rango y les hara danno
    private void StartBasicAtack() {
        // Comienzo la animacion y sonido de ataque.
        animator.SetBool("IsAttacking", true);
        screamAttackAudioSource.Play();

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
                    //yield return new WaitForSeconds(time);

                    if (!SwordAudioSource.isPlaying)
                        SwordAudioSource.Play();

                    Debug.Log("Se ataco a " + enemy.name);
                    enemy.GetComponent<EnemyBehavior>().health -= 1;
                }
                else if (enemy.CompareTag("BossFire")) { // En caso que se ataque a la pelota del ultimo piso del juego.
                    enemy.GetComponent<BossFire>().TakeDamage(damage);
                    Debug.Log("Se ataco a la pelota del enemigo");
                } 
            }

            // Pausamos el audio source de la espada cuando no se esten atacando enemigos.
            if (hitEnemies.Length == 0) 
                SwordAudioSource.Stop();

            //yield break;
        }
    }

    private void EndBasicAttack() {
        animator.SetBool("IsAttacking", false);
        screamAttackAudioSource.Stop();
        SwordAudioSource.Stop();
        playerMovement.moveSpeed = walkingMoveSpeed;
        blockAttack();
    }

    private IEnumerator blockAttack() {
        canAttack = false;
        yield return new WaitForSeconds(3);
        canAttack = true;
    }

    private void checkIfDead() {
        if (health < 1) { 
            Destroy(gameObject);
            SceneManager.LoadScene("Game Over");
        }
    }

    public void PlayDamagedSound() { 
        DamagedAudioSource.Play();
    }

    private void OnDrawGizmosSelected() {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}