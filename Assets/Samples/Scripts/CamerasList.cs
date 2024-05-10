using Unity.Cinemachine;
using UnityEngine;

namespace Assets.Samples.Scripts
{
    [ExecuteInEditMode]
    internal class CamerasList: MonoBehaviour
    {
        public CinemachineCamera[] cameras;
        void Start()
        {
            var bench = GetComponent<TestBench>();
            bench.cameras = cameras;
        }
    }
}
