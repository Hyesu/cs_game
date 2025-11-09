namespace HEngine.Core;

public enum HResultCode
{
    Success = 0,
    Error = 1,
    InvalidArgument,
    
    // CheatSpotlight
    CheatSpotlightStart = 1001,
    CheatSpotlightNotFoundCommand,
    CheatSpotlightInvalidOption,
    CheatSpotlightEnd = 1100,
    
    // 10000~ application error codes
}