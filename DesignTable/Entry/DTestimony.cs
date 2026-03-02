using DesignTable.Core;

namespace DesignTable.Entry
{
    public class DTestimony : DEntry
    {
        public readonly string EvidenceSid;
        public readonly string TargetActorSid;
        public readonly string DialogSid;
        
        public DTestimony(IdParsedObject parsedObject)
            : base(parsedObject)
        {
            EvidenceSid = parsedObject.GetString("Evidence");
            TargetActorSid = parsedObject.GetString("TargetActor");
            DialogSid = parsedObject.GetString("Dialog");
        }
    }
}