using Production.Controller;
using Production.Core.Enums;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Production.View
{
    internal partial class MainForm : Form, IProductionView
    {
        private readonly ConcurrentDictionary<int, PictureBox> _modulePictureBoxes = new();
        private readonly ConcurrentDictionary<int, PictureBox> _conveyorPictureBoxes = new();
        private readonly Dictionary<ProductType, int> _deliveredDishes = new Dictionary<ProductType, int>();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ProductionController Controller { get; set; }
        public MainForm()
        {
            InitializeComponent();
        }
        private void AnimateMovement(PictureBox picture, Point from, Point to, int duration, Action onCompleted = null)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => AnimateMovement(picture, from, to, duration, onCompleted)));
                return;
            }

            var timer = new System.Windows.Forms.Timer { Interval = 16 };
            DateTime startTime = DateTime.Now;

            timer.Tick += (s, e) =>
            {
                if (InvokeRequired)
                {
                    Invoke(new Action(() => { timer.Stop(); timer.Dispose(); }));
                    return;
                }

                double progress = (DateTime.Now - startTime).TotalMilliseconds / duration;
                if (progress >= 1)
                {
                    progress = 1;
                    timer.Stop();
                    timer.Dispose();
                    onCompleted?.Invoke();
                }

                int x = (int)(from.X + (to.X - from.X) * progress);
                int y = (int)(from.Y + (to.Y - from.Y) * progress);

                picture.Location = new Point(x, y);
            };

            timer.Start();
        }
        public void ConveyorMovement(int conveyorId, int moduleId, ProductType productType)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => ConveyorMovement(conveyorId, moduleId, productType)));
                return;
            }

            Console.WriteLine($"[Конвейер {conveyorId}] Перемещение {productType}");

            if (!_conveyorPictureBoxes.TryGetValue(conveyorId, out var conveyor))
                return;

            // Создаем PictureBox для продукта
            var product = new PictureBox
            {
                Size = new Size(80, 80),
                BackColor = Color.Transparent,
                Image = GetProductImage(productType),
                SizeMode = PictureBoxSizeMode.Zoom,
                Visible = true
            };

            Controls.Add(product);
            product.BringToFront();

            // Начальная позиция - начало конвейера (левая сторона)
            Point startPoint = new Point(conveyor.Left, conveyor.Top + conveyor.Height / 2 - product.Height / 2);
            // Конечная позиция - конец конвейера (правая сторона)
            Point endPoint = new Point(conveyor.Right - product.Width, conveyor.Top + conveyor.Height / 2 - product.Height / 2);

            product.Location = startPoint;

            // Анимация перемещения (1000 мс = 1 секунда)
            AnimateMovement(product, startPoint, endPoint, 1000, () =>
            {
                // После завершения анимации удаляем продукт
                if (InvokeRequired)
                {
                    Invoke(new Action(() =>
                    {
                        Controls.Remove(product);
                        product.Dispose();
                    }));
                }
                else
                {
                    Controls.Remove(product);
                    product.Dispose();
                }
            });
        }
        private Image GetProductImage(ProductType dishType)
        {
            string resourceName = dishType.ToString();
            var resourceProperty = typeof(Properties.Resources)
                .GetProperty(resourceName,
                            System.Reflection.BindingFlags.Static |
                            System.Reflection.BindingFlags.Public |
                            System.Reflection.BindingFlags.NonPublic);

            return (Image)resourceProperty?.GetValue(null, null)!;
        }
        public void NewOrder(ProductType dishType)
        {
            Console.WriteLine($"[НАЧАЛЬНИК] Новый заказ: {dishType}");
            if (InvokeRequired)
            {
                this.Invoke(() =>
                {
                    // Показываем элементы интерфейса
                    managerLabel.Text = "Я хочу:";
                    managerLabel.Visible = true;

                    // Устанавливаем соответствующую картинку из ресурсов
                    // Получаем имя ресурса, соответствующее значению enum
                    string resourceName = dishType.ToString();

                    // Получаем свойство из ресурсов по имени
                    var resourceProperty = typeof(Properties.Resources)
                        .GetProperty(resourceName,
                                    System.Reflection.BindingFlags.Static |
                                    System.Reflection.BindingFlags.Public |
                                    System.Reflection.BindingFlags.NonPublic);
                    managerWishPicture.Image = (Image)resourceProperty!.GetValue(null, null)!;
                    managerWishPicture.Visible = true;
                });
            }
            // Скрываем элементы через 1.5 секунды
            Task.Delay(1500).ContinueWith(t =>
            {
                if (managerLabel.InvokeRequired)
                    managerLabel.Invoke(new Action(() => managerLabel.Visible = false));
                else
                    managerLabel.Visible = false;

                if (managerWishPicture.InvokeRequired)
                    managerWishPicture.Invoke(new Action(() => managerWishPicture.Visible = false));
                else
                    managerWishPicture.Visible = false;
            });
        }

        public void WorkerAction(string action)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => WorkerAction(action)));
                return;
            }

            Console.WriteLine($"[РАБОЧИЙ] {action}");

            // Запоминаем исходную позицию
            Point originalPosition = workerPicture.Location;

            // Целевая позиция - вверх на 50 пикселей
            Point targetPosition = new Point(originalPosition.X, originalPosition.Y + 250);

            // Анимация вверх (1 секунда)
            AnimateMovement(workerPicture, originalPosition, targetPosition, 1000, () =>
            {
                // Анимация возврата (1 секунда)
                AnimateMovement(workerPicture, targetPosition, originalPosition, 1000);
            });
        }

        public void DeliveryAction(ProductType productType)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => DeliveryAction(productType)));
                return;
            }

            Console.WriteLine($"[ДОСТАВКА] {productType}");

            // Обновляем счётчик блюд
            if (!_deliveredDishes.ContainsKey(productType))
            {
                _deliveredDishes[productType] = 1;
            }
            else
            {
                _deliveredDishes[productType]++;
            }

            Point originalPosition = deliveryPicture.Location;
            Point targetPosition = new Point(originalPosition.X, originalPosition.Y - 250);

            // Анимация вниз (1 секунда)
            AnimateMovement(deliveryPicture, originalPosition, targetPosition, 1000, () =>
            {
                // Обновляем label после завершения движения вниз
                UpdateDeliveredDishesLabel();

                // Анимация возврата (1 секунда)
                AnimateMovement(deliveryPicture, targetPosition, originalPosition, 1000);
            });
        }

        private void UpdateDeliveredDishesLabel()
        {
            // Сортируем блюда по количеству (по убыванию)
            var sortedDishes = _deliveredDishes.OrderByDescending(x => x.Value);

            // Формируем текст для отображения
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Приготовленные блюда:");
            sb.AppendLine("---------------------");

            foreach (var dish in sortedDishes)
            {
                sb.AppendLine($"{dish.Key}: {dish.Value} шт.");
            }

            // Обновляем Label
            label1.Text = sb.ToString();

            // Автоматически подстраиваем размер Label под содержимое
            using (Graphics g = label1.CreateGraphics())
            {
                SizeF size = g.MeasureString(label1.Text, label1.Font, label1.Width);
                label1.Height = (int)Math.Ceiling(size.Height);
            }
        }


        public void InitializeConveyor(int conveyorId, ModuleType startModuleType, ModuleType targetModuleType)
        {
            string controlName = $"conveyor{startModuleType}To{targetModuleType}";
            var pictureBox = Controls.Find(controlName, true).FirstOrDefault() as PictureBox;

            if (pictureBox != null)
            {
                _conveyorPictureBoxes[conveyorId] = pictureBox;
                Console.WriteLine($"Конвейер инициализирован: {startModuleType} -> {targetModuleType} (ID: {conveyorId})");
            }
        }
        public void InitializeModule(int moduleId, ModuleType moduleType)
        {
            string controlName = $"{moduleType.ToString().ToLower()}Module";
            var pictureBox = Controls.Find(controlName, true).FirstOrDefault() as PictureBox;

            if (pictureBox != null)
            {
                _modulePictureBoxes[moduleId] = pictureBox;
                Console.WriteLine($"Модуль инициализирован: {moduleType} (ID: {moduleId})");
            }
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            Controller.StartProduction();
        }
    }
}
