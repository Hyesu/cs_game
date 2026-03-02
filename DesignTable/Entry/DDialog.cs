using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using DesignTable.Core;

namespace DesignTable.Entry
{
    public enum DialogType
    {
        None,
        Conversation,
        Investigation,
    }
    
    public class DDialog : DEntry
    {
        public readonly DialogType Type;
        public readonly string BackgroundImage;
        public readonly List<DDialogSpeech> Speeches;
        public readonly string HandlerSid;

        public DDialog(IdParsedObject parsedObject)
            : base(parsedObject)
        {
            Type = parsedObject.GetEnum<DialogType>("Type");
            BackgroundImage = parsedObject.GetString("BackgroundImage");
            Speeches = parsedObject.GetObjArray("Speeches")
                .Select(x => new DDialogSpeech(x))
                .ToList();
            HandlerSid = parsedObject.GetString("HandlerSid");
        }

        public DDialogSpeech FindSpeech(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }

            return Speeches.FirstOrDefault(x => key == x.Key);
        }

        public int FindSpeechIndex(string key)
        {
            var dSpeech = FindSpeech(key);
            if (dSpeech is null)
            {
                return -1;
            }
            
            return Speeches.IndexOf(dSpeech);
        }
    }
    
    public class DDialogSpeech
    {
        public readonly string Key;
        public readonly string Character;
        public readonly bool CharacterHide;
        public readonly string Text;
        
        // 이벤트 기능
        public readonly ImmutableArray<DDialogEvent> PreEvents;
        public readonly ImmutableArray<DDialogEvent> PostEvents;

        // 대사 분기
        public readonly string JumpKey;
        public readonly List<DDialogChoice> Choices;

        public DDialogSpeech(IdParsedObject parsedObject)
        {
            Key = parsedObject.GetString("Key") ?? string.Empty;
            Character = parsedObject.GetString("Character");
            CharacterHide = parsedObject.GetBool("CharacterHide");
            Text = parsedObject.GetString("Text") ?? string.Empty;

            PreEvents = parsedObject.GetObjArray("PreEvents")
                .Select(x => new DDialogEvent(x))
                .ToImmutableArray();
            PostEvents = parsedObject.GetObjArray("PostEvents")
                .Select(x => new DDialogEvent(x))
                .ToImmutableArray();

            JumpKey = parsedObject.GetString("JumpKey") ?? string.Empty;
            Choices = parsedObject.GetObjArray("Choices")
                .Select(x => new DDialogChoice(x))
                .ToList();
        }
    }
    
    public class DDialogChoice
    {
        public readonly string Text;
        public readonly string Key;

        public DDialogChoice(IdParsedObject parsedObject)
        {
            Text = parsedObject.GetString("Text");
            Key = parsedObject.GetString("Key");
        }
    }

    public class DDialogEvent
    {
        public readonly string EventType;
        public readonly ImmutableArray<string> EventArg;

        public DDialogEvent(IdParsedObject parsedObject)
        {
            EventType = parsedObject.GetString("EventType");
            EventArg = parsedObject.GetStrArray("EventArg").ToImmutableArray();
        }
    }
}