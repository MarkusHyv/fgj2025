namespace ProtoToolkit.Scripts.UI.Common
{
    public interface ISelectable
    {
        public bool IsSelected { get; }
        public void SetSelected(bool value);
    }
}
