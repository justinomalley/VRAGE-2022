using UnityEngine;

public class ElevatorSender : MonoBehaviour { 
    [SerializeField]
    private Transform target;
    
    private bool sent;
    
    public void Send() {
        if (sent) {
            return;
        }
        
        Elevator.GoAway(target.position);
        sent = true;
    }
}
