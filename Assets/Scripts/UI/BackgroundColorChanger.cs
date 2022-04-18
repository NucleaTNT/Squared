using Dev.NucleaTNT.Squared.Managers;
using UnityEngine;
using UnityEngine.UI;


namespace Dev.NucleaTNT.PUNTesting.UI
{
	[RequireComponent(typeof(RawImage))]
	public class BackgroundColorChanger : MonoBehaviour
	{
		[SerializeField] private RawImage _image;
		[SerializeField, Range(0, 1)] private float _speed;
		private float _r => _image.color.r;
		private float _g => _image.color.g;
		private float _b => _image.color.b;

		private void Awake()
		{
			if (_image == null) _image = GetComponent<RawImage>();
			_image.color = PhotonManager.BackgroundImageColor;
		}

		private void IncrementColor()
		{
			/*
				Definitely a *much* faster way to compute this, but it works. 
				Checks which section of the color wheel the image is in then modifies
				the appropriate RBG value to create "rainbow" effect	
			*/
			if (_r >= 1 && _g < 1 && _b <= 0) _image.color = new Color(_r, Mathf.Clamp(_g + (Time.deltaTime * _speed), 0, 1), _b);
			else if (_r > 0 && _g >= 1 && _b <= 0) _image.color = new Color(Mathf.Clamp(_r - (Time.deltaTime * _speed), 0, 1), _g, _b);
			else if (_r <= 0 && _g >= 1 && _b < 1) _image.color = new Color(_r, _g, Mathf.Clamp(_b + (Time.deltaTime * _speed), 0, 1));
			else if (_r <= 0 && _g > 0 && _b >= 1) _image.color = new Color(_r, Mathf.Clamp(_g - (Time.deltaTime * _speed), 0 , 1), _b);
			else if (_r < 1 && _g <= 0 && _b >= 1) _image.color = new Color(Mathf.Clamp(_r + (Time.deltaTime * _speed), 0, 1), _g, _b);
			else _image.color = new Color(_r, _g, Mathf.Clamp(_b - (Time.deltaTime * _speed), 0, 1));
		}
		
		private void Update()
		{
			IncrementColor();
			PhotonManager.BackgroundImageColor = _image.color;
		}
	}
}
