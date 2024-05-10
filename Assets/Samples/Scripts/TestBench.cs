using Unity.Cinemachine;
using UnityEngine;

namespace Assets.Samples
{

    public class TestBench: MonoBehaviour
    {
        public Transform character;
        public int frameRate = 30;
        public float timeScale = 0.5f;
        public Transform viewPoint1;
        public Transform viewPoint2;
        public float fixedTime = 0.2f;

        public CinemachineCamera[] cameras { get; set; }
    }
}
