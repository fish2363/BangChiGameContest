using UnityEngine;

[CreateAssetMenu(menuName = "SO/EnemyData")]
public class EnemyDataSO : ScriptableObject
{
    public LayerMask whatIsPlayer;
    public float targetingRange;
    public float attackRange;
    public float movementSpeed;
}
