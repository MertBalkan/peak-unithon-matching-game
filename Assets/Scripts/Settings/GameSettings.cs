using UnityEngine;

namespace Settings
{
	public class GameSettings : MonoBehaviour
	{
		private void Awake ()
		{
			Application.targetFrameRate = 60;
		}
	}
}
