using System.Drawing.Text;
using UnityEngine;

public class GoalStopper : MonoBehaviour
{
    private int _grade;
    private void OnTriggerEnter(Collider Other)
    {
        if (Other.TryGetComponent(out Runner runner))
        {
            runner.Finish(_grade++);
        }
    }
}