using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using HEngine.Core;

namespace HFin.Cheat
{
    public class CheatSpotlight
    {
        public IEnumerable<CheatAction> All => [];
   
        public HResultCode Add(CheatAction action, Func<ImmutableArray<string>, HResultCode> executor)
        {
            return HResultCode.Error;
        }

        public HResultCode Execute(string query)
        {
            return HResultCode.Error;
        }
    }   
}