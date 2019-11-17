namespace VRTK.Examples
{
    using UnityEngine;
    using VRTK.Controllables;

    public class ElevatorControl : MonoBehaviour
    {
        public VRTK_BaseControllable controllable;
        public GameObject platform;
        public GameObject pivotPoint;
        public float speed;

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
                platform.transform.Translate(Vector3.forward * (e.normalizedValue * Time.deltaTime));
            }
        }
    }
}