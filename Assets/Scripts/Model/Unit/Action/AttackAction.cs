namespace Model.Unit.Command
{
    public class AttackAction : IUnitAction
    {
        public Unit Unit { get; }
        private IDamageable _damageable;
        
        
        public AttackAction(Unit unit, IDamageable damageable)
        {
            Unit = unit;
            _damageable = damageable;
        }
        public void Execute()
        {
            Unit.Attack(_damageable);
        }

        public void Undo()
        {
            throw new System.NotImplementedException();
        }
    }
}