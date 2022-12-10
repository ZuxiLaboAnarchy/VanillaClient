namespace VanillaClient.Modules
{
    public abstract class VanillaModule
    {

        public virtual void UI() { }
        public virtual void Start() { }
        public virtual void Stop() { }
        public virtual void Update() { }
        public virtual void LateStart() { }
        public virtual void WaitForPlayer() { }
        public virtual void OnGUI() { }
  
    }
}

