namespace VRTK.Examples
{
    using UnityEngine;
    using VRTK.Controllables;

    public class ElevatorControl : MonoBehaviour
    {
        public VRTK_BaseControllable controllable;
        public GameObject platform;
        public GameObject pivotPoint;
        public GameObject wheel;
        public float speed;
        public float value;
        public float lastValue = 0;
        public int side = 1;

        private void Update()
        {
            if (wheel == null)
            {
                if (controllable.gameObject.name == "WheelControl1")
                {
                    wheel = GameObject.Find("[VRTK][AUTOGEN][WheelControl1][Controllable][ArtificialBased][RotatorContainer]");
                    lastValue = wheel.transform.rotation.y;
                }

                if (controllable.gameObject.name == "WheelControl2")
                {
                    wheel = GameObject.Find("[VRTK][AUTOGEN][WheelControl2][Controllable][ArtificialBased][RotatorContainer]");
                    lastValue = wheel.transform.rotation.y;
                }
            }

            if (lastValue < wheel.transform.rotation.y)
            {
                Debug.Log(false);
                side = (-1);
            }
            else if (lastValue > wheel.transform.rotation.y)
            {
                Debug.Log(true);
                side = 1;
            }

            lastValue = wheel.transform.rotation.y;
        }

        protected virtual void OnEnable()
        {
            if (controllable != null)
            {
                controllable.ValueChanged += ValueChanged;
            }
        }

        protected virtual void OnDisable()
        {
            if (controllable != null)
            {
                
                controllable.ValueChanged -= ValueChanged;
            }
        }

        [System.Obsolete]
        protected virtual void ValueChanged(object sender, ControllableEventArgs e)
        {
            if (platform != null)
            {
                platform.transform.RotateAround(pivotPoint.transform.position, Vector3.up, (speed * e.normalizedValue * Time.deltaTime));
                platform.transform.Translate(side * Vector3.forward * (e.normalizedValue * Time.deltaTime));
                
            }
        }
    }
}