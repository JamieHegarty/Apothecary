namespace DefaultNamespace
{
    public class Ground : InteractableObject
    {
        public override void DefaultAction()
        {
            Walk();
        }

        public void Walk()
        {
            GlobalEventHandler.Instance.Walk(gameObject);
        }
    }
}