using UnityEngine;

[CreateAssetMenu(menuName = "SO/EnemyData")]
public class EnemyDataSO : ScriptableObject
{
    [Header("Movement")]
    public float movementSpeed;
    
    
    [Header("Combat")]
    public LayerMask whatIsPlayer;
    public float targetingRange;
    public float attackRange;
    public float attackCoolTime;
}
