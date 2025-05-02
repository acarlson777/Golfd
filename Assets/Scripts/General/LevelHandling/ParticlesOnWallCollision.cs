using UnityEngine;
using System.Collections;

//NOT COMPLETE (5/2/2025)
public class ParticlesOnWallCollision : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;

    public void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("Wall")){
            var emitParams = new ParticleSystem.EmitParams();
            //particleSystem.Emit(transform.position);
        }
    }
}