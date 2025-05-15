using UnityEngine;
using System.Collections;

//NOT COMPLETE (5/2/2025)
public class ParticlesOnWallCollision : MonoBehaviour
{
    [SerializeField] private ParticleSystem collisionParticleSystem;
    [SerializeField] private GameObject collisionParticleSystemGameObject;
    [SerializeField] private float particleDirectionStrength;
    [SerializeField] private AudioClip ballHitWall;

    public void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("Wall")){
            //collisionParticleSystemGameObject.GetComponent<ParticleSystemRenderer>().material = collision.gameObject.GetComponent<Material>();
            collisionParticleSystemGameObject.GetComponent<ParticleSystemRenderer>().material = collision.gameObject.GetComponent<MeshRenderer>().material;
            collisionParticleSystemGameObject.transform.position = collision.GetContact(0).point;

            var emitParams = new ParticleSystem.EmitParams();
            emitParams.velocity = (transform.position - collisionParticleSystemGameObject.transform.position).normalized * particleDirectionStrength;

            collisionParticleSystem.Emit(emitParams, Random.Range(1,8));
            AudioSource.PlayClipAtPoint(ballHitWall, collision.GetContact(0).point);
        }
    }
}