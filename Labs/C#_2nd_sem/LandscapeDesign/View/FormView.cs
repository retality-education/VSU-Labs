using LandscapeDesign.Controller;
using LandscapeDesign.Enums;
using LandscapeDesign.Models;
using LandscapeDesign.ObserverPattern;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace LandscapeDesign.View
{
    internal partial class FormView : Form, ILandscapeView
    {
        public LandscapeController controller;

        struct AreaAndFlower
        {
            public int AreaId;
            public int FlowerId;
        }

        private ConcurrentDictionary<AreaAndFlower, PictureBox> FlowersPictures = new();
        private ConcurrentDictionary<int, PictureBox> ObjectsPictures = new();
        
        
        private readonly Size objectSize = new Size(96, 89);
        private readonly List<Point> areaCenters = new List<Point>
        {
            new Point(131, 268),
            new Point(418, 74),
            new Point(676, 268)
        };

        private readonly Size flowerSize = new Size(44, 45);
        private readonly List<Point> flowerOffsets = new List<Point>
        {
            new Point(-83, 21),
            new Point(29, -72),
            new Point(135, 21)
        };

        private readonly Size mayorAndDesignerSize = new Size(71, 107);
        private readonly Size carSize = new Size(133, 79);

        private readonly Point centerCity = new Point(429, 250);
        private readonly Point carCenterPosition = new Point(399, 268);
        private readonly Point carUnderCenterPosition = new Point(399, 422);
        private readonly Point mayorStartPosition = new Point(429, 500);
        private readonly Point mayorUnderCenterPosition = new Point(429, 434);

        private PictureBox mayorPicture = new PictureBox();
        private PictureBox designerPicture = new PictureBox();
        private PictureBox carPicture = new PictureBox();

        public FormView()
        {
            InitializeComponent();

            // Инициализация картинки мэра
            mayorPicture.Size = mayorAndDesignerSize;
            mayorPicture.Location = mayorStartPosition;
            mayorPicture.Image = Properties.Resources.Mayor;
            mayorPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            mayorPicture.Visible = false;
            Controls.Add(mayorPicture);

            // Инициализация картинки дизайнера
            designerPicture.Size = mayorAndDesignerSize;
            designerPicture.Location = designerShopPicture.Location;
            designerPicture.Image = Properties.Resources.Designer;
            designerPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            designerPicture.Visible = false;
            Controls.Add(designerPicture);

            // Инициализация картинки машины
            carPicture.Size = carSize;
            carPicture.Location = designerShopPicture.Location;
            carPicture.Image = Properties.Resources.DesignerCar;
            carPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            carPicture.Visible = false;
            Controls.Add(carPicture);

        }
        private Image GetObjectImage(ObjectType type)
        {
            return type switch
            {
                ObjectType.Fountain => Properties.Resources.Fountain,
                ObjectType.Gazebo => Properties.Resources.Gazebo,
                ObjectType.Statue => Properties.Resources.Statue,
                ObjectType.Pond => Properties.Resources.Pond,
                _ => Properties.Resources.NonObject
            };
        }

        private Image GetFlowerImage(FlowerType type)
        {
            return type switch
            {
                FlowerType.Rose => Properties.Resources.Rose,
                FlowerType.Tulip => Properties.Resources.Tulip,
                FlowerType.Daisy => Properties.Resources.Daisy,
                FlowerType.Sunflower => Properties.Resources.Sunflower,
                _ => Properties.Resources.NonFlower
            };
        }

        private Image GetWiltedFlowerImage(FlowerType type)
        {
            return type switch
            {
                FlowerType.Rose => Properties.Resources.RoseWilted,
                FlowerType.Tulip => Properties.Resources.TulipWilted,
                FlowerType.Daisy => Properties.Resources.DaisyWilted,
                FlowerType.Sunflower => Properties.Resources.SunflowerWilted,
                _ => Properties.Resources.NonFlower
            };
        }
        private void AnimateMovement(PictureBox picture, Point from, Point to, int duration, Action onCompleted = null)
        {
            var timer = new Timer { Interval = 16 };
            DateTime startTime = DateTime.Now;

            timer.Tick += (s, e) =>
            {
                double progress = (DateTime.Now - startTime).TotalMilliseconds / duration;
                if (progress >= 1)
                {
                    progress = 1;
                    timer.Stop();
                    onCompleted?.Invoke();
                }

                int x = (int)(from.X + (to.X - from.X) * progress);
                int y = (int)(from.Y + (to.Y - from.Y) * progress);

                picture.Location = new Point(x, y);
            };

            timer.Start();
        }

        public void MayorGoingAwayFromCity()
        {
            Console.WriteLine($"{nameof(MayorGoingAwayFromCity)}()");

            Invoke(new Action(() =>
            {
                mayorPicture.Visible = true;
                AnimateMovement(mayorPicture, designerShopPicture.Location, 
                    new Point(designerShopPicture.Location.X, designerShopPicture.Location.Y + 300), 500, () =>
                {
                    mayorPicture.Visible = false;
                });
            }));
        }

        public void MayorGoingToDesigner()
        {
            Console.WriteLine($"{nameof(MayorGoingToDesigner)}()");

            Invoke(new Action(() =>
            {
                mayorPicture.Visible = true;
                // Движение от центра города до магазина
                AnimateMovement(mayorPicture, centerCity, mayorUnderCenterPosition, 250, () =>
                {
                    // Затем сразу вниз
                    AnimateMovement(mayorPicture, mayorUnderCenterPosition,
                        designerShopPicture.Location, 250, () =>
                        {
                            mayorPicture.Visible = false;
                        });
                });
            }));
        }

        public void MayorGeneratedIdea()
        {
            Console.WriteLine($"{nameof(MayorGeneratedIdea)}()");

            Invoke(new Action(() =>
            {
                // Показываем идею над мэром
                ideaPicture.Location = new Point(
                    mayorPicture.Location.X + mayorPicture.Width / 2 - ideaPicture.Width / 2,
                    mayorPicture.Location.Y - ideaPicture.Height
                );
                ideaPicture.Visible = true;

                // Через 500ms скрываем
                Task.Delay(500).ContinueWith(t =>
                {
                    Invoke(new Action(() => ideaPicture.Visible = false));
                });
            }));
        }

        public void MayorComingToCity()
        {
            Console.WriteLine($"{nameof(MayorComingToCity)}()");

            Invoke(new Action(() =>
            {
                mayorPicture.Visible = true;
                AnimateMovement(mayorPicture, mayorStartPosition, centerCity, 500);
            }));
        }

        public void DesignerRidingToCity()
        {
            Console.WriteLine($"{nameof(DesignerRidingToCity)}()");

            Invoke(new Action(() =>
            {
                carPicture.Visible = true;
                designerPicture.Location = carCenterPosition;

                // Движение от магазина до нижней позиции
                AnimateMovement(carPicture, designerShopPicture.Location, carUnderCenterPosition, 250, () =>
                {
                    // Затем сразу до центральной позиции
                    AnimateMovement(carPicture, carUnderCenterPosition, carCenterPosition, 250);
                });
            }));
        }

        public void DesignerComeToArea(int areaId)
        {
            Console.WriteLine($"{nameof(DesignerComeToArea)}(areaId: {areaId})");

            Invoke(new Action(() =>
            {
                designerPicture.Visible = true;

                // Движение от машины к области
                AnimateMovement(designerPicture, designerPicture.Location, areaCenters[areaId], 500);
            }));
        }

        public void DesignerComeBackToShop()
        {
            Console.WriteLine($"{nameof(DesignerComeBackToShop)}()");

            Invoke(new Action(() =>
            {
                // Движение от текущей позиции к машине
                AnimateMovement(designerPicture, designerPicture.Location, carCenterPosition, 333, () =>
                {
                    designerPicture.Visible = false;

                    // Затем машина уезжает
                    AnimateMovement(carPicture, carCenterPosition, carUnderCenterPosition, 333, () =>
                    {
                        AnimateMovement(carPicture, carUnderCenterPosition, designerShopPicture.Location, 333, () =>
                        {
                            carPicture.Visible = false;
                        });
                    });
                });
            }));
        }

        public void FlowerChangeStateToWilted(int areaId, int flowerId, FlowerType flowerType)
        {
            Console.WriteLine($"{nameof(FlowerChangeStateToWilted)}(areaId: {areaId}, flowerId: {flowerId}, flowerType: {flowerType})");

            var key = new AreaAndFlower { AreaId = areaId, FlowerId = flowerId };
            if (FlowersPictures.TryGetValue(key, out var pictureBox))
            {
                Invoke(new Action(() => pictureBox.Image = GetWiltedFlowerImage(flowerType)));
            }
        }

        public void FlowerChanged(int areaId, int flowerId, FlowerType flowerType)
        {
            Console.WriteLine($"{nameof(FlowerChanged)}(areaId: {areaId}, flowerId: {flowerId}, flowerType: {flowerType})");

            var key = new AreaAndFlower { AreaId = areaId, FlowerId = flowerId };
            if (FlowersPictures.TryGetValue(key, out var pictureBox))
            {
                Invoke(new Action(() => pictureBox.Image = GetFlowerImage(flowerType)));
            }
        }

        public void MainObjectEndBuilding(int areaId, ObjectType objectType)
        {
            Console.WriteLine($"{nameof(MainObjectEndBuilding)}(areaId: {areaId}, objectType: {objectType})");

            if (ObjectsPictures.TryGetValue(areaId, out var pictureBox))
            {
                Invoke(new Action(() =>
                {
                    pictureBox.Image = GetObjectImage(objectType);
                    if (pictureBox.Image is Bitmap bmp)
                    {
                        ImageAnimator.StopAnimate(bmp, null);
                    }
                }));
            }
        }

        public void MainObjectStartBuilding(int areaId)
        {
            Console.WriteLine($"{nameof(MainObjectStartBuilding)}(areaId: {areaId})");

            if (ObjectsPictures.TryGetValue(areaId, out var pictureBox))
            {
                var buildingImage = Properties.Resources.BuildingObject;
                Invoke(new Action(() =>
                {
                    pictureBox.Image = buildingImage;
                }));
            }
        }

        private PictureBox CreatePictureBoxOfFlower(int areaId, int flowerId)
        {
            var picture = new PictureBox();
            picture.Size = flowerSize;
            picture.Location = new Point(
                areaCenters[areaId].X + flowerOffsets[flowerId].X,
                areaCenters[areaId].Y + flowerOffsets[flowerId].Y
            );
            picture.Image = GetFlowerImage(FlowerType.NonFlower);
            picture.SizeMode = PictureBoxSizeMode.StretchImage;

            Invoke(new Action(() => Controls.Add(picture)));
            return picture;
        }
        public void CreatePlaceForFlower(int areaId, int flowerId)
        {
            Console.WriteLine($"{nameof(CreatePlaceForFlower)}(areaId: {areaId}, flowerId: {flowerId})");

            FlowersPictures[new AreaAndFlower {AreaId = areaId, FlowerId = flowerId}] = 
                CreatePictureBoxOfFlower(areaId, flowerId);
        }

        private PictureBox CreatePictureBoxOfObject(int areaId)
        {

            var picture = new PictureBox();
            picture.Size = objectSize;
            picture.Location = new Point(
                areaCenters[areaId].X,
                areaCenters[areaId].Y
            );
            picture.Image = GetObjectImage(ObjectType.NonObject);
            picture.SizeMode = PictureBoxSizeMode.StretchImage;

            Invoke(new Action(() => Controls.Add(picture)));
            return picture;
        }
        public void CreatePlaceForObject(int areaId)
        {
            Console.WriteLine($"{nameof(CreatePlaceForObject)}(areaId: {areaId})");

            ObjectsPictures[areaId] =
                CreatePictureBoxOfObject(areaId);        }

        public void OnCityEvent(CityEventArgs e)
        {
            switch (e.EventType)
            {
                case EventType.MayorGoingAwayFromCity:
                    MayorGoingAwayFromCity();
                    break;

                case EventType.MayorGoingToDesigner:
                    MayorGoingToDesigner();
                    break;

                case EventType.MayorGeneratedIdea:
                    MayorGeneratedIdea();
                    break;

                case EventType.MayorComingToCity:
                    MayorComingToCity();
                    break;

                case EventType.DesignerRidingToCity:
                    DesignerRidingToCity();
                    break;

                case EventType.DesignerComeToArea:
                    DesignerComeToArea(e.AreaId);
                    break;

                case EventType.DesignerComeBackToShop:
                    DesignerComeBackToShop();
                    break;

                case EventType.FlowerChangeStateToWilted:
                    FlowerChangeStateToWilted(e.AreaId, e.FlowerId, e.FlowerType);
                    break;

                case EventType.FlowerChanged:
                    FlowerChanged(e.AreaId, e.FlowerId, e.FlowerType);
                    break;

                case EventType.MainObjectEndBuilding:
                    MainObjectEndBuilding(e.AreaId, e.ObjectType);
                    break;

                case EventType.MainObjectStartBuilding:
                    MainObjectStartBuilding(e.AreaId);
                    break;

                case EventType.CreatePlaceForFlower:
                    CreatePlaceForFlower(e.AreaId, e.FlowerId);
                    break;

                case EventType.CreatePlaceForObject:
                    CreatePlaceForObject(e.AreaId);
                    break;

                default:
                    Console.WriteLine($"Unknown event type: {e.EventType}");
                    break;
            }
        }

        private void FormView_Load(object sender, EventArgs e)
        {
            controller.Start();
        }
    }
}
