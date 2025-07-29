using UnityEngine;

namespace Content.Features.SolverApplicationBootstrap.Scripts
{
    public class SolverApplicationBootstrap : MonoBehaviour
    {
        [SerializeField] private SolverApplication.Scripts.SolverApplication _solverApplication;

        private void Start()
        {
            Instantiate(_solverApplication);
        }
    }
}