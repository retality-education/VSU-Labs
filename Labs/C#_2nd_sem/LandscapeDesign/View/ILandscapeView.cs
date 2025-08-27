using LandscapeDesign.Enums;
using LandscapeDesign.ObserverPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandscapeDesign.View
{
    internal interface ILandscapeView : IObserver
    {
        void MayorGoingAwayFromCity();
        void MayorGoingToDesigner();
        void MayorGeneratedIdea();
        void MayorComingToCity();

        void DesignerRidingToCity();
        void DesignerComeToArea(int areaId);
        void DesignerComeBackToShop();

        void FlowerChangeStateToWilted(int areaId, int flowerId, FlowerType flowerType);
        void FlowerChanged(int areaId, int flowerId, FlowerType flowerType);
        void MainObjectEndBuilding(int areaId, ObjectType objectType);
        void MainObjectStartBuilding(int areaId);
        void CreatePlaceForFlower(int areaId, int flowerId);
        void CreatePlaceForObject(int areaId);
    }
}
