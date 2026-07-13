using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PhoneBook : MonoBehaviour
{



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TMP_InputField nameInputField,numberInputField,searchBar;
    public GameObject addContactPanel;
    public List<Contact> contactList;
    public List<ContactCard> contactCardList;
    public Transform PhoneBookParent;
    public GameObject contactCardPrefab;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showAddContactPanel()
    {
        addContactPanel.SetActive(true);
    }

    public void SaveContact()
    {
        string name = nameInputField.text;
        string number = numberInputField.text;
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(number))
        {
            Debug.Log("Please fill in all fields.");
            return;
        }
        Contact newContact = new Contact(name, number);
        var temp = Instantiate(contactCardPrefab, PhoneBookParent);
        temp.GetComponent<ContactCard>().setCardInfo(newContact);
        contactCardList.Add(temp.GetComponent<ContactCard>());
        contactList.Add(newContact);
        Debug.Log("Contact saved: " + name + ", " + number);
        addContactPanel.SetActive(false);
    }




}

[System.Serializable]
public class Contact
{
    public string name;
    public string number;
    public Contact(string name, string number)
    {
        this.name = name;
        this.number = number;
    }
}