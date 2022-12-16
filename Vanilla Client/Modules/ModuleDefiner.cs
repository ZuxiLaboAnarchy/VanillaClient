namespace Vanilla.Modules
{
    public abstract class VanillaModule
    {

        internal virtual void UI() { }
        internal virtual void Start() { }
        internal virtual void Stop() { }
        internal virtual void Update() { }
        internal virtual void LateStart() { }
        internal virtual void WaitForPlayer() { }
        internal virtual void OnGUI() { }
        internal virtual void PlayerEvent() { }
        internal virtual void WorldLoad(int level) { }
        internal virtual void WorldUnload(int level) { }
        internal virtual void PlayerJoin(VRC.Player __User) { }
    }
}

