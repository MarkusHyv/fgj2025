namespace ProtoToolkit.Scripts.UI.Common
{
    public interface IToggleable
    {
        public bool Enabled { get; }
        public void SetEnabled(bool value);
    }
}
