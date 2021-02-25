using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


    [RequireComponent(typeof(ARRaycastManager))]
    public class PlaceOnPlane : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Instantiates this prefab on a plane at the touch location.")]
        GameObject m_PlacedPrefab;
        public GameObject placedPrefab
        {
            get { return m_PlacedPrefab; }
            set { m_PlacedPrefab = value; }
        }

    [SerializeField]
        Mediator mediator;
        public GameObject spawnedObject { get; private set; }

        ARRaycastManager m_RaycastManager;

    public UnityEvent cubeSpawned;

    [HideInInspector]
        public ARPlaneManager _planeManager;
    [HideInInspector]
        public ARPointCloudManager _cloudManager;
        static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

        void Awake()
        {
            m_RaycastManager = GetComponent<ARRaycastManager>();
            _cloudManager = GetComponent<ARPointCloudManager>();
            _planeManager = GetComponent<ARPlaneManager>();
        }


        void Update()
        {
            RayCaster();
        }


        private void RayCaster()
        {
            if (!TryGetTouchPosition(out Vector2 touchPosition))
                return;

        cubeSpawned.Invoke();

            if (m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon) && !(mediator._trackerController.isClicked))
            {

                var hitPose = s_Hits[0].pose;

                spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation);

            }
        }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }

}