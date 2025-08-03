using UnityEngine;
using System;

namespace ShutTheSquare.Utilities
{
    public class MobileInputDetector : MonoBehaviour
    {
        [Header("Shake Detection")]
        [SerializeField] private float shakeThreshold = 2.0f;
        [SerializeField] private float shakeCooldown = 1.0f;
        
        private float lastShakeTime;
        private Vector3 lastAcceleration;
        
        public event Action OnShakeDetected;
        public event Action<Vector2> OnTouch;
        public event Action<Vector2> OnTouchEnd;
        
        private void Start()
        {
            lastAcceleration = Input.acceleration;
            
#if UNITY_ANDROID || UNITY_IOS
            // Enable accelerometer
            Input.gyro.enabled = true;
#endif
        }
        
        private void Update()
        {
            DetectShake();
            DetectTouch();
        }
        
        private void DetectShake()
        {
#if UNITY_ANDROID || UNITY_IOS
            Vector3 acceleration = Input.acceleration;
            Vector3 deltaAcceleration = acceleration - lastAcceleration;
            float deltaAccelerationMagnitude = deltaAcceleration.magnitude;
            
            if (deltaAccelerationMagnitude > shakeThreshold && 
                Time.time > lastShakeTime + shakeCooldown)
            {
                lastShakeTime = Time.time;
                OnShakeDetected?.Invoke();
                Debug.Log("Shake detected for dice roll!");
            }
            
            lastAcceleration = acceleration;
#else
            // Desktop testing - use spacebar as shake substitute
            if (Input.GetKeyDown(KeyCode.Space) && 
                Time.time > lastShakeTime + shakeCooldown)
            {
                lastShakeTime = Time.time;
                OnShakeDetected?.Invoke();
                Debug.Log("Shake detected (spacebar) for dice roll!");
            }
#endif
        }
        
        private void DetectTouch()
        {
#if UNITY_ANDROID || UNITY_IOS
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                
                if (touch.phase == TouchPhase.Began)
                {
                    OnTouch?.Invoke(touch.position);
                }
                else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    OnTouchEnd?.Invoke(touch.position);
                }
            }
#else
            // Desktop testing - use mouse as touch substitute
            if (Input.GetMouseButtonDown(0))
            {
                OnTouch?.Invoke(Input.mousePosition);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                OnTouchEnd?.Invoke(Input.mousePosition);
            }
#endif
        }
    }
}