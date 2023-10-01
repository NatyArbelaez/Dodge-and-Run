using UnityEngine;

public class MovementCharacter : MonoBehaviour
{
    public float velocidadNormal = 150.0f;
    public float velocidadAcelerada = 200.0f;
    public float velocidadRotacion = 30.0f;

    private Rigidbody rb;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        float velocidad = Input.GetKey(KeyCode.Space) ? velocidadAcelerada : velocidadNormal;
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        Vector3 movimiento = new Vector3(movimientoHorizontal, 0.0f, movimientoVertical) * velocidad;

        rb.velocity = new Vector3(movimiento.x, rb.velocity.y, movimiento.z);

        if (movimiento != Vector3.zero)
        {
            Quaternion rotacionObjetivo = Quaternion.LookRotation(movimiento);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, Time.deltaTime * velocidadRotacion);

            //estaMoviendo = true;
            animator.SetBool("Walk", true);
        }
        else
        {
            //estaMoviendo = false;
            animator.SetBool("Walk", false);
        }
    }

}
