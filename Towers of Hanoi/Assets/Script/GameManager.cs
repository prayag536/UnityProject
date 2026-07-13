using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    public Tower[] tower;
    public Tower  from, to,target;
    public Ring[] ringg;
    public bool isanimated ;
    public GameObject GameWinPanel,offpanel,Gameoverpanel;
    public int counter,move;
    public TMP_Text MoveText;
    private void Start()
    {
        offpanel.SetActive(true);
        MoveText.text = "Move: "+move;
        Time.timeScale = 1;
        for (int i = 0; i < ringg.Length; i++)
        {
            tower[0].rings.Push(ringg[i]);
        }
    }
    public void SelectedTower(Tower t)
    {
        if(move==0)
        {
            Gameoverpanel.SetActive(true);
            offpanel.SetActive(false);

        }

        if (isanimated)
        {
            return;
        }
        if (from == null && t.rings.Count > 0)
        {
            from = t;
            var rings = from.rings.Peek();
            rings.ringposition = rings.transform.position;
            rings.transform.DOMoveY(from.transform.position.y + 2, 0.1f);
        }
        else if (from != null && from != t)
        {
            to = t;
            if (to.rings.Count == 0)
            {
                var ring = from.rings.Pop();
                StartCoroutine(Animmation(ring));
                
            }
            else if (from.rings.Peek().size < to.rings.Peek().size)
            {
                var ring = from.rings.Pop();
                StartCoroutine(Animmation(ring));
                
            }
            else
            {
                var ring = from.rings.Peek();
                ring.transform.DOMoveY(ring.ringposition.y, 0.5f);
                StartCoroutine(Animmation(ring));
                from = null;
                to = null;
            }
        }
        //else
        //{
        //    var ring = from.rings.Peek();
        //    ring.transform.DOMoveY(ring.ringposition.y, 0.5f);
        //    from = null;
        //    to = null;
        //}
    }
    IEnumerator Animmation(Ring ring)
    {
        isanimated = true;
        ring.transform.DOMoveX(to.transform.position.x, 0.1f);
        yield return new WaitForSeconds(0.1f);

        ring.transform.DOMoveY(to.transforms[to.rings.Count].transform.position.y,0.1f);
        yield return new WaitForSeconds(0.1f);
        to.rings.Push(ring);
        move--;
        MoveText.text = "Move: " + move;
        if (target.rings.Count == counter)
        {
            GameWinPanel.SetActive(true);
            offpanel.SetActive(false);
        }
        from = null;
        to = null;
        isanimated = false;
    }
    public void NextButtton()
    {
        SceneManager.LoadScene(0);
    }
    public void HomeButton()
    {
        SceneManager.LoadScene(0);
    }

}