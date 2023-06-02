using System;
using UnityEngine;

namespace PID
{
   [Serializable]
   public class LinearPIDController
   {
      public enum DerivativeMeasurement
      {
         Velocity,
         ErrorRateOfChange
      }

      public float proportionalGain;
      public float integralGain;
      public float integralSaturation = 1.0f;
      public float derivativeGain;

      public float outputMin = -1.0f;
      public float outputMax = 1.0f;

      public DerivativeMeasurement derivativeMeasurement;

      private float previousError;
      private float previousValue;
      private float integrationStored;

      private bool derivativeInitialized;

      public void Reset()
      {
         derivativeInitialized = false;
      }

      public float Update(float dt, float currentVal, float targetVal)
      {
         float error = targetVal - currentVal;

         //calculate the P Term
         float p = proportionalGain * error;

         //calculate the D Term as rate of change
         float errorRateOfChange = (error - previousError) / dt;

         //calculate the D Term as a velocity
         float valueRateOfChange = (currentVal - previousValue) / dt;

         float deriveMeasure = 0;

         if (derivativeInitialized)
         {
            if (derivativeMeasurement == DerivativeMeasurement.Velocity)
            {
               deriveMeasure = -valueRateOfChange;
            }
            else
            {
               deriveMeasure = errorRateOfChange;
            }
         }
         else
         {
            derivativeInitialized = true;
         }

         float d = derivativeGain * deriveMeasure;

         //calculate the I Term

         integrationStored = Mathf.Clamp(integrationStored + (error * dt), -integralSaturation, integralSaturation);
         float i = integralGain * integrationStored;

         //store the error for the next step
         previousError = error;
         //store the value for the next step
         previousValue = currentVal;

         return Mathf.Clamp(p + i + d, outputMin, outputMax);
      }
   }
}