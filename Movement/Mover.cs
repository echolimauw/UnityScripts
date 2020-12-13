using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG2.Combat;

namespace RPG2.Movement
{

    public class Mover : MonoBehaviour
    {
        [SerializeField] Transform target;


        NavMeshAgent navMeshAgent;

        // Update is called once per frame
        void Update()
        {
            UpdateAnimator();
        }

        public void Start()
            {
                navMeshAgent = GetComponent<NavMeshAgent>();
            }

        private void MoveToCursor()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            bool hasHit = Physics.Raycast(ray, out hit);
            if (hasHit)
            {

                MoveTo(hit.point);
            }
        }

        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }

        

        public void Stop()
        {
            navMeshAgent.isStopped = true;
        }
    }
}
