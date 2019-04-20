using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public RawImage rawimage;
	public Heart heart;


	private List<RawImage> displayHearts = new List<RawImage>();

	public MainUIHolder holder;


	[System.Serializable]
	public class Heart
	{
		public Texture fullheart;
		public Texture q3_heart;
		public Texture half_heart;
		public Texture q1_heart;
		public Texture empty_heart;
	}

	void Start(){
		DontDestroyOnLoad (this.gameObject);
	}

	// Use this for initialization
	void Update()
	{
		if (holder == null) {
			holder = FindObjectOfType<MainUIHolder> ();
		}
	}
		

	public void DisplayHearts(int health, int maxhealth){
		foreach (RawImage h in displayHearts) {
			if (h != null) {
				Destroy (h.gameObject);
			}
		}


		int offset = 0;
		int voffset = 0;
		for (int i = 0; i < health / 4; i++, offset++) {
			if (offset == 6) {
				offset = 0;
				voffset++;
			}
			RawImage rheart = Instantiate (rawimage) as RawImage;
			rheart.transform.SetParent(holder.canvas.transform);
			rheart.transform.localScale = new Vector2 (.5f,.5f);
			rheart.rectTransform.anchoredPosition = new Vector2 (25f + (offset * 50f),-25f - ( voffset * 50));
			rheart.texture = this.heart.fullheart;
			displayHearts.Add (rheart);
		}	


		if (health % 4 != 0) {
			RawImage pheart = Instantiate (rawimage) as RawImage;
			pheart.transform.SetParent(holder.canvas.transform);
			pheart.transform.localScale = new Vector2 (.5f,.5f);
			switch (health % 4) {
			case 1:pheart.texture = this.heart.q1_heart;
				break;
			case 2:pheart.texture = this.heart.half_heart;
				break;
			case 3:pheart.texture = this.heart.q3_heart;
				break;
			default:
				Debug.LogError ("Heart Error");
				break;
			}
			displayHearts.Add (pheart);
			if (offset >= 6) {
				offset = 0;
				voffset++;
			}
			pheart.rectTransform.anchoredPosition = new Vector2 (25f + (offset * 50f), -25f - (voffset * 50));
			offset++;
		}
		for (int i = 0; i < (maxhealth - health) / 4; i++, offset++) {
			if (offset >= 6) {
				offset = 0;
				voffset++;
			}
			RawImage rheart = Instantiate (rawimage) as RawImage;
			rheart.transform.SetParent(holder.canvas.transform);
			rheart.transform.localScale = new Vector2 (.5f,.5f);
			rheart.rectTransform.anchoredPosition = new Vector2 (25f + (offset * 50f),-25f - ( voffset * 50));
			rheart.texture = this.heart.empty_heart;
			displayHearts.Add (rheart);
		}	
	}
}
