

using AutoBase.Controller;
using AutoBase.Core.Enums;
using AutoBase.Core.Events;
using AutoBase.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoBase.View
    {
        internal partial class AutoBaseForm : Form, IAutoBaseView
        {
            public AutoBaseController autoBaseController;
            private Dictionary<int, PictureBox> carsIdToPicture = new();
            private PictureBox workerPictureBox;
            private readonly object pictureLock = new object();

            public AutoBaseForm()
            {
                InitializeComponent();
            }

            void start()
            {
                autoBaseController.Start();
            }

            private void LogMessage(string message, [CallerMemberName] string methodName = "")
            {
                Console.WriteLine($"[{methodName}] {message}");
            }

            #region Helper Methods
            private PictureBox CreateCar(CarType type = CarType.LightGuest)
            {
                var car = new PictureBox
                {
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Image = type == CarType.LightGuest ? Properties.Resources.LightGuestCarBroken :
                           type == CarType.LightWorker ? Properties.Resources.LightWorkerCar :
                           Properties.Resources.CargoWorkerCar,
                    Location = new Point(-100, -100), // Начальная позиция вне видимости
                    Size = new Size(100, 60),
                    Visible = false
                };

                this.Invoke((MethodInvoker)(() => Controls.Add(car)));
                return car;
            }

            private PictureBox CreateWorker()
            {
                var worker = new PictureBox
                {
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Image = Properties.Resources.Worker1,
                    Location = workerHousePictureBox.Location,
                    Size = new Size(60, 100),
                    Visible = false
                };

                this.Invoke((MethodInvoker)(() => Controls.Add(worker)));
                return worker;
            }

            private new async Task Move(PictureBox picture, Point target, int durationMs)
            {
                if (picture == null) return;

                var startPos = picture.Location;
                float stepX = (target.X - startPos.X) / (float)(durationMs / 10);
                float stepY = (target.Y - startPos.Y) / (float)(durationMs / 10);

                for (int i = 0; i < durationMs / 10; i++)
                {
                    await Task.Delay(10);
                    var newX = startPos.X + (int)(stepX * i);
                    var newY = startPos.Y + (int)(stepY * i);

                    this.Invoke((MethodInvoker)(() =>
                    {
                        picture.Location = new Point(newX, newY);
                    }));
                }

                this.Invoke((MethodInvoker)(() => picture.Location = target));
            }
            #endregion

            #region IAutoBaseView Implementation
            public async void OnGuestMoveAwayFromBase(int CarId)
            {
                LogMessage($"{CarId}");
                await Task.Run(() =>
                {
                    this.Invoke((MethodInvoker)(() =>
                    {
                        carsIdToPicture[CarId].Location = repairPictureBox.Location with { Y = repairPictureBox.Location.Y + 50 };
                        carsIdToPicture[CarId].Visible = true;
                        carsIdToPicture[CarId].Image = Properties.Resources.LightGuestCar;
                    }));
                    
                    Move(carsIdToPicture[CarId], new Point(this.Width + 100, repairPictureBox.Location.Y), 400).Wait();

                    this.Invoke((MethodInvoker)(() =>
                    {
                        if (carsIdToPicture.TryGetValue(CarId, out var car))
                        {
                            Controls.Remove(car);
                            car.Dispose();
                            carsIdToPicture.Remove(CarId);
                        }
                    }));
                });
            }

            public async void OnGuestMoveToChief(int CarId)
            {
                LogMessage($"{CarId}");

                await Task.Run(() =>
                {
                    this.Invoke((MethodInvoker)(() =>
                    {
                        var car = CreateCar();
                        carsIdToPicture[CarId] = car;
                        car.Visible = true;
                        car.Location = new Point(chiefPictureBox.Location.X - 100, this.Height + 100);
                    }));

                    var target = new Point(chiefPictureBox.Location.X - 100, chiefPictureBox.Location.Y);
                    Move(carsIdToPicture[CarId], target, 500).Wait();
                });
            }

            public async void OnGuestMoveToRepair(int CarId)
            {
                LogMessage($"{CarId}");

                await Task.Run(() =>
                {
                    var target = repairPictureBox.Location;
                    Move(carsIdToPicture[CarId], target, 500).Wait();

                    this.Invoke((MethodInvoker)(() =>
                    {
                        carsIdToPicture[CarId].Visible = false;
                    }));
                });
            }

            public async void OnReceiveOfflineOrder()
            {
                LogMessage("");
                await Task.Run(async () =>
                {
                    this.Invoke((MethodInvoker)(() =>
                    {
                        dialogChief.Image = Properties.Resources.dialogNeedWorkerToRepair;
                    }));

                    await Task.Delay(1000);

                    this.Invoke((MethodInvoker)(() =>
                    {
                        dialogChief.Image = null;
                    }));
                });
            }

            public async void OnReceiveOnlineOrder(CarDto car)
            {
                LogMessage("");
                await Task.Run(async () =>
                {
                    this.Invoke((MethodInvoker)(() =>
                    {
                        dialogDispatcher.Image = car.Type == CarType.LightWorker ?
                            Properties.Resources.dialogNeedLightCar :
                            Properties.Resources.dialogNeedCargoCar;
                    }));

                    await Task.Delay(1000);

                    this.Invoke((MethodInvoker)(() =>
                    {
                        dialogDispatcher.Image = null;
                    }));
                });
            }

            public async void OnWorkerCarCameFromOrderToGarage(int CarId)
            {
                LogMessage($"{CarId}");

                await Task.Run(() =>
                {
                    this.Invoke((MethodInvoker)(() =>
                    {
                        carsIdToPicture[CarId].Location = new Point(garagePictureBox.Location.X, this.Height + 50);
                        carsIdToPicture[CarId].Visible = true;
                    }));

                    Move(carsIdToPicture[CarId], garagePictureBox.Location, 500).Wait();

                    this.Invoke((MethodInvoker)(() =>
                    {
                        Controls.Remove(carsIdToPicture[CarId]);
                        carsIdToPicture[CarId].Dispose();
                        carsIdToPicture.Remove(CarId);
                    }));
                });
            }

            public async void OnWorkerCarCameFromOrderToRepair(CarDto car)
            {
                LogMessage($"{car.Id}");

                await Task.Run(() =>
                {
                    this.Invoke((MethodInvoker)(() =>
                    {
                        carsIdToPicture[car.Id].Location = new Point(repairPictureBox.Location.X, this.Height + 50);
                        carsIdToPicture[car.Id].Visible = true;
                        carsIdToPicture[car.Id].Image = (car.Type == CarType.HeavyWorker) ?
                            Properties.Resources.CargoWorkerCarBroken :  Properties.Resources.LightWorkerCarBroken;
                    }));

                    Move(carsIdToPicture[car.Id], repairPictureBox.Location, 500).Wait();

                    this.Invoke((MethodInvoker)(() =>
                    {
                        carsIdToPicture[car.Id].Visible = false;
                    }));
                });
            }

            public async void OnWorkerCarMoveFromRepairToGarage(CarDto car)
            {
                LogMessage($"{car.Id}");

                await Task.Run(() =>
                {
                    this.Invoke((MethodInvoker)(() =>
                    {
                        carsIdToPicture[car.Id].Location = repairPictureBox.Location;
                        carsIdToPicture[car.Id].Visible = true;
                        carsIdToPicture[car.Id].Image = (car.Type == CarType.HeavyWorker ?
                            Properties.Resources.CargoWorkerCar : Properties.Resources.LightWorkerCar);
                    }));

                    Move(carsIdToPicture[car.Id], garagePictureBox.Location, 500).Wait();

                    this.Invoke((MethodInvoker)(() =>
                    {
                        Controls.Remove(carsIdToPicture[car.Id]);
                        carsIdToPicture[car.Id].Dispose();
                        carsIdToPicture.Remove(car.Id);
                    }));
                });
            }

            public async void OnWorkerCarMoveToOrder(int CarId, CarDto car)
            {
                LogMessage($"{CarId}");

                await Task.Run(() =>
                {
                    CarType type = car.Type;

                    this.Invoke((MethodInvoker)(() =>
                    {
                        var car = CreateCar(type);
                        carsIdToPicture[CarId] = car;
                        car.Location = garagePictureBox.Location;
                        car.Visible = true;
                    }));

                    var target = new Point(garagePictureBox.Location.X, this.Height + 50);
                    Move(carsIdToPicture[CarId], target, 500).Wait();

                    this.Invoke((MethodInvoker)(() =>
                    {
                        carsIdToPicture[car.Id].Visible = false;
                    }));
                });
            }

            public async void OnWorkerMoveToGarage()
            {
                LogMessage("");

                await Task.Run(() =>
                {
                    this.Invoke((MethodInvoker)(() =>
                    {
                        workerPictureBox = CreateWorker();
                        workerPictureBox.Visible = true;
                     }));

                    // Движение вверх
                    var midPoint1 = new Point(workerPictureBox.Location.X, garagePictureBox.Location.Y - 150);
                    Move(workerPictureBox, midPoint1, 200).Wait();

                    // Движение вправо
                    var midPoint2 = new Point(garagePictureBox.Location.X, midPoint1.Y);
                    Move(workerPictureBox, midPoint2, 300).Wait();
                    
                    // Движение вниз
                    var target = new Point(garagePictureBox.Location.X, garagePictureBox.Location.Y);
                    Move(workerPictureBox, target, 200).Wait();

                    this.Invoke((MethodInvoker)(() =>
                    {
                        workerPictureBox.Visible = false;
                        Controls.Remove(workerPictureBox);
                        workerPictureBox.Dispose();
                    }));
                });
            }

            public async void OnWorkerMoveToRepair()
            {
                LogMessage("");

                await Task.Run(() =>
                {
                    this.Invoke((MethodInvoker)(() =>
                    {
                        workerPictureBox = CreateWorker();
                        workerPictureBox.Visible = true;
                    }));

                    // Движение вверх
                    var midPoint1 = new Point(workerPictureBox.Location.X, repairPictureBox.Location.Y - 150);
                    Move(workerPictureBox, midPoint1, 200).Wait();

                    // Движение вправо
                    var midPoint2 = new Point(repairPictureBox.Location.X, midPoint1.Y);
                    Move(workerPictureBox, midPoint2, 300).Wait();

                    // Движение вниз
                    var target = new Point(repairPictureBox.Location.X, repairPictureBox.Location.Y);
                    Move(workerPictureBox, target, 200).Wait();

                    this.Invoke((MethodInvoker)(() =>
                    {
                        workerPictureBox.Visible = false;
                        Controls.Remove(workerPictureBox);
                        workerPictureBox.Dispose();
                    }));
                });
            }
        #endregion

        public void OnAutobaseEvent(ModelEventArgs args)
        {
            switch (args.EventType)
            {
                case EventType.GuestMoveToChief:
                    OnGuestMoveToChief(CarId: args.CarInfo!.Id);
                    break;
                case EventType.ReceiveOfflineOrder:
                    OnReceiveOfflineOrder();
                    break;
                case EventType.GuestMoveToRepair:
                    OnGuestMoveToRepair(CarId: args.CarInfo!.Id);
                    break;
                case EventType.WorkerMoveToRepair:
                    OnWorkerMoveToRepair();
                    break;
                case EventType.GuestMoveAwayFromBase:
                    OnGuestMoveAwayFromBase(CarId: args.CarInfo!.Id);
                    break;
                case EventType.ReceiveOnlineOrder:
                    OnReceiveOnlineOrder(args.CarInfo!);
                    break;
                case EventType.WorkerMoveToGarage:
                    OnWorkerMoveToGarage();
                    break;
                case EventType.WorkerCarMoveToOrder:
                    OnWorkerCarMoveToOrder(CarId: args.CarInfo!.Id, car: args.CarInfo);
                    break;
                case EventType.WorkerCarCameFromOrderToGarage:
                    OnWorkerCarCameFromOrderToGarage(CarId: args.CarInfo!.Id);
                    break;
                case EventType.WorkerCarCameFromOrderToRepair:
                    OnWorkerCarCameFromOrderToRepair(car: args.CarInfo!);
                    break;
                case EventType.WorkerCarMoveFromRepairToGarage:
                    OnWorkerCarMoveFromRepairToGarage(car: args.CarInfo!);
                    break;
                default:
                    break;
            }
        }

            private void AutoBaseForm_Load(object sender, EventArgs e)
            {
                start();
            }
        }
    }
