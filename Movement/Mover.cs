﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG2.Core;

namespace RPG2.Movement
{

    public class Mover : MonoBehaviour, IAction
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

        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);

        }

        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }
    }
}
