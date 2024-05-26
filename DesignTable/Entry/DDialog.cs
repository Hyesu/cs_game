using System.Collections.Generic;
using System.Linq;
using DesignTable.Core;

namespace DesignTable.Entry
{
    public enum DialogType
    {
        None,
        Conversation,
    }

    public class DDialogChoice
    {
        public readonly string Text;
        public readonly string Key;

        public DDialogChoice(IDParsedObject parsedObject)
        {
            Text = parsedObject.GetString("Text");
            Key = parsedObject.GetString("Key");
        }
    }

    public class DDialogSpeech
    {
        public readonly string Key;
        public readonly string Character;
        public readonly string Pivot;
        public readonly string Emotion;
        public readonly string Text;

        // 대사 분기
        public readonly string JumpKey;
        public readonly List<DDialogChoice> Choices;

        public DDialogSpeech(IDParsedObject parsedObject)
        {
            Key = parsedObject.GetString("Key") ?? string.Empty;
            Character = parsedObject.GetString("Character");
            Pivot = parsedObject.GetString("Pivot") ?? "Center";
            Emotion = parsedObject.GetString("Emotion") ?? "Idle";
            Text = parsedObject.GetString("Text") ?? string.Empty;

            JumpKey = parsedObject.GetString("JumpKey") ?? string.Empty;
            Choices = parsedObject.GetObjArray("Choices")
                .Select(x => new DDialogChoice(x))
                .ToList();
        }
    }

    public class DDialog : DEntry
    {
        public readonly DialogType Type;
        public readonly List<DDialogSpeech> Speeches;

        public DDialog(IDParsedObject parsedObject)
            : base(parsedObject)
        {
            Type = parsedObject.GetEnum<DialogType>("Type");
            Speeches = parsedObject.GetObjArray("Speeches")
                .Select(x => new DDialogSpeech(x))
                .ToList();
        }

        public DDialogSpeech FindSpeech(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }

            return Speeches.FirstOrDefault(x => key == x.Key);
        }
    }
}