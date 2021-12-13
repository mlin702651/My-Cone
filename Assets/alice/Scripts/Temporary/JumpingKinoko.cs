using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingKinoko : MonoBehaviour
{
    float mass = 3.0f; // defines the character mass
    [SerializeField]float jumpingForce = 100.0f; // defines the character mass
    Vector3 impact = Vector3.zero;
    [SerializeField]private CharacterController character;
 
  
 
 // call this function to add an impact force:
 void AddImpact(Vector3 dir, float force){
   dir.Normalize();
   if (dir.y < 0) dir.y = -dir.y; // reflect down force on the ground
   impact += dir.normalized * force / mass;
 }
 private void Update() {
     // apply the impact force:
   if (impact.magnitude > 0.2) character.Move(impact * Time.deltaTime);
   // consumes the impact energy each cycle:
   impact = Vector3.Lerp(impact, Vector3.zero, 5*Time.deltaTime);
 }
 
 private void OnTriggerEnter(Collider other) {
     AddImpact(new Vector3(0,1,0), jumpingForce);
     Debug.Log("enter jump");
 }
}
