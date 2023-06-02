using UnityEngine;

namespace PID
{
    public class AngleTestController:MonoBehaviour
    {
        public Vector3 target = new(0, 0, 0);
        public AnglePIDController pidController;
        public Rigidbody rigid;
        public float force = 10.0f;
        
        private void FixedUpdate()
        {
            var forwardDir = rigid.rotation * Vector3.forward;

            var currentAngle = Vector3.SignedAngle(Vector3.forward, forwardDir, Vector3.up);
            
            float y = pidController.Update(Time.fixedDeltaTime, currentAngle, target.y);
            rigid.AddTorque(new Vector3(0, y, 0) * force);
        }
    }
}