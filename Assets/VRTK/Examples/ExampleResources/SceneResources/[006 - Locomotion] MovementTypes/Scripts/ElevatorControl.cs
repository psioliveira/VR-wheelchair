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
        private float oldrot, newrot;
        private int direction = 0;
 
        private void LateUpdate()
        {
            newrot = controllable.GetValue();
        }

        private void Update()
        {
            if(oldrot > newrot)
            {
                direction = -1;
            }
            if(oldrot < newrot)
            {
                direction = 1;
            }
            oldrot = newrot;
            //if (Vector3.Dot(fwd, movement) < 0)
            //{
            //    Debug.Log("Back");
            //    direction = -1;
            //}
            //else if (Vector3.Dot(fwd, movement) > 0)
            //    direction = 1;
            //else
            //{
            //    Debug.Log("Forward");
            //    direction = 0;
            //}
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
            Debug.Log(pivotPoint.transform.position);
            if (platform != null)
            {
                if(direction == 1)
                {

                	if(wheel.name == "Wheel1"){
                		 platform.transform.RotateAround(pivotPoint.transform.position, Vector3.up, -(speed * e.normalizedValue * Time.deltaTime));
                    platform.transform.Translate(side * Vector3.forward * (e.normalizedValue * Time.deltaTime));
                	}

                	if(wheel.name == "Wheel2"){
                		 platform.transform.RotateAround(pivotPoint.transform.position, Vector3.up, (speed * e.normalizedValue * Time.deltaTime));
                    platform.transform.Translate(side * Vector3.forward * (e.normalizedValue * Time.deltaTime));
                	}
                   

                }
                else if(direction == -1)
                {
					if(wheel.name == "Wheel1"){
					platform.transform.RotateAround(pivotPoint.transform.position, Vector3.up, -(speed * e.normalizedValue * Time.deltaTime));
                    platform.transform.Translate(side * Vector3.forward * -(e.normalizedValue * Time.deltaTime));	
					}

					if(wheel.name == "Wheel2"){
					platform.transform.RotateAround(pivotPoint.transform.position, Vector3.up, (speed * e.normalizedValue * Time.deltaTime));
                    platform.transform.Translate(side * Vector3.forward * -(e.normalizedValue * Time.deltaTime));	
					}                    
                }
            }
        }
    }
}