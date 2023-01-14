using UnityEngine;

namespace UnityAdaptation.Utils
{
    public class SeparatedParticleSystem : MonoBehaviour
    {
        private Transform target;
        private Vector3 d;
        private ParticleSystem particleSystem;

        private void Start()
        {
            this.particleSystem = this.gameObject.GetComponent<ParticleSystem>();
            this.target = this.transform.parent;
            this.transform.parent = null;
        }

        private void Update()
        {
            if (this.target == null)
            {
                Destroy(gameObject);
                return;
            }
            
            d = this.target.position - transform.position;
            this.transform.position = this.target.position;
            this.particleSystem.enableEmission = this.d.sqrMagnitude < 1;
        }

    }
}