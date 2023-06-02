using UnityEngine;

namespace PID
{
    public class LinearTestController : MonoBehaviour
    {
        public Vector3 target = new(0, 0, 0);
        public LinearPIDController pidController;
        public Rigidbody rigid;
        public float force = 10.0f;

        private void FixedUpdate()
        {
            float y = pidController.Update(Time.fixedDeltaTime, rigid.position.y, target.y);
            rigid.AddForce(new Vector3(0, y, 0) * force);
        }
    }
}