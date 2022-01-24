using UnityEngine;

public class ElevatorSender : MonoBehaviour { 
    [SerializeField]
    private Transform target;
    
    private bool sent;
    
    public void Send() {
        if (sent) {
            return;
        }
        
        Elevator.SendAway(target.position);
        sent = true;
    }
}
