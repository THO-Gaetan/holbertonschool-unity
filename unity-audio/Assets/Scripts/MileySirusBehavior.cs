using UnityEngine;

public class MileySirusBehavior : MonoBehaviour
{
    [SerializeField] private float launchForce = 20f;
    [SerializeField] private float upwardBoost = 5f;
    [SerializeField] private PlayerController PlayerControllerScript;
    [SerializeField] private Rigidbody PlayerRigidbody;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            if (PlayerRigidbody != null && PlayerControllerScript != null)
            {
                Vector3 dir = collision.transform.position - transform.position;
                dir.y = 0;
                dir.Normalize();

                PlayerControllerScript.StunUntilGrounded();
                PlayerRigidbody.AddForce(dir * launchForce + Vector3.up * upwardBoost, ForceMode.Impulse);
            }
        }
    }
}
