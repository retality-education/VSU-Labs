using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandscapeDesign.Enums
{
    internal enum EventType
    {
        MayorGoingAwayFromCity,
        MayorGoingToDesigner,
        MayorGeneratedIdea,
        MayorComingToCity,

        DesignerRidingToCity,
        DesignerComeToArea,
        DesignerComeBackToShop,

        FlowerChangeStateToWilted,
        MainObjectEndBuilding,
        MainObjectStartBuilding,
        CreatePlaceForFlower,
        CreatePlaceForObject,
        FlowerChanged
    }
}
