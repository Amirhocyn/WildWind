using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WildWind.Movement
{

    public class Mover : MonoBehaviourMaster<Mover>
    {

        [SerializeField] float turningSpeed;
        [SerializeField] float maxTurnAngle;
        [SerializeField] float speed;
        [SerializeField] Transform mesh;
        [SerializeField, Range(0.01f, 10)] float smoothedSteeringMultiplier;
        [SerializeField, Range(0.1f, 1000)] float minimumLerpMultiplier;
        private float targetZRotation = 0;
        private float rotationDirection = 0;
        private float delayedRotationDirection = 0;

        public void Update()
        {

            MoveForward();
            Rotate();

            delayedRotationDirection = Mathf.Lerp
                (
                delayedRotationDirection,
                rotationDirection,
                Mathf.Lerp(Time.deltaTime * minimumLerpMultiplier, 1, Mathf.Clamp01(Mathf.Abs(rotationDirection - delayedRotationDirection))) *
                Mathf.Lerp(Time.deltaTime * minimumLerpMultiplier, 1, 1f - Mathf.Clamp01(Mathf.Abs(rotationDirection - delayedRotationDirection))) *
                smoothedSteeringMultiplier
                );

            transform.Rotate(Vector3.up * delayedRotationDirection * Time.deltaTime * maxTurnAngle);

        }

        private void Rotate()
        {

            if (mesh != null)
            {

                targetZRotation = Mathf.Clamp(delayedRotationDirection * (45f), -45, 45);
                float rot = mesh.localEulerAngles.z;
                if (rot > 180)
                    rot = 180 - (rot - 180);
                else
                    rot = -rot;
                float increaseDir = 0;
                if (targetZRotation - rot > 0)
                    increaseDir = -1;
                if (targetZRotation - rot < 0)
                    increaseDir = 1;

                increaseDir = Mathf.Clamp(-(targetZRotation - rot) * 0.33f, -1, 1);

                mesh.Rotate(Vector3.forward * Time.deltaTime * 45 * 3f * increaseDir);

            }

        }

        public void Turn(float dir)
        {

            rotationDirection = dir;

        }

        public float GetTurnAngle()
        {

            return maxTurnAngle;

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
