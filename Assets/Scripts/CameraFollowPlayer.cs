using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
		public Transform objectToFollow;
		public Vector3 offset = new Vector3(0, 1, -5);

		void Awake()
		{
			this.FindCurrentObjectToFollow();
		}

		void Update()
		{
			if (objectToFollow) {
				this.transform.position = objectToFollow.position + offset;
			} else {
				Debug.LogError("Current Player not Found !");
			}
		}

		private void FindCurrentObjectToFollow()
		{
			GameObject obj = GameObject.FindWithTag("Player") ?
				GameObject.FindWithTag("Player")
			:
				GameObject.Find("CurrentPlayer");
			objectToFollow = obj ? obj.transform : null;
		}
}
