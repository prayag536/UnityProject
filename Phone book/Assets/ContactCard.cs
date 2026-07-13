using TMPro;
using UnityEngine;

public class ContactCard : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public TextMeshProUGUI nameText,numberText;
    public Contact contact;
    public void setCardInfo(Contact c)
    {
        contact = c;
        nameText.text = c.name;
        numberText.text = c.number;
    }
}
