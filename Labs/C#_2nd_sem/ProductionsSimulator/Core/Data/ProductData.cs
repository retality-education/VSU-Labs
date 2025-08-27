using Production.Core.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PT = Production.Core.Enums.ProductType;

namespace Production.Core.Data
{
    internal static class ProductData
    {
        public static IReadOnlyDictionary<PT, PT> ResultOfFrying { get; } = new Dictionary<PT, PT>
        {
            [PT.Potato] = PT.FriedPotato,
            [PT.Sausages] = PT.FriedSausages,
            [PT.Bacon] = PT.FriedBacon,
            [PT.Cutlets] = PT.FriedCutlets
        };

        public static IReadOnlyDictionary<PT, PT> ResultOfBoiling { get; } = new Dictionary<PT, PT>
        {
            [PT.Buckwheat] = PT.BoiledBuckwheat,
            [PT.Pasta] = PT.BoiledPasta,
            [PT.Rice] = PT.BoiledRice,
            [PT.Porridge] = PT.BoiledPorridge
        };
        public static IReadOnlyDictionary<PT, List<PT>> RecipeBook { get; } = new Dictionary<PT, List<PT>>
        {
            [PT.CookedPorridge] = new() 
            { 
                PT.BoiledPorridge 
            },
            [PT.CookedRiceWithCutlets] = new() 
            { 
                PT.BoiledRice, 
                PT.FriedCutlets 
            },
            [PT.CookedCutlets] = new() 
            { 
                PT.FriedCutlets 
            },
            [PT.CookedPastaWithBacon] = new() 
            { 
                PT.BoiledPasta, 
                PT.FriedBacon 
            },
            [PT.CookedFrenchFries] = new() 
            { 
                PT.FriedPotato 
            },
            [PT.CookedBuckWheatWithSausages] = new() 
            { 
                PT.FriedSausages, 
                PT.BoiledBuckwheat 
            }
        };
    }
}
