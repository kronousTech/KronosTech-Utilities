using UnityEditor;

namespace KendirStudios.CustomPackages.Utilities.EditorTools
{
    public readonly struct MessageTypeStringPair
    {
        public MessageType Type { get; }
        public string Message { get; }

        public MessageTypeStringPair(MessageType type, string message)
        {
            Type = type;
            Message = message;
        }
    }
}