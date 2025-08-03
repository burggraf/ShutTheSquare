using UnityEngine;
using System;

namespace ShutTheSquare.Core
{
    public class DiceManager : MonoBehaviour
    {
        public static DiceManager Instance { get; private set; }
        
        public event Action<int, int> OnDiceRollComplete;
        public event Action OnDiceRollStarted;
        
        public bool IsRolling { get; private set; }
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        private void Start()
        {
            CreateDice();
        }
        
        private void CreateDice()
        {
            // Create ground plane to catch dice
            GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
            ground.name = "Ground";
            ground.transform.position = Vector3.zero;
            ground.transform.localScale = Vector3.one * 2;
            ground.GetComponent<Renderer>().material.color = Color.green;
            
            // Create two simple cube dice
            GameObject dice1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            dice1.name = "Dice1";
            dice1.transform.position = new Vector3(-0.5f, 2f, 0f);
            dice1.AddComponent<Rigidbody>();
            
            GameObject dice2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            dice2.name = "Dice2";
            dice2.transform.position = new Vector3(0.5f, 2f, 0f);
            dice2.AddComponent<Rigidbody>();
            
            Debug.Log("Dice and ground created");
        }
        
        public void RollDice()
        {
            if (IsRolling) return;
            
            IsRolling = true;
            OnDiceRollStarted?.Invoke();
            
            // Apply forces to dice for realistic rolling
            GameObject dice1 = GameObject.Find("Dice1");
            GameObject dice2 = GameObject.Find("Dice2");
            
            if (dice1 != null && dice2 != null)
            {
                Rigidbody rb1 = dice1.GetComponent<Rigidbody>();
                Rigidbody rb2 = dice2.GetComponent<Rigidbody>();
                
                // Reset positions
                dice1.transform.position = new Vector3(-0.5f, 2f, 0f);
                dice2.transform.position = new Vector3(0.5f, 2f, 0f);
                
                // Apply random forces
                Vector3 force1 = new Vector3(UnityEngine.Random.Range(-3f, 3f), UnityEngine.Random.Range(2f, 5f), UnityEngine.Random.Range(-3f, 3f));
                Vector3 force2 = new Vector3(UnityEngine.Random.Range(-3f, 3f), UnityEngine.Random.Range(2f, 5f), UnityEngine.Random.Range(-3f, 3f));
                
                rb1.AddForce(force1, ForceMode.Impulse);
                rb2.AddForce(force2, ForceMode.Impulse);
                
                rb1.AddTorque(UnityEngine.Random.insideUnitSphere * 5f, ForceMode.Impulse);
                rb2.AddTorque(UnityEngine.Random.insideUnitSphere * 5f, ForceMode.Impulse);
            }
            
            // Wait for dice to settle then give result
            StartCoroutine(SimulateRoll());
        }
        
        private System.Collections.IEnumerator SimulateRoll()
        {
            yield return new WaitForSeconds(1f);
            
            int die1 = UnityEngine.Random.Range(1, 7);
            int die2 = UnityEngine.Random.Range(1, 7);
            
            Debug.Log($"Dice rolled: {die1}, {die2}");
            
            IsRolling = false;
            OnDiceRollComplete?.Invoke(die1, die2);
        }
    }
}