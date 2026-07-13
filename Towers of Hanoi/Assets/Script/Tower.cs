using System.Collections.Generic;
using UnityEngine;
public class Tower : MonoBehaviour
{
    public Stack<Ring> rings = new Stack<Ring>();
    public GameManager gm;
    public Transform[] transforms;
    private void OnMouseDown()
    {
        gm.SelectedTower(this);
    }
}