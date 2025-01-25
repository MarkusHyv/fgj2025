namespace ProtoToolkit.Scripts.UI.State
{
    public class UIStateTransition
    {
        public UIStateDefinition Definition { get; set; }
        public object[] Parameters { get; set; }
        public readonly bool ResetHistory = false;
        public readonly bool WriteToHistory = true;
    }
}
