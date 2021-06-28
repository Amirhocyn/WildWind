using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WildWind.Movement
{

    public class Mover : MonoBehaviour
    {

        [SerializeField] float turnAngle;
        [SerializeField] float speed;

        private void Update()
        {

            MoveForward();

        }   

        public void Turn(float dir)
        {

            transform.Rotate(Vector3.up * dir * Time.deltaTime * turnAngle);

        }

        public float GetTurnAngle()
        {

            return turnAngle;

        }

        public float GetSpeed()
        {

            return speed;

        }

        public void SetSpeed(float speed)
        {

            this.speed = speed;

        }

        private void MoveForward()
        {

            transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);

        }

    }

}
