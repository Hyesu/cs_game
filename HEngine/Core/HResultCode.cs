namespace HEngine.Core
{
    public enum HResultCode
    {
        Success = 0,
        Error = 1,
        InvalidArgument,
        NotImplemented,
    
        // CheatSpotlight
        CheatSpotlightStart = 1001,
        CheatSpotlightNotFoundCommand,
        CheatSpotlightInvalidOption,
        CheatSpotlightEnd = 1100,
        
        // Dialog
        DialogStart = 1101,
        DialogNotFound,
        DialogEnd = 1200,
    
        // Episode
        EpisodeStart = 10000,
        EpisodeNotFoundPhase,
        EpisodeNotProgressing,
        EpisodeEnd = 10100,
    }   
}