using UnityEngine;

namespace Dev.NucleaTNT.Squared.Gameplay
{
	public class ClimbGameDeathBarrier : DeathBarrier
	{
	 	[SerializeField] private float _moveSpeed = 1, _minIntensity = 1, _maxIntensity = 50, _intensityIncRate = 1;
	    private float _intensity => Mathf.Clamp(Time.timeSinceLevelLoad / (1 / _intensityIncRate), _minIntensity, _maxIntensity);

	    private void Update() 
		{
			transform.position += new Vector3(0, _moveSpeed * _intensity * Time.deltaTime, 0);
		}
	}
}
