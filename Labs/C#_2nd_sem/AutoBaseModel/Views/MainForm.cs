using AutoBaseModel.Controllers;
using AutoBaseModel.Core.Enums;
using AutoBaseModel.Core.ObserverPattern;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace AutoBaseModel.Views
{
    internal partial class MainForm : Form, IAutoBase
    {
        internal Controller controller;
        private const int AnimationDurationMs = 900;
        public MainForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        private void SafeCreateTempPictureBox(Image image, Point startPos, Point endPos, Size size)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => SafeCreateTempPictureBox(image, startPos, endPos, size)));
                return;
            }

            var tempPictureBox = new PictureBox
            {
                Image = image,
                Size = size,
                Location = startPos,
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent
            };

            this.Controls.Add(tempPictureBox);
            tempPictureBox.BringToFront();

            var startTime = DateTime.Now;
            var totalDistance = Math.Sqrt(Math.Pow(endPos.X - startPos.X, 2) + Math.Pow(endPos.Y - startPos.Y, 2));

            var timer = new Timer { Interval = 16 }; // ~60 FPS
            timer.Tick += (s, e) =>
            {
                var elapsed = (DateTime.Now - startTime).TotalMilliseconds;
                var progress = Math.Min(elapsed / AnimationDurationMs, 1.0);

                var newX = startPos.X + (int)((endPos.X - startPos.X) * progress);
                var newY = startPos.Y + (int)((endPos.Y - startPos.Y) * progress);

                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => tempPictureBox.Location = new Point(newX, newY)));
                }
                else
                {
                    tempPictureBox.Location = new Point(newX, newY);
                }

                if (progress >= 1.0)
                {
                    timer.Stop();
                    timer.Dispose();
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action(() =>
                        {
                            this.Controls.Remove(tempPictureBox);
                            tempPictureBox.Dispose();
                        }));
                    }
                    else
                    {
                        this.Controls.Remove(tempPictureBox);
                        tempPictureBox.Dispose();
                    }
                }
            };
            timer.Start();
        }

        // === Клиентские методы ===
        public void ClientComeToAutoBase()
        {
            var clientImage = Properties.Resources.Client;
            SafeCreateTempPictureBox(
                clientImage,
                new Point(343, 708),
                new Point(343, 527),
                new Size(72, 116)
            );
        }

        public void ClientGoToGarage()
        {
            var clientImage = Properties.Resources.Client;
            SafeCreateTempPictureBox(
                clientImage,
                new Point(343, 527),
                new Point(148, 277),
                new Size(72, 116)
            );
        }

        public void ClientRentCar()
        {
            var carImage = Properties.Resources.RentCar;
            SafeCreateTempPictureBox(
                carImage,
                new Point(23, 327),
                new Point(-153, 327),
                new Size(159, 71)
            );
        }

        public void ClientBackCar()
        {
            var carImage = Properties.Resources.RentCar;
            SafeCreateTempPictureBox(
                carImage,
                new Point(-153, 277),
                new Point(148, 277),
                new Size(159, 71)
            );
        }

        public void ClientLeaveFromAutoBase()
        {
            var clientImage = Properties.Resources.Client;
            SafeCreateTempPictureBox(
                clientImage,
                new Point(148, 277),
                new Point(148, 708),
                new Size(72, 116)
            );
        }

        // === Лёгкий случай (машина уезжает и возвращается) ===
        public void LightCarGoToOrder()
        {
            var carImage = Properties.Resources.LightWorkerCar;
            SafeCreateTempPictureBox(
                carImage,
                new Point(148, 277),
                new Point(148, -70),
                new Size(159, 71)
            );
        }

        public void WorkerComeBackFromLightOrder()
        {
            var carImage = Properties.Resources.LightWorkerCar;
            SafeCreateTempPictureBox(
                carImage,
                new Point(181, -70),
                new Point(181, 258),
                new Size(159, 71)
            );
        }

        // === Тяжёлый случай (буксировка) ===
        public void TowCarGoToOrder()
        {
            var towTruckImage = Properties.Resources.TowWorkerCarWithoutCar; 
            SafeCreateTempPictureBox(
                towTruckImage,
                new Point(148, 229),
                new Point(148, -70),
                new Size(159, 71)
            );
        }

        public void WorkerComeBackFromTowOrder()
        {
            var towTruckWithCarImage = Properties.Resources.TowWorkerCarWithCar;
            SafeCreateTempPictureBox(
                towTruckWithCarImage,
                new Point(675, -66),
                new Point(675, 268),
                new Size(159, 71)
            );
        }

        public void TowCarComeBackToGarage()
        {
            var towTruckImage = Properties.Resources.TowWorkerCarWithoutCar;
            SafeCreateTempPictureBox(
                towTruckImage,
                new Point(675, 268),
                new Point(148, 268),
                new Size(159, 71)
            );
        }

        public void ClientLeaveWithRepairedCar()
        {
            var repairedCarImage = Properties.Resources.TowCaseClientCar; 
            SafeCreateTempPictureBox(
                repairedCarImage,
                new Point(725, 277),
                new Point(1100, 277),
                new Size(159, 71)
            );
        }

        // === Рабочий ===
        public void WorkerGoToGarage()
        {
            var workerImage = Properties.Resources.Employee1;
            SafeCreateTempPictureBox(
                workerImage,
                new Point(435, 148),
                new Point(187, 297),
                new Size(60, 82)
            );
        }

        public void WorkerGoBackToHouseFromGarage()
        {
            var workerImage = Properties.Resources.Employee1;
            SafeCreateTempPictureBox(
                workerImage,
                new Point(187, 297),
                new Point(435, 148),
                new Size(60, 82)
            );
        }

        public void WorkerGoToRepairShop()
        {
            var workerImage = Properties.Resources.Employee1;
            SafeCreateTempPictureBox(
                workerImage,
                new Point(435, 148),
                new Point(715, 289),
                new Size(60, 82)
            );
        }

        public void WorkerGoBackToHouseFromRepair()
        {
            var workerImage = Properties.Resources.Employee1;
            SafeCreateTempPictureBox(
                workerImage,
                new Point(715, 289),
                new Point(435, 148),
                new Size(60, 82)
            );
        }

        public void BossScreaming()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(BossScreaming));
                return;
            }

            dialogBoss.Visible = true;

            var timer = new Timer { Interval = 500 };
            timer.Tick += (s, e) =>
            {
                timer.Stop();
                timer.Dispose();
                dialogBoss.Visible = false;
            };
            timer.Start();
        }
        public void CarClientComeToAutoBase()
        {
           var clientCarImage = Properties.Resources.BrokenClientCar;
            SafeCreateTempPictureBox(
                clientCarImage,
                new Point(522, 715),
                new Point(522, 513),
                new Size(159, 71)
            );
        }
        public void CarClientGoToRepair()
        {
            var clientCarImage = Properties.Resources.BrokenClientCar;
            SafeCreateTempPictureBox(
                clientCarImage,
                new Point(522, 513),
                new Point(675, 314),
                new Size(159, 71)
            );
        }
        public void ClientLeaveFromAutoBaseSimple()
        {
            var clientCarImage = Properties.Resources.ClientCar;
            SafeCreateTempPictureBox(
                clientCarImage,
                new Point(675, 314),
                new Point(675, 715),
                new Size(159, 71)
            );
        }
        public void MoneyChanged(decimal Money) {
            Console.WriteLine($"{nameof(MoneyChanged)}: {Money}");
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => MoneyChanged(Money)));
                return;
            }

            money.Text = "Заработок: " + Money.ToString("C");
        }
        
        public void Update(EventData eventData)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => Update(eventData)));
                return;
            }
            switch (eventData.EventType)
            {
                case EventType.MoneyChanged:
                    MoneyChanged(eventData.Money);
                    break;
                case EventType.BossScreaming:
                    BossScreaming();
                    break;

                case EventType.WorkerGoToGarage:
                    WorkerGoToGarage();
                    break;

                case EventType.LightCarGoToOrder:
                    LightCarGoToOrder();
                    break;
                case EventType.TowCarGoToOrder:
                    TowCarGoToOrder();
                    break;
                case EventType.WorkerComeBackFromLightOrder:
                    WorkerComeBackFromLightOrder();
                    break;
                case EventType.WorkerComeBackFromTowOrder:
                    WorkerComeBackFromTowOrder();
                    break;
                case EventType.TowCarComeBackToGarage:
                    TowCarComeBackToGarage();
                    break;
                case EventType.WorkerGoToRepairShop:
                    WorkerGoToRepairShop();
                    break;
                case EventType.WorkerGoBackToHouseFromGarage:
                    WorkerGoBackToHouseFromGarage();
                    break;
                case EventType.WorkerGoBackToHouseFromRepair:
                    WorkerGoBackToHouseFromRepair();
                    break;

                case EventType.ClientComeToAutoBase:
                    ClientComeToAutoBase();
                    break;
                case EventType.CarClientComeToAutoBase:
                    CarClientComeToAutoBase();
                    break;
                case EventType.CarClientGoToRepair:
                    CarClientGoToRepair();
                    break;
                case EventType.ClientGoToGarage:
                    ClientGoToGarage();
                    break;

                case EventType.ClientRentCar:
                    ClientRentCar();
                    break;
                case EventType.ClientBackCar:
                    ClientBackCar();
                    break;
                case EventType.ClientLeaveWithRepairedCar:
                    ClientLeaveWithRepairedCar();
                    break;
                case EventType.ClientLeaveFromAutoBase:
                    ClientLeaveFromAutoBase();
                    break;
                case EventType.ClientLeaveFromAutoBaseSimple:
                    ClientLeaveFromAutoBaseSimple();
                    break;

                default:
                    break;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            controller.Start();
        }
    }
}