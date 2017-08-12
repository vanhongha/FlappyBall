using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBall : MonoBehaviour {

	public Image ball;
	public Text priceText;
	public Button buyButton;
	public Toggle chooseToggle;

	private int id; // ID của ball, là số thứ tự trong storage.cs
	private int price; // Giá tiền của ball

	public void Init(int index, int price, ToggleGroup group)
	{
		this.transform.localScale = Vector3.one;
		this.transform.localPosition = Vector3.zero;

		this.id = index;
		this.price = price;
		this.chooseToggle.isOn = false;
		this.chooseToggle.interactable = false;

		this.ball.sprite = Storage.Instance.GetBallSprite(index);
		this.priceText.text = price.ToString();

		this.buyButton.onClick.AddListener(delegate { Buy(); });
		this.chooseToggle.onValueChanged.AddListener(delegate { Choose(); });

		if (UserProfile.Instance.IsBallAvailable(index))
		{
			// Nếu đã mua loại ball này thì ẩn button đi
			this.buyButton.gameObject.SetActive(false);

			// Hiện ra toggle để chọn ball
			this.chooseToggle.gameObject.SetActive(true);

			if (UserProfile.Instance.GetCurrentBall() == this.id)
			{
				Debug.Log(this.id);
				this.chooseToggle.isOn = true;
				this.chooseToggle.interactable = false;
			}
			else
			{
				this.chooseToggle.isOn = false;
				this.chooseToggle.interactable = true;
			}
		}
		else
		{
			// Nếu chưa mua thì có nút mua
			this.buyButton.gameObject.SetActive(true);

			// Không cho tương tác với toggle
			this.chooseToggle.gameObject.SetActive(true);
			this.chooseToggle.isOn = false;
			this.chooseToggle.interactable = false;
		}


		this.chooseToggle.group = group;
	}

	public void Buy()
	{
		// Nếu đủ tiền thì mua
		if (UserProfile.Instance.ReduceDiamond(this.price))
		{
			// Đưa thông tin vào UserProfile
			UserProfile.Instance.SetBallAvaiable(this.id);

			// Sau khi mua xong thì ẩn nút đi
			buyButton.gameObject.SetActive(false);

			// Hiện toggle ra
			this.chooseToggle.gameObject.SetActive(true);
			this.chooseToggle.interactable = true;
			this.chooseToggle.isOn = false;
		}
	}

	public void Choose()
	{
		if (chooseToggle.isOn)
		{
			chooseToggle.interactable = false;
			UserProfile.Instance.SetCurrentBall(this.id);
		}
		else
		{
			chooseToggle.interactable = true;
		}
	}
}
